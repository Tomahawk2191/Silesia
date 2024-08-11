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
    private GameObject _journalTextPopup;
    private GameObject _pigeonPopup;
    

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
            _journalTextPopup = transform.Find("JournalTextPopup").gameObject;
            _WASDPopup = transform.Find("WASDPopup").gameObject;
            _LMBPopup = transform.Find("LMBPopup").gameObject;
            _KeyPopup = transform.Find("KeyPopup").gameObject;
            _pigeonPopup = transform.Find("PigeonPopup").gameObject;
        }
    }
    public void JournalPopup()
    {
        Debug.Log("normal journal popup[");
            StartCoroutine(Fade(_journalPopup));
    }

    public void JournalTextPopup()
    {
        StartCoroutine(Fade(_journalTextPopup));
    }

    public void WASDPopup()
    {
        StartCoroutine(Fade(_WASDPopup));
    }

    public void LMBPopup()
    {
        StartCoroutine(Fade(_LMBPopup));

    }

    public void TogglePigeonPopup(float alpha)
    {
        _pigeonPopup.GetComponent<CanvasGroup>().alpha = alpha;

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
