using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Video;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; set; }

    private Queue<string> sentences;
    private TextMeshPro display;
    public event EventHandler OnStartDialogue;
    private PlayerInput input;

    // Start is called before the first frame update
    void Start()
    {
        input = PlayerInteract.input;
        sentences = new Queue<string>();
    }

    // Update is called once per frame
    public void StartDialogue(TextAsset text)
    {
        input.SwitchToDialogueMap();
        input.DisableInputForCameraMovemen();
        Debug.Log("Started the dialogue");
        sentences.Clear();
        string[] str = text.text.Split('\n');
        foreach (var sentence in str)
        {
            sentences.Enqueue(sentence);
        }
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count() == 0) 
        {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        Debug.Log(sentence);
    }

    private void EndDialogue()
    {
        
    }
}

