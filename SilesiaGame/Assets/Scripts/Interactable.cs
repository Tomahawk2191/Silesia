using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Interactable : MonoBehaviour
{
    private int id;
    private static int maxID = 1;
    private bool ableToUse;

    public ICameraMovementType cameraMovementType { get; protected set; }

    //datadump of the object. Here we store the serialized info.
    [SerializeField] private InteractableSO data;
    //[SerializeField] private GameObject outline;
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

    //THIS METHOD MUST BE OVERRIDEN IN CLASSES THAT EXTEND INTERACTABLE
    public virtual void Interact()
    {
    }

    public void TriggerDialogue()
    {
        if (ableToUse)
        {
            DialogueManager.Instance.StartDialogue(this);
        }

        ableToUse = false;
    }


    public void setOutlineON()
    {
        //outline.SetActive(true);

    }
    public void setOutlineOFF()
    {
        //outline.SetActive(false);

    }

    public bool getAbleToUse()
    {
        return ableToUse;
    }

    public string getText()
    {
        return data.text.text;
    }
    
}