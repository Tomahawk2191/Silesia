using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Matchbox : Interactable
{
    private void Awake()
    {
        cameraMovementType = new CameraForSmallObjects(this.transform);
    }

    public override void Interact()
    {
        Debug.Log("matchymatchy");
        LivingroomDoor.bedDoorOpen = true;
        Popup.Instance.KeyPopup();
        //Progress.instance.CollectKey();
        LivingDoorLock.instance.gameObject.SetActive(false);
    }
}
