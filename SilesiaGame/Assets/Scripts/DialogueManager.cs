using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Video;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; set; }

    private Queue<string> _sentences;
    private PlayerInput _input;
    public static bool inTheDialogue = false;

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
    }

    // Update is called once per frame
    public void StartDialogue(TextAsset text)
    {
        if (inTheDialogue) return;
        inTheDialogue = true;
        PlayerInteract.selectedInteractable.cameraMovementType.cameraMoveIn();
        _input.SwitchToDialogueMap();
        Debug.Log("Started the dialogue");
        _sentences.Clear();
        string[] str = text.text.Split('\n');
        foreach (var sentence in str)
        {
            _sentences.Enqueue(sentence);
        }

    }

    private void DisplayNextSentence(object sender, EventArgs e)
    {
        if (!inTheDialogue) return;
        if (!_sentences.Any()) 
        {
            EndDialogue();
            return;
        }
        string sentence = _sentences.Dequeue();
        Debug.Log(sentence);
        PlayerUI.Instance.UpdateDialogueText(sentence);
    }

    private static void EndDialogue()
    {
        Debug.Log("End of Dialogue");
        if (PlayerInteract.selectedInteractable != null)
        {
            PlayerInteract.selectedInteractable.cameraMovementType.cameraMoveOut();
        }
        PlayerUI.Instance.UpdateDialogueText(String.Empty);
        PlayerInteract.input.SwitchToPlayerMap();
        inTheDialogue = false;

    }
    
}

