using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ClosedDoor : Interactable
{
    public static ClosedDoor instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("Two instances of door script");
        }
    }
    public override void Interact()
    {
        AudioManager.instance.Play("DoorClosed");
    }
}
