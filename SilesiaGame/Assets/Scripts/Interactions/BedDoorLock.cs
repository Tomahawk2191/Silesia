using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BedDoorLock : Interactable
{
    public static BedDoorLock instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("Two instances of bed door script");
        }
    }
    public override void Interact()
    {
        AudioManager.instance.Play("DoorLocked", AudioManager.instance.GetBedDoorPos()); 
    }
}
