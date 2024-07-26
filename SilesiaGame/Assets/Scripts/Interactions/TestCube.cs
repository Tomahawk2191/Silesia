using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCube : Interactable
{
    private void Awake()
    {
        cameraMovementType = new CameraForSmallObjects(this.transform);
    }

    //Just a test Interactable. For other uses create other scripts
    public override void Interact()
    {
    }

}
