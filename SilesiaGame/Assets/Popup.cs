using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Popup : MonoBehaviour
{
    private GameObject _journalPopup;
    private GameObject _WASDPopup;
    

    public static Popup Instance { get; private set; }

    private void Awake()
    {

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            _journalPopup = transform.Find("JournalPopup").gameObject;
            _WASDPopup = transform.Find("WASDPopup").gameObject;
        }
    }
    public void JournalPopup()
    {
        StartCoroutine(Fade(_journalPopup));
        
    }

    public void WASDPopup()
    {
        StartCoroutine(Fade(_WASDPopup));
    }

    IEnumerator Fade(GameObject obj)
    {
        Debug.Log(obj.name);
        float alpha = obj.GetComponent<CanvasGroup>().alpha;
        DOTween.To(() => alpha, x => alpha = x, 1f, 2f).SetEase(Ease.InOutCubic).OnUpdate(() => obj.GetComponent<CanvasGroup>().alpha = alpha);
        yield return new WaitForSeconds(3f);
        DOTween.To(() => alpha, x => alpha = x, 0f, 2f).SetEase(Ease.InOutCubic).OnUpdate(() => obj.GetComponent<CanvasGroup>().alpha = alpha);
    }
}
