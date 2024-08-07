using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using DefaultNamespace;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Video;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; set; }

    private Queue<String> _sentences;
    private Queue<Color> _sentColors;
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
        _sentences = new Queue<String>();
        _sentColors = new Queue<Color>();
        PlayerInteract.input.NextLine += DisplayNextSentence;
        rotation = GetComponent<InspectorModeRotation>();
    }

    public void StartDialogue(Interactable interactable)
    {
        AudioManager.instance.Play("Grab");
        currentObject = interactable;
        currentObject.setLayerToInteractable();
        InspectorModeRotation.setObject(currentObject.transform);
        InspectorModeRotation.setEnabledRotation(true);
        PlayerInteract.Instance.blockPlayerForDialogue();
        currentObject.cameraMovementType.cameraMoveIn();
        _input.SwitchToDialogueMap();

        _sentences.Clear();
        // get array of text obj for enqueue
        InteractableSO.DialogueText dialogueTexts = currentObject.getDialogueTextObj();
        foreach (var text in dialogueTexts.getComboSpeakerTexts())
        {
            //Debug.Log("color start");
            foreach (String extractedText in text.getTextAsset())
            {
                _sentences.Enqueue(extractedText);
                Color extractedColor = text.getTextColor();
                _sentColors.Enqueue(extractedColor);
            }
            // Debug.Log("extractedColor: " + extractedColor);
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
        Color lineColor = _sentColors.Dequeue();

        if (lineColor != null)
        {
            PlayerUI.Instance.UpdateDialogueText(sentence, lineColor);
        }
        else
        {
            Debug.LogError(sentence + " color is null");
        }
    }

    private static void EndDialogue()
    {
        currentObject.setLayerToDefault();
        currentObject.cameraMovementType.cameraMoveOut();
        PlayerUI.Instance.ClearDialogueText();
        PlayerInteract.input.SwitchToPlayerMap();
        PlayerInteract.Instance.unblockPlayerFromDialogue();
        currentObject = null;
        InspectorModeRotation.setEnabledRotation(false);
    }
}

