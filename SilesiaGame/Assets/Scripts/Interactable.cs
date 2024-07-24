using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    private int id;
    private static int maxID = 1;
    private bool ableToUse;

    //datadump of the object. Here we store the serialized info.
    [SerializeField] private InteractableSO data;
    [SerializeField] private GameObject outline;
    private static PlayerInput input;


    private void Start()
    {
        id = maxID;
        maxID += 1;
        ableToUse = data.basicState;
        input = PlayerInteract.input;
        PlayerInteract.Instance.OnSelectedArtefactChanged += Instance_OnSelectedArtefactChanged;
        input.ShowHint += ShowAllObjects;
        input.HideHint += HideAllObjects;

    }

    private void Instance_OnSelectedArtefactChanged(object sender, PlayerInteract.OnSelectedArtefactChangedEventArgs e)
    {
        if (this == e.selectedArtefact)
        {
            setOutlineON();
        }
        else
        {
            setOutlineOFF();
        }
    }


    private void HideAllObjects(object sender, EventArgs e)
    {
        setOutlineOFF();
    }

    private void ShowAllObjects(object sender, EventArgs e)
    {
        setOutlineON();
    }
    

    // method called on interacting with an object
    //this should be completly rewriten once the inpput system will be made
    //see the OnInteraction State Diagram (lucidchart)

    // method called on hovering over the object

    public string getLine(int lineNumber)
    {
        return GetComponent<InteractableSO>().text.text.Split('\n')[lineNumber];
    }
    // method called to set ableToUse to true for all connected objects
    public virtual void Interact()
    {
    }


    public void setOutlineON()
    {
        outline.SetActive(true);

    }
    public void setOutlineOFF()
    {
        outline.SetActive(false);

    }
}