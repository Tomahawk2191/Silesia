using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraForBigObjects : ICameraMovementType
{
    public void cameraMoveIn()
    {
        Debug.Log("move in for big");
    }

    public void cameraMoveOut()
    {
        Debug.Log("move out for big");
    }
}
