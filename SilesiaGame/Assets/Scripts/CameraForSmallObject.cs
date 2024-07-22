using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraForSmallObjects : ICameraMovementType
{
    public void cameraMoveIn()
    {
        Debug.Log("move in for small");
    }

    public void cameraMoveOut()
    {
        Debug.Log("move out for small");
    }
}
