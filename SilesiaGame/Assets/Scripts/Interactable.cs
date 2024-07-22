using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    int id;
    bool ableToUse;

    [SerializeField]
    ICameraMovementType cameraMovementType;

    private void Update()
    {
        Debug.Log(DialogueManagerScr.Instance.getLine());
    }

    // method called on interacting with an object
    public void OnInteraction()
    {
        cameraMovementType.cameraMoveIn();
        Debug.Log(gameObject.name + " was interacted");
    }

    // method called on hovering over the object
    public void OnHover()
    {
        Debug.Log(gameObject.name + " was hovered");
    }

    // method called to set ableToUse to true for all connected objects
    private void EnableObjects()
    {

    }
}
