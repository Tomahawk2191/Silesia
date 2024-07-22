using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    public Vector2 MousePositiotn => Input.Main.MousePos.ReadValue<Vector2>();

    public event Action OnInteract;
    public event Action OnBack;

    private InputActions Input;

    private void Awake()
    {
        Assert.IsNull(Instance);
        Instance = this;
        Input = new InputActions();
        Input.Main.Click.started += (_) => OnInteract?.Invoke();
        Input.Main.Back.started += (_) => OnBack?.Invoke();
    }

    void OnEnable()
    {
        Input.Enable();
    }

    private void OnDisable()
    {
        Input.Disable();
    }
}
