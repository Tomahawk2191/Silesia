using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerUI : MonoBehaviour
{
    public static PlayerUI Instance { get; set; }

    [SerializeField] private TextMeshProUGUI InteractionText;

    [SerializeField] private TextMeshProUGUI DialogueDiasplay;
    // Start is called before the first frame update
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There are two player UIs");
        }
        else
        {
            Instance = this;
        }
        InteractionText.text = String.Empty;
        DialogueDiasplay.text = String.Empty;

    }

    // Update is called once per frame
    public void UpdateInteractionText(string promptMessage)
    {
        InteractionText.text = promptMessage;
    }

    public void UpdateDialogueText(string promptMessage)
    {
        DialogueDiasplay.text = promptMessage;
    }
}
