using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput
{
    //input handler for the DefaultInputs. Triggeres events for other classses based on player input
    //the subscribers are PlayerInteract and Interactable

    private static DefaultInputs input { get; set; }

    public event EventHandler OnInteraction;

    public event EventHandler ShowHint;

    public event EventHandler HideHint;
    public event EventHandler NextLine;

    public event EventHandler OpenJournal;
    public event EventHandler QuitJournal;
    public event EventHandler OpenPauseMenu;
    public event EventHandler OnZoomInEvent;
    public event EventHandler OnZoomOutEvent;


    private InputActionMap _currentActionMap;
    // Start is called before the first frame update

    public PlayerInput()
    {
        input = new DefaultInputs();
        SwitchToPlayerMap();
        //input.UI.Enable();
        input.Journal.Enable();
        input.Player.Interact.performed += Interact_performed;
        input.Player.ShowHint.performed += ShowHint_performed;
        input.Player.ShowHint.canceled += ShowHint_canceled;
        input.Journal.QuitJournal.performed += OpenJournal_performed;
        input.Dialogue.NextLine.performed += NextLine_performed;
        input.Player.PauseMenu.performed += OpenPauseMenu_performed;
    }

    private void OnzoomOut(InputAction.CallbackContext obj)
    {
        OnZoomOutEvent?.Invoke(this, EventArgs.Empty);
    }

    private void OnZoomIn(InputAction.CallbackContext obj)
    {
        OnZoomInEvent?.Invoke(this, EventArgs.Empty);
    }

    private void OpenPauseMenu_performed(InputAction.CallbackContext obj)
    {
        OpenPauseMenu?.Invoke(this, EventArgs.Empty);
    }

    public void SwitchToDialogueMap()
    {
        if (_currentActionMap != null)
        {
            _currentActionMap.Disable();
        }
        _currentActionMap = input.Dialogue;
        _currentActionMap.Enable();
        input.Journal.Enable();
        input.Zoom.Enable();
        input.Player.PauseMenu.performed -= OpenPauseMenu_performed;
        input.Journal.CloseWithESC.performed -= OpenJournal_performed;
        input.Zoom.ZoomIn.performed -= OnZoomIn;
        input.Zoom.ZoomIn.canceled -= OnzoomOut;

    }

    public void SwitchToPlayerMap()
    {
        if (_currentActionMap != null)
        {
            _currentActionMap.Disable();
        }
        _currentActionMap = input.Player;
        _currentActionMap.Enable();
        input.Journal.Enable();
        input.Zoom.Enable();
        input.Player.PauseMenu.performed += OpenPauseMenu_performed;
        input.Journal.CloseWithESC.performed -= OpenJournal_performed;
        input.Zoom.ZoomIn.performed += OnZoomIn;
        input.Zoom.ZoomIn.canceled += OnzoomOut;

    }

    public void EnableZoomIN()
    {
        
    }
    public void DisableZoomIN()
    {
        
    }

    public void SwitchToJournalMap()
    {
        if (_currentActionMap != null)
        {
            _currentActionMap.Disable();
        }
        _currentActionMap = input.Journal;
        _currentActionMap.Enable();
        input.Player.PauseMenu.performed -= OpenPauseMenu_performed;
        input.Journal.CloseWithESC.performed += OpenJournal_performed;
        input.Zoom.Disable();
        input.Zoom.ZoomIn.performed -= OnZoomIn;
        input.Zoom.ZoomIn.canceled -= OnzoomOut;

    }

    private void QuitJournal_performed(InputAction.CallbackContext obj)
    {
        QuitJournal?.Invoke(this, EventArgs.Empty);
    }

    private void OpenJournal_performed(InputAction.CallbackContext obj)
    {
        OpenJournal?.Invoke(this, EventArgs.Empty);
    }

    public void DisableInputForCameraMovemen()
    {
        _currentActionMap.Disable();
    }

    public void EnableInputForCameraMovemen()
    {
        _currentActionMap.Enable();
    }

    private void NextLine_performed(InputAction.CallbackContext obj)
    {
        NextLine?.Invoke(this, EventArgs.Empty);
    }

    private void ShowHint_canceled(InputAction.CallbackContext obj)
    {
        HideHint?.Invoke(this, EventArgs.Empty);
    }

    private void ShowHint_performed(InputAction.CallbackContext obj)
    {
        ShowHint?.Invoke(this, EventArgs.Empty);
    }

    private void Interact_performed(InputAction.CallbackContext obj)
    {
        OnInteraction?.Invoke(this, EventArgs.Empty);
    }

    public void BlockInputForInteraction()
    {
        input.Disable();
    }

    public void EnableInputForInteraction()
    {
        input.Enable();
    }
}