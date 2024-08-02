using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraForBigObjects : ICameraMovementType
{
    // after the movein method is executed, the camera of the player moves to the cameraPosition point. Might need to also add the rotation of the camera and the path that the camera will make.
    [SerializeField]
    private Transform cameraPosition;

    private Transform transform;
    

    public void cameraMoveIn()
    {
        PlayerInteract.input.BlockInputForInteraction();
    }

    public void cameraMoveOut()
    {
        PlayerInteract.input.EnableInputForInteraction();
    }
}