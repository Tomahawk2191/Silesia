using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowInteract : Interactable
{
    private void Awake()
    {
        cameraMovementType = new CameraForBigObjects();
    }

    //Interact is empty because it doesn't do anything else other than run dialogue
    public override void Interact()
    {
        base.Interact();
        if (!Progress.instance.GetWindowStatus()) KitchenWindow.instance.OpenWindow(); 
    }
}
