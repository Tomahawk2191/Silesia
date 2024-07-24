using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput
{
    //input handler for the DefaultInputs. Triggeres events for other classses based on player input

    private static DefaultInputs input { get; set; }

    public event EventHandler OnInteraction;

    public event EventHandler ShowHint;

    public event EventHandler HideHint;
    // Start is called before the first frame update

    public PlayerInput()
    {
        input = new DefaultInputs();
        input.Player.Enable();
        input.Player.Interact.performed += Interact_performed;
        input.Player.ShowHint.performed += ShowHint_performed;
        input.Player.ShowHint.canceled += ShowHint_canceled;
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