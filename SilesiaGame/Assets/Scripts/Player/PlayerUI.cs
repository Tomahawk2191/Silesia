using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using TMPro;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public static PlayerUI Instance { get; set; }

    //this is old text, we are just using a 2d cursor now
    //[SerializeField] private TextMeshProUGUI promptText;

    [SerializeField] private GameObject interactCursor;
    [SerializeField] private GameObject normalCursor;
    [SerializeField] private TextMeshProUGUI DialogueDisplay;
    [SerializeField] float fadeDuration;
    [SerializeField] float fadeDistance;
    [SerializeField] private GameObject _pauseMenuPanel;
    [SerializeField] private GameObject _settings;
    [SerializeField] private GameObject _pause;
    public bool inAnimation { get; private set; }


    private bool cameraWasLocked = false;

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

        DialogueDisplay.text = String.Empty;
    }

    private void TogglePauseMenuPerformed(object sender, EventArgs e)
    {
        TogglePauseMenu();
    }

    public void TogglePauseMenu()
    {
        if (_pauseMenuPanel != null)
        {
            if (_pauseMenuPanel.activeSelf)
            {
                
                _pause.SetActive(true);
                _pauseMenuPanel.SetActive(false);
                if (cameraWasLocked)
                {                    
                    PlayerMovement.setCanMove(true);                    
                    PlayerCam.setCanMoveCamera(true);                    
                }

                PlayerCam.LockCursor();
                JournalManager.inTheMenu = false;

                Time.timeScale = 1f;
                AudioManager.instance._inGame.TransitionTo(0.2f); 
            }
            else
            {
                cameraWasLocked = PlayerCam.getCanMoveCamera();
                Time.timeScale = 0f;
                JournalManager.inTheMenu = true;
                PlayerCam.UnlockCursor();
                PlayerCam.setCanMoveCamera(false);
                _settings.SetActive(false);
                _pauseMenuPanel.SetActive(true);
                PlayerMovement.setCanMove(false);
                AudioManager.instance._pause.TransitionTo(0.2f); 
            }
        }
    }

    private void Start()
    {
        DialogueDisplay.alpha = 0f;
        PlayerInteract.input.OpenPauseMenu += TogglePauseMenuPerformed;
    }

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

    public void HideAllCursors()
    {
        interactCursor.SetActive(false);
        normalCursor.SetActive(false);
    }

    public void UpdateDialogueText(string promptMessage, Color lineColor, FontStyles lineFontStyle, FontWeight lineFontWeight) {
        if (DialogueDisplay.text != string.Empty)
        {
            HidePreviousLine(promptMessage, lineColor, lineFontStyle, lineFontWeight);
        }
        else
        {
            ShowNewLine(promptMessage, lineColor, lineFontStyle, lineFontWeight);
        }
    }

    public void ClearDialogueText()
    {
        HidePreviousLine(String.Empty, Color.clear, FontStyles.Normal, FontWeight.Regular);
    }

    private void ShowNewLine(string promptMessage, Color lineColor, FontStyles lineFontStyle, FontWeight linefontWeight)
    {
        inAnimation = true;
        DialogueDisplay.color = lineColor;
        DialogueDisplay.text = promptMessage;
        DialogueDisplay.fontStyle = lineFontStyle;
        DialogueDisplay.fontWeight = linefontWeight;
        var outSeq = DOTween.Sequence();
        outSeq.Insert(0, DialogueDisplay.DOFade(1f, fadeDuration).SetEase(Ease.InOutSine));
        outSeq.AppendCallback(() =>
        {
            inAnimation = false;
        });
        outSeq.Play();
    }

    private void HidePreviousLine(string promptMessage, Color linecolor, FontStyles lineFontStyle, FontWeight linefontWeight)
    {
        inAnimation = true;
        var outSeq = DOTween.Sequence();
        outSeq.Insert(0, DialogueDisplay.DOFade(0f, fadeDuration).SetEase(Ease.InSine));
        outSeq.AppendCallback(() => ShowNewLine(promptMessage, linecolor, lineFontStyle, linefontWeight));
    }

    public void goToMainMenu()
    {
        SceneManager.LoadSceneAsync(0);
    }

}
