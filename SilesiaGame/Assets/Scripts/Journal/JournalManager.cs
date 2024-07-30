using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JournalManager : MonoBehaviour
{
    public static GameObject previousArtefactPage;
    public static GameObject previousMainPage;
    // Start is called before the first frame update
    private static PlayerInput input;
    void Start()
    {
        gameObject.SetActive(false);
        
        input = PlayerInteract.input;

        input.OpenJournal += OpenJournal;
        input.QuitJournal += QuitJournal;
    }

    private void QuitJournal(object sender, EventArgs e)
    {
        PlayerCam.LockCursor();
        PlayerMovement.setCanMove(true);
        PlayerCam.canMoveCamera = true;
        gameObject.SetActive(false);
        Debug.Log("close");
        PlayerInteract.input.SwitchToPlayerMap();
    }

    private void OpenJournal(object sender, EventArgs e)
    {
        PlayerCam.UnlockCursor();
        PlayerCam.canMoveCamera = false;
        PlayerMovement.setCanMove(false);
        gameObject.SetActive(true);
        Debug.Log("open");
        PlayerInteract.input.SwitchToJournalMap();
    }

    // Update is called once per frame
    

    public void CloseButtonPressed()
    {
        if (previousMainPage != null)
        {
            if (previousMainPage.activeSelf)
            {
                QuitJournal(this,EventArgs.Empty);
            }
            else
            {
                PhotoButton.CloseArtefactDescriptionPage();
            }
        }
        else
        {
            QuitJournal(this,EventArgs.Empty);
        }
        
    }
    
}
