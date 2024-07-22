using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugClicker : MonoBehaviour, IClicker
{
    [SerializeField]
    string message;

    public void TriggerAction()
    {
        Debug.Log(message);
    }
}
