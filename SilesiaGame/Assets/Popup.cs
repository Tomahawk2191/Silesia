using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Popup : MonoBehaviour
{
    private GameObject _journalPopup;
    private GameObject _WASDPopup;
    private GameObject _LMBPopup;
    private GameObject _KeyPopup;
    

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
            _LMBPopup = transform.Find("LMBPopup").gameObject;
            _KeyPopup = transform.Find("KeyPopup").gameObject;
        }
    }
    public void JournalPopup()
    {
        if (JournalManager.openedJournal)
        {
            StartCoroutine(Fade(_journalPopup));
        }
        
        
    }

    public void WASDPopup()
    {
        StartCoroutine(Fade(_WASDPopup));
    }

    public void LMBPopup()
    {
        StartCoroutine(Fade(_LMBPopup));

    }

    public void KeyPopup()
    {
        float alpha = _KeyPopup.GetComponent<CanvasGroup>().alpha;
        if (LivingroomDoor.bedDoorOpen)
        {
            
            DOTween.To(() => alpha, x => alpha = x, 1f, 2f).SetEase(Ease.InOutCubic).OnUpdate(() => _KeyPopup.GetComponent<CanvasGroup>().alpha = alpha);
        }
        else
        {
            DOTween.To(() => alpha, x => alpha = x, 0f, 2f).SetEase(Ease.InOutCubic).OnUpdate(() => _KeyPopup.GetComponent<CanvasGroup>().alpha = alpha);

        }

    }

    IEnumerator Fade(GameObject obj)
    {
        float alpha = obj.GetComponent<CanvasGroup>().alpha;
        DOTween.To(() => alpha, x => alpha = x, 1f, 2f).SetEase(Ease.InOutCubic).OnUpdate(() => obj.GetComponent<CanvasGroup>().alpha = alpha);
        yield return new WaitForSeconds(3f);
        DOTween.To(() => alpha, x => alpha = x, 0f, 2f).SetEase(Ease.InOutCubic).OnUpdate(() => obj.GetComponent<CanvasGroup>().alpha = alpha);
    }
}
