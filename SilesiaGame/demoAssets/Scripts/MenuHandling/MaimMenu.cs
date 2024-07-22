using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MaimMenu : MonoBehaviour
{
    bool gameStarted = false;
    bool isPaused = true;
    [SerializeField] GameObject RaycastBlocker;
    [SerializeField] DialogueManager dialogueManager;
    [SerializeField] GameObject MenuCanvas;
    [SerializeField] GameObject Credits;
    [SerializeField] GameObject Settings;
    [SerializeField] GameObject PausedClicks;
    [SerializeField] GameObject GameTitle;

    private void Start()
    {
        Credits.SetActive(false);
        //Settings.SetActive(false);
        PausedClicks.SetActive(false);
        GameTitle.SetActive(true);
    }

    private void Update()
    {
        if (Keyboard.current.pKey.wasPressedThisFrame)
        {
            isPaused = !isPaused;
            if (isPaused && gameStarted)
            {
                GamePause();
            }
            else if(!isPaused && gameStarted)
            {
                GameUnpause();
            }
        }
    }

    public void StartGame()
    {
        gameStarted = true;
        GameTitle.SetActive(false);
        PausedClicks.SetActive(true);
        GameUnpause();
        dialogueManager.InitializeFirstDialogue();
    }
    public void GameUnpause()
    {
        RaycastBlocker.SetActive(false);
        MenuCanvas.SetActive(false);
    }
    public void GamePause()
    {
        RaycastBlocker.SetActive(true);
        MenuCanvas.SetActive(true);
    }
    public void CreditsEnter()
    {
        Credits.SetActive(true);
    }
    public void CreditsExit()
    {
        Credits.SetActive(false);
    }
    public void SettingsEnter()
    {
        Settings.SetActive(true);
    }
    public void SettingsExit()
    {
        Settings.SetActive(false);
    }
    public void GameExit()
    {
        Application.Quit();
    }
}
