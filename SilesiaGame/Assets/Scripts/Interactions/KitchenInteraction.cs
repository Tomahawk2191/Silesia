using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenInteraction : Interactable
{
    public override void Interact()
    {
        DoorOpeningScript.newInteraction();
    }
}
