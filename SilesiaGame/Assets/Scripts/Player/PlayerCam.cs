using Cinemachine;
using System.Collections;
using UnityEditor.Rendering;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{

    [SerializeField] private float sensX = 600f;
    [SerializeField] private float sensY = 600f;

    [SerializeField] private Transform orientation;
    public static bool canMoveCamera = true;


    float xRotation; 
    float yRotation;

// CAMERA BOB VARIABLES
    bool bIsOnTheMove;
    CinemachineVirtualCamera vCam;
    [SerializeField] private float AmplitudeGain = 2f;
    [SerializeField] private float FrequencyGain = 0.02f;

    float horizontalInput;
    float verticalInput;


    // Start is called before the first frame update
    void Start()
    {
        LockCursor();
        bIsOnTheMove = false;
        vCam = gameObject.GetComponent<CinemachineVirtualCamera>();
        PlayerInteract.input.OnZoomOutEvent += yourmethod;
        PlayerInteract.input.OnZoomInEvent += yoursecondmethod;

    }

    public static void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked; 
        Cursor.visible = false;
    }

    public static void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.Confined; 
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMoveCamera)
        {
            float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
            float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;
        
            yRotation += mouseX; 

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            // rotate cam and orientation 
            transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
            orientation.rotation = Quaternion.Euler(0, yRotation, 0); 
        }
        
        CheckInput(); 

        CameraBobOn();

    }
    
    
    private void CheckInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        bIsOnTheMove = (horizontalInput != 0f || verticalInput != 0f) && PlayerMovement.getCanMove();
    }

    private void CameraBobOn()
    {
        if (bIsOnTheMove)
        {
            vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = AmplitudeGain; 
        
        }
        else
        {
            vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;
           
        }
    }
    
    
}
