using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LivingDoorLock : Interactable
{
    public static LivingDoorLock instance;
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
            Debug.LogError("Two instances of door script");
        }
    }
    public override void Interact()
    {
        AudioManager.instance.Play("DoorLocked", AudioManager.instance.GetLivingDoorPos());
        if (!alreadyTried) Progress.instance.SetAbsolute(Progress.instance.GetAbsolute() - 1);
        alreadyTried = true;
    }
}
