using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    private int id;
    private static int maxID = 1;
    private bool ableToUse;
    private static List<Interactable> listOfAllObjects = new List<Interactable>();

    //datadump of the object. Here we store the serialized info.
    [SerializeField] private InteractableSO data;


    private void Start()
    {
        id = maxID;
        maxID += 1;
        listOfAllObjects.Add(this);
        ableToUse = data.basicState;
    }

    // method called on interacting with an object
    //this should be completly rewriten once the inpput system will be made
    //see the OnInteraction State Diagram (lucidchart)
    public void OnInteraction()
    {
        if (ableToUse)
        {
            Debug.Log(getLine(0));
            GetComponent<ICameraMovementType>().cameraMoveIn();
        }

    }

    // method called on hovering over the object
    public void OnHover()
    {
        Debug.Log(gameObject.name + " was hovered");
    }
    
    public string getLine(int lineNumber)
    {
        return GetComponent<InteractableSO>().text.text.Split('\n')[lineNumber];
    }
    // method called to set ableToUse to true for all connected objects
    public virtual void Interact()
    {
    }

}
