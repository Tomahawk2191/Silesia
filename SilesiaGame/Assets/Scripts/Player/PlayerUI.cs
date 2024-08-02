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

        DialogueDisplay.text = string.Empty;
    }

    private void TogglePauseMenuPerformed(object sender, EventArgs e)
    {
        TogglePauseMenu();
    }

    public void TogglePauseMenu()
    {
        if (_pauseMenuPanel != null)
        {
            if (_pauseMenuPanel.activeSelf )
            {
                
                _pause.SetActive(true);
                PlayerCam.LockCursor();
                PlayerMovement.setCanMove(true);
                _pauseMenuPanel.SetActive(false);
                PlayerCam.setCanMoveCamera(true);
            }
            else
            {
                PlayerCam.UnlockCursor();
                PlayerCam.setCanMoveCamera(false);
                _settings.SetActive(false);
                _pauseMenuPanel.SetActive(true);
                PlayerMovement.setCanMove(false);
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

    public void UpdateDialogueText(string promptMessage)
    {
        if (DialogueDisplay.text != string.Empty)
        {
            HidePreviousLine(promptMessage);
        }
        else
        {
            ShowNewLine(promptMessage);
        }
    }

    private void ShowNewLine(string promptMessage)
    {
        inAnimation = true;
        DialogueDisplay.text = promptMessage;
        var outSeq = DOTween.Sequence();
        outSeq.Insert(0, DialogueDisplay.DOFade(1f, fadeDuration).SetEase(Ease.InOutSine));
        outSeq.AppendCallback(() =>
        {
            inAnimation = false;
            Debug.Log("Anim is finished");
        });
        outSeq.Play();
    }

    private void HidePreviousLine(string promptMessage)
    {
        inAnimation = true;
        var outSeq = DOTween.Sequence();
        outSeq.Insert(0, DialogueDisplay.DOFade(0f, fadeDuration).SetEase(Ease.InSine));
        outSeq.AppendCallback(() => ShowNewLine(promptMessage));
    }

    public void goToMainMenu()
    {
        SceneManager.LoadSceneAsync(0);
    }




}
