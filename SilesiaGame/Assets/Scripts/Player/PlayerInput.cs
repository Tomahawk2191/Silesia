using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;
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

    private DialogueManager _dialogueManager;
    // Start is called before the first frame update

    public PlayerInput()
    {
        _dialogueManager = DialogueManager.Instance;
        input = new DefaultInputs();
        input.Player.Enable();
        input.Player.Interact.performed += Interact_performed;
        input.Player.ShowHint.performed += ShowHint_performed;
        input.Player.ShowHint.canceled += ShowHint_canceled;
        _dialogueManager.OnStartDialogue += BlockInputsForTheDialogue;

    }

    private void BlockInputsForTheDialogue(object sender, EventArgs e)
    {
    }
    private void SwitchToCameraMovementMap(){}

    private void SwitchToPlayerMap()
    {
    }

    private void SwitchToDialogueMap()
    {
        input.Player.Disable();
        input.Dialogue.Enable();
        input.Dialogue.NextLine.performed += NextLine_performed;
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

    // Update is called once per frame
}