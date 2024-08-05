using System;
using JetBrains.Annotations;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    private static int maxID = 1;
    private bool ableToUse;
    private bool collectable;
    [SerializeField] private GameObject photo;
    public ICameraMovementType cameraMovementType { get; protected set; }
    public static event EventHandler<NewItemCollected> collectableInteracted;

    public class NewItemCollected : EventArgs
    {
        public int id;
    }

    //datadump of the object. Here we store the serialized info.
    [SerializeField] private InteractableSO data;
    //[SerializeField] private GameObject outline;
    private static PlayerInput input;

    private void Start()
    {
        if (data.isBig)
        {
            cameraMovementType = new CameraForBigObjects();
        }
        else
        {
            cameraMovementType = new CameraForSmallObjects(this.transform);
        }
        collectable = data.collectable;
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
        GetComponentInChildren<Renderer>().material.renderQueue = 4000;
        if (ableToUse)
        {
            DialogueManager.Instance.StartDialogue(this);
            ableToUse = false;
        }

        if (collectable)
        {
            collectableInteracted?.Invoke(this, new NewItemCollected()
            {
                id = data.id
            });
        }

        if (photo != null)
        {
            photo.SetActive(true);
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