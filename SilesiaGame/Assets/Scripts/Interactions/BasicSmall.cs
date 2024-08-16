using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicSmall : Interactable
{
    private void Awake()
    {
        cameraMovementType = new CameraForSmallObjects(this.transform);
    }

    //Interact is empty because it doesn't do anything else other than run dialogue
    public override void Interact()
    {
        
    }
}
