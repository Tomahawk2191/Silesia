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

    // method that handles the outline. Subscribed to PlayerInteract class
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
    
    //Hide and Show all objects might not be used. Wrote this for hints
    private void HideAllObjects(object sender, EventArgs e)
    {
        setOutlineOFF();
    }

    private void ShowAllObjects(object sender, EventArgs e)
    {
        setOutlineON();
    }
    

    public string getLine(int lineNumber)
    {
        return GetComponent<InteractableSO>().text.text.Split('\n')[lineNumber];
    }
    
    //THIS METHOD MUST BE OVERRIDEN IN CLASSES THAT EXTEND INTERACTABLE
    public virtual void Interact(PlayerInteract playerInteract)
    {
    }
    public virtual void Uninteract()
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