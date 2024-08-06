using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using DefaultNamespace;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Video;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; set; }

    private Queue<string> _sentences;
    private PlayerInput _input;
    public static bool justFinishedTheDialogue = false;
    public static Interactable currentObject;
    public static InspectorModeRotation rotation;

    // Start is called before the first frame update
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("There two instances of DialogueManagers");
        }
    }

    void Start()
    {
        _input = PlayerInteract.input;
        _sentences = new Queue<string>();
        PlayerInteract.input.NextLine += DisplayNextSentence;
        rotation = GetComponent<InspectorModeRotation>();
    }

    // Update is called once per frame
    public void StartDialogue(Interactable interactable)
    {
        FindObjectOfType<AudioManager>().Play("Grab");
        currentObject = interactable;
        //currentObject.setLayerToInteractable();
        InspectorModeRotation.setObject(currentObject.transform);
        InspectorModeRotation.setEnabledRotation(true);
        PlayerInteract.Instance.blockPlayerForDialogue();
        currentObject.cameraMovementType.cameraMoveIn();
        _input.SwitchToDialogueMap();

        _sentences.Clear();
        string[] str = interactable.getText().Split('\n');
        foreach (var sentence in str)
        {
            _sentences.Enqueue(sentence);
        }
        DisplayNextSentence(this, EventArgs.Empty);

    }

    private void DisplayNextSentence(object sender, EventArgs e)
    {
        
        if (PlayerUI.Instance.inAnimation)
            return;
        if (!_sentences.Any()) 
        {
            EndDialogue();
            return;
        }
        string sentence = _sentences.Dequeue();
        PlayerUI.Instance.UpdateDialogueText(sentence);
    }

    private static void EndDialogue()
    {
        //currentObject.setLayerToDefault();
        currentObject.cameraMovementType.cameraMoveOut();
        PlayerUI.Instance.UpdateDialogueText(String.Empty);
        PlayerInteract.input.SwitchToPlayerMap();
        PlayerInteract.Instance.unblockPlayerFromDialogue();
        currentObject = null;
        InspectorModeRotation.setEnabledRotation(false);


    }

}

