using Cinemachine;
using System.Collections;
using UnityEditor.Rendering;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    // CAMERA MOVEMENT CONTROL FACTORS
    [SerializeField] private float sensX = 600f;
    [SerializeField] private float sensY = 600f;

    [SerializeField] private Transform orientation;


    float xRotation;
    float yRotation;


    // CAMERA BOB VARIABLES
    bool bIsOnTheMove;
    CinemachineVirtualCamera vCam;
    [SerializeField] private float AmplitudeGain = 2.0f;
    [SerializeField] private float FrequencyGain = 0.02f;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        bIsOnTheMove = false;
    }










    // Update is called once per frame
    void Update()
    {
        // get mouse input
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        yRotation += mouseX;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // rotate cam and orientation 
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);

        // check if object is moving

        Debug.Log("Called checkMoving");

        Tempfunc();

        CheckMoving();

        CameraBobOn();

        //Debug.Log()


    }

    void Tempfunc()
    {
        Debug.Log("Ran checkMoving");
        Vector3 startPos = new Vector3(3f,3f,3f);
        Debug.Log("set startPos"); 
    }

    private IEnumerator CheckMoving()
    {
        Debug.Log("Ran checkMoving");
        Vector3 startPos = vCam.transform.position;
        yield return new WaitForSeconds(0.01f);
        Vector3 finalPos = vCam.transform.position;

        //bIsOnTheMove = (startPos.x != finalPos.x || startPos.y != finalPos.y
        // || startPos.z != finalPos.z);
        Debug.Log("Set CM Vars");
        if (startPos.x != finalPos.x || startPos.z != finalPos.z)
            bIsOnTheMove = true;
        Debug.Log("StartPos.x != finalPos.x: " + (startPos.x != finalPos.x));
        Debug.Log("StartPos.z != finalPos.z: " + (startPos.z != finalPos.z));

    }

    void CameraBobOn()
    {
        Debug.Log("Ran CameraBobOn");
        if (bIsOnTheMove) vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = FrequencyGain;
        else vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 0;
    }



}
