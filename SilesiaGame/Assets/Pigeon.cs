using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pigeon : Interactable
{
    public bool pigeonInteraction;
    public bool pigeonInteracted;
    private bool canEndGame;

    private static PlayerInput input;

    [SerializeField] private GameObject player;

    public static Pigeon Instance { get; private set; }

    void Start()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            input = PlayerInteract.input;
            input.EndGame += endGame;
        }
    }
    private void Awake()
    {
        ableToUse = true;
        cameraMovementType = new CameraForBigObjects();
    }

    //Interact is empty because it doesn't do anything else other than run dialogue
    public override void Interact()
    {
        pigeonInteraction = true;
    }

    private void Update()
    {
        if (!pigeonInteracted)
        {
            return;
        }

        if (Vector3.Distance(player.transform.position, transform.position) < 5)
        {
            canEndGame = true;
            Popup.Instance.TogglePigeonPopup(1f);
        }
        else
        {
            canEndGame = false;
            Popup.Instance.TogglePigeonPopup(0f);

        }
    }

    public void endGame(object sender, EventArgs e)
    {
        if (canEndGame)
        {
            LetterScript.instance.playOutroLetter();
            canEndGame = false;
        }
    }
}
