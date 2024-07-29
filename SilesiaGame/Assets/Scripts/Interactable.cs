using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Rendering;

public class Interactable : MonoBehaviour
{
    private int id;
    private static int maxID = 1;
    private bool ableToUse;
    private bool collectable;
    public ICameraMovementType cameraMovementType { get; protected set; }

    //datadump of the object. Here we store the serialized info.
    [SerializeField] private InteractableSO data;
    //[SerializeField] private GameObject outline;
    private static PlayerInput input;
    public static IEnumerable<Interactable> collected = new List<Interactable>();
    


    private void Start()
    {
        collectable = data.collectable;
        id = maxID;
        maxID += 1;
        ableToUse = data.basicState;
        input = PlayerInteract.input;
        

    }

    // method that handles the outline. Subscribed to PlayerInteract class
   
    
    //Hide and Show all objects might not be used. Wrote this for hints
   

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