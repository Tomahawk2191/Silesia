using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JournalManager : MonoBehaviour
{
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
        gameObject.SetActive(false);
        Debug.Log("close");
        PlayerInteract.input.SwitchToPlayerMap();
    }

    private void OpenJournal(object sender, EventArgs e)
    {
        gameObject.SetActive(true);
        Debug.Log("open");
        PlayerInteract.input.SwitchToJournalMap();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void NextPage()
    {
        Debug.Log("next");

    }

    private void PreviousPage()
    {
        Debug.Log("previous");

    }
}
