using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;
using Player;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Video;

public class PlayerInput
{
    //input handler for the DefaultInputs. Triggeres events for other classses based on player input
    //the subscribers are PlayerInteract and Interactable

    private static DefaultInputs input { get; set; }

    public event EventHandler OnInteraction;

    public event EventHandler ShowHint;

    public event EventHandler HideHint;
    public event EventHandler NextLine;

    private InputActionMap _currentActionMap;
    // Start is called before the first frame update

    public PlayerInput()
    {
        input = new DefaultInputs();
        SwitchToPlayerMap();

    }

    public void SwitchToDialogueMap()
    {
        _currentActionMap.Disable();
        _currentActionMap = input.Dialogue;
        _currentActionMap.Enable();
        input.Dialogue.NextLine.performed += NextLine_performed;
    }
    public void SwitchToPlayerMap()
    {
        _currentActionMap.Disable();
        _currentActionMap = input.Player;
        _currentActionMap.Enable();
        input.Player.Interact.performed += Interact_performed;
        input.Player.ShowHint.performed += ShowHint_performed;
        input.Player.ShowHint.canceled += ShowHint_canceled;
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
    public void BlockInputForInteraction() { 
        input.Disable();
    }
    public void EnableInputForInteraction()
    {
        input.Enable();
    }
      

    // Update is called once per frame
}