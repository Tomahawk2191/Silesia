using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LivingDoorLock : Interactable
{
    public static LivingDoorLock instance;
    
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
        AudioManager.instance.Play("DoorLocked", AudioManager.instance.GetLivingDoorPos());
        Progress.instance.SetAbsolute(Progress.instance.GetAbsolute() - 1);
    }
}
