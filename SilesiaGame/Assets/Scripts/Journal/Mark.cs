using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mark : MonoBehaviour
{
    [SerializeField] private GameObject _defPos;
    [SerializeField] private GameObject _openPos;
    [SerializeField] private bool isOpen;
    private int id;
    private static int maxid = 1;
    private static Mark previousPage;
    [SerializeField] private GameObject relatedPage;
    

    private void Start()
    {
        if (isOpen)
        {
            id = 0;
            isOpen = true;
            transform.position = _defPos.transform.position - new Vector3(-40,40*id,0);
            if (id == 0 && previousPage != null)
            {
                Debug.LogError("Two pages of the diary are selected as isOpen = true");
            }
            previousPage = this;
        }
        else
        {
            id = maxid;
            transform.position = _defPos.transform.position - new Vector3(0,40*id,0);
            maxid++;
        }
    }

    public void SwitchPage()
    {
        if (previousPage == this) return;
        previousPage.CloseDedicatedPage();
        OpenDedicatedPage();
        AudioManager.instance.Play("PageTurn" + (UnityEngine.Random.Range(0, 2) + 1));
        previousPage = this;
    }


    private void OpenDedicatedPage()
    {
        relatedPage.SetActive(true);
        isOpen = true;
        transform.position = _defPos.transform.position - new Vector3(-40,40*id,0);
    }

    private void CloseDedicatedPage()
    {
        relatedPage.SetActive(false);
        isOpen = false;
        transform.position = _defPos.transform.position - new Vector3(0,40*id,0);
    }
}
