using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PhotoButton : MonoBehaviour
{
    [SerializeField] private GameObject artefactDescriptionPage;
    [SerializeField] private GameObject mainPage;

    // Start is called before the first frame update
    public void OpenArtefactDescriptionPage()
    {
        Debug.Log("chuj");
        artefactDescriptionPage.SetActive(true);
        JournalManager.previousArtefactPage = this.artefactDescriptionPage;
        JournalManager.previousMainPage = mainPage;
        mainPage.SetActive(false);
        FindObjectOfType<AudioManager>().Play("PageTurn" + (UnityEngine.Random.Range(0, 2) + 1));
    }

    public static void CloseArtefactDescriptionPage()
    {
        if (JournalManager.previousArtefactPage != null)
        {
            JournalManager.previousArtefactPage.SetActive(false);
        }

        if (JournalManager.previousMainPage != null)
        {
            JournalManager.previousMainPage.SetActive(true);
        }
    }
}
