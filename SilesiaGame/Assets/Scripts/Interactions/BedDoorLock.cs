using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BedDoorLock : Interactable
{
    public static BedDoorLock instance;
    private bool alreadyTried; 

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            alreadyTried = false; 
        }
        else
        {
            Debug.LogError("Two instances of bed door script");
        }
    }
    public override void Interact()
    {
        AudioManager.instance.Play("DoorLocked", AudioManager.instance.GetBedDoorPos());
        if (!alreadyTried) Progress.instance.SetAbsolute(Progress.instance.GetAbsolute() - 1);
        alreadyTried=true;

    }
}
