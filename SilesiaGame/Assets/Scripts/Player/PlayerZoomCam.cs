using Cinemachine;
using UnityEngine;

public class PlayerZoomCam : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera vcam;

    public void switchCamera(CinemachineVirtualCamera switchCam)
    {
        if (Input.GetButtonDown("Fire2") && !PlayerMovement.getCanMove())
        {
            switchCam.Priority = 20;
            vcam.Priority = 10;
        }
    }
}
