using System;
using DefaultNamespace;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    private static int maxID = 1;
    protected bool ableToUse;
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

        InspectorModeRotation.setEnabledRotation(!data.isBig);
        DialogueManager.Instance.StartDialogue(this);
        AudioManager.instance.Play("Swipe" + UnityEngine.Random.Range(1, 2));
        Debug.Log("Played Grab Sound");
        ableToUse = false;
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

    public void setLayerToInteractable()
    {
        gameObject.layer = LayerMask.NameToLayer("Interactable");
        foreach (Transform child in transform)
        {
            child.gameObject.layer = LayerMask.NameToLayer("Interactable");
        }
    }

    public void setLayerToDefault()
    {
        gameObject.layer = LayerMask.NameToLayer("Default");
        foreach (Transform child in transform)
        {
            child.gameObject.layer = LayerMask.NameToLayer("Default");
        }
    }

    public bool getAbleToUse()
    {
        return ableToUse;
    }

    public InteractableSO.DialogueText getDialogueTextObj()
    {
        return data.text;
    }

    public void setAbleToUse(bool value)
    {
        ableToUse = value;
    }
}