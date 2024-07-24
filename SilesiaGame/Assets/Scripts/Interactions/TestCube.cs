using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCube : Interactable
{
    CameraForSmallObjects cameraMovement;

    //Just a test Interactable. For other uses create other scripts
    public override void Interact(PlayerInteract playerInteract)
    {
        if (cameraMovement == null)
        {
            cameraMovement = new CameraForSmallObjects(playerInteract, transform);
            cameraMovement.cameraMoveIn();
        }
    }
    public override void Uninteract()
    {
        if (cameraMovement != null)
        {
            cameraMovement.cameraMoveOut();
            cameraMovement = null;
        }
    }

    //THIS IS VERY WRONG AND SUBJECT TO CHANGE!!!!!!
    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Uninteract();
        }
    }

}
