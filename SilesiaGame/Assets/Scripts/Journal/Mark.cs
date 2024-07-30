using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mark : MonoBehaviour
{
    [SerializeField] private GameObject _defPos;
    [SerializeField] private GameObject _openPos;
    private bool isOpen;
    private int id;
    private static int maxid = 0;
    private static Mark previousPage;
    [SerializeField] private GameObject relatedPage;
    

    private void Start()
    {
        
        id = maxid;
        if (this.id == 0)
        {
            isOpen = true;
            transform.position = _openPos.transform.position - new Vector3(0,40*id,0);
            transform.Rotate(0,0,180,0);
        }
        maxid++;
        if (previousPage == null) previousPage = this;
        transform.position = _defPos.transform.position - new Vector3(0,40*id,0);
        

    }

    public void SwitchPage()
    {
        if (previousPage == this) return;
        previousPage.CloseDedicatedPage();
        OpenDedicatedPage();
        previousPage = this;
    }


    private void OpenDedicatedPage()
    {
        relatedPage.SetActive(true);
        isOpen = true;
        Debug.Log("open page");
        transform.Rotate(0,0,180,0);
        transform.position = _openPos.transform.position - new Vector3(0,40*id,0);
    }

    private void CloseDedicatedPage()
    {
        relatedPage.SetActive(false);
        isOpen = false;
        Debug.Log("close page");
        transform.position = _defPos.transform.position - new Vector3(0,40*id,0);
        transform.Rotate(0,0,180,0);
    }
}
