using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public static PlayerUI Instance { get; set; }

    //this is old text, we are just using a 2d cursor now
    //[SerializeField] private TextMeshProUGUI promptText;

    [SerializeField] private GameObject interactCursor;
    [SerializeField] private GameObject normalCursor;

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
    }

    // old
    //public void UpdateText(string promptMessage)
    //{
    //    promptText.text = promptMessage;
    //}

    public void ShowInteractCursor()
    {
        interactCursor.SetActive(true);
        normalCursor.SetActive(false);
    }

    public void ShowNormalCursor()
    {
        interactCursor.SetActive(false);
        normalCursor.SetActive(true);
    }
}
