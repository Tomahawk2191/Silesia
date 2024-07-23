using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    //input handler for the DefaultInputs. Triggeres events for other classses based on player input

    private DefaultInputs input;

    public event EventHandler OnInteraction;
    // Start is called before the first frame update

    private void Awake()
    {
        input = new DefaultInputs();
        input.Player.Enable();
        input.Player.Interact.performed += Interact_performed;
    }

    private void Interact_performed(InputAction.CallbackContext obj)
    {
        OnInteraction?.Invoke(this, EventArgs.Empty);
    }

    // Update is called once per frame
}
