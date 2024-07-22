using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueClicker : MonoBehaviour, IClicker
{
    [SerializeField] GameObject dialogueManager;
    DialogueManager dialogueManagerScript;
    [SerializeField] TextAsset sourceFile;
    void Start()
    {
        dialogueManagerScript = dialogueManager.GetComponent<DialogueManager>();
    }
    public void TriggerAction()
    {
        Debug.Log("here");
        if (!dialogueManagerScript.inDialogue)
        {
            Debug.Log("here2");
            dialogueManagerScript.sourceFile = this.sourceFile;
            dialogueManagerScript.InitializeDialogue();
            dialogueManagerScript.NextLine();
            StartCoroutine(WaitCoroutine());
            
        }
        
    }
    IEnumerator WaitCoroutine()
    {
        yield return null ;
        dialogueManagerScript.inDialogue = true;
    }
}
