using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    int id;
    bool ableToUse;

    [SerializeField]
    ICameraMovementType cameraMovementType;

    [SerializeField] private InteractableSO data;
    
    
    // method called on interacting with an object
    public void OnInteraction()
    {
        cameraMovementType.cameraMoveIn();
        GetComponent<Interaction>().interact();
        
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
