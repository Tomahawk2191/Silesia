using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraForBigObjects : MonoBehaviour, ICameraMovementType
{
    // after the movein method is executed, the camera of the player moves to the cameraPosition point. Might need to also add the rotation of the camera and the path that the camera will make.
    [SerializeField]
    private Transform cameraPosition;
    public void cameraMoveIn()
    {
        Debug.Log("move in for big");
    }

    public void cameraMoveOut()
    {
        Debug.Log("move out for big");
    }
}