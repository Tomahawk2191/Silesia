using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour
{
    [SerializeField]
    private LayerMask mask;

    [SerializeField] DialogueManager dialogueManagerScript;

    void Start()
    {
        InputManager.Instance.OnInteract += Interact;
    }

    public void Interact()
    {
        //TODO Chck If we can interact
        if (!dialogueManagerScript.inDialogue)
        {
            if (Physics.Raycast(
            Camera.main.ScreenPointToRay(
                InputManager.Instance.MousePositiotn
                ),
            out var hit,
            float.PositiveInfinity,
            mask.value))
            {
                var clickers = hit.collider.GetComponents<IClicker>();

                foreach (var clicker in clickers)
                {
                    clicker.TriggerAction();
                }
            }
        }
        
    }
}
