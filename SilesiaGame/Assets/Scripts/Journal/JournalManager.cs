using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class JournalManager : MonoBehaviour
{
    public static GameObject previousArtefactPage;
    public static GameObject previousMainPage;
    // Start is called before the first frame update
    private static PlayerInput input;
    public static bool openedJournal;
    
    void Start()
    {
        gameObject.SetActive(false);
        
        input = PlayerInteract.input;

        input.OpenJournal += onPressJ;
    }

    private void onPressJ(object sender, EventArgs e)
    {
        if (gameObject.activeSelf)
        {
            QuitJournal(this, EventArgs.Empty);
        }

        else
        {
            OpenJournal(this,EventArgs.Empty);
        }
    }

    /*
    IEnumerator DelayAction(float delayTime)
    {
        //Wait for the specified delay time before continuing.
        yield return new WaitForSeconds(delayTime);

        //Do the action after the delay time has finished.
    } */

    private void QuitJournal(object sender, EventArgs e)
    {
        Debug.Log("quit journal");
        PlayerCam.LockCursor();
        PlayerMovement.setCanMove(true);
        PlayerCam.setCanMoveCamera(true);
        gameObject.SetActive(false);
        Debug.Log("close");
        AudioManager.instance.Play("CloseBook");
        //StartCoroutine(DelayAction(4f)); 
        PlayerInteract.input.SwitchToPlayerMap();
        if (previousArtefactPage != null)
        {
            previousArtefactPage.SetActive(false);
        }

        if (previousMainPage != null)
        {
            previousMainPage.SetActive(true);
        }
    }

    private void OpenJournal(object sender, EventArgs e)
    {
        Debug.Log("open journal");
        PlayerCam.UnlockCursor();
        PlayerCam.setCanMoveCamera(false);
        PlayerMovement.setCanMove(false);
        gameObject.SetActive(true);
        Debug.Log("open");
        AudioManager.instance.Play("OpenBook");
        //StartCoroutine(DelayAction(4f));
        PlayerInteract.input.SwitchToJournalMap();
    }

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
