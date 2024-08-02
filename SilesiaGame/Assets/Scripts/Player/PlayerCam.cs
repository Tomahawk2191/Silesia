using Cinemachine;
using System;
using System.Collections;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    [Header("Camera Attributes")]
    [SerializeField] private float sensX = 100f;
    [SerializeField] private float sensY = 100f;
    private static float sensModifier = 5; 
    
    [SerializeField] private Transform orientation;
    private static bool canMoveCamera = true;

    private float xRotation;
    private float yRotation;

    [Header("CameraBob Variables")]
    private bool bIsOnTheMove;
    private CinemachineVirtualCamera vCam;
    [SerializeField] private float AmplitudeGain = 2f;
    [SerializeField] private float FrequencyGain = 0.02f;

    [Header("Inspect Camera")]
    [SerializeField] private CinemachineVirtualCamera inspectCam;
    private static CinemachineVirtualCamera currentCam;
    private CinemachineBrain brain;
    [SerializeField] private float blendTime;

    [Header("Shader")]
    [SerializeField] private Material shaderMat;
    private Renderer rend;

    private bool isZoomed;

    private float horizontalInput;
    private float verticalInput;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        bIsOnTheMove = false;
        vCam = gameObject.GetComponent<CinemachineVirtualCamera>();
        brain = Camera.main.GetComponent<CinemachineBrain>();
        blendTime = brain.m_DefaultBlend.BlendTime;
        currentCam = vCam;
        LockCursor();
        PlayerInteract.input.OnZoomOutEvent += onZoomOut;
        PlayerInteract.input.OnZoomInEvent += onZoomIn;
        //rend = shaderMat.GetComponent<Renderer>();
    }

    private void onZoomIn(object sender, EventArgs e)
    {
        isZoomed = true;
        switchCamera(inspectCam);
        rend.material.SetFloat("_ditherStrength", 200);
    }

    public static void setSensModifier(float value)
    {
        sensModifier = value;
    }

    private void onZoomOut(object sender, EventArgs e)
    {
        isZoomed = false;
        switchCamera(vCam);
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
            float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX * sensModifier;
            float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY * sensModifier;
            
            yRotation += mouseX;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            // rotate cam and orientation
            currentCam.transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
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

    public void switchCamera(CinemachineVirtualCamera switchCam)
    {
        switchCam.Priority = 20;
        currentCam.Priority = 10;
        currentCam = switchCam;
    }

    public static bool getCanMoveCamera()
    {
        return canMoveCamera;
    }

    public static void setCanMoveCamera(bool set)
    {
        canMoveCamera = set;
    }

    public static CinemachineVirtualCamera getCurrentCamera()
    {
        return currentCam;
    }

    public void playIntroLetter()
    {
        GameObject player = transform.parent.transform.parent.transform.Find("Player").gameObject;
        player.transform.position = new Vector3(84.9300003f, 3.61249995f, -2.66000009f);
        player.transform.rotation = Vector3(0,270,0);
        
        PlayerInteract playerInteract =
            player.GetComponent<PlayerInteract>();
        playerInteract.blockPlayerForDialogue();
        
        GameObject introLetter = GameObject.Find("IntroLetter");

        introLetter.transform.position = new Vector3(83.3259964f,4.8499999f,-2.3900001f);
        StartCoroutine(ScrollLetter(introLetter, playerInteract));

    }

    IEnumerator ScrollLetter(GameObject introLetter, PlayerInteract playerInteract)
    {
        yield return new WaitForSeconds(1);
        while (introLetter.transform.position.y < 6.81699991f || introLetter.transform.position.z < -2.43199992f)
        {
            introLetter.transform.Translate(Vector3.up);
        }
        yield return new WaitForSeconds(1);
        playerInteract.unblockPlayerFromDialogue();
    }
    
    public void playOutroLetter()
    {
        PlayerInteract playerInteract =
            transform.parent.transform.parent.transform.Find("Player").GetComponent<PlayerInteract>();
        playerInteract.unblockPlayerFromDialogue();
        
    }
}