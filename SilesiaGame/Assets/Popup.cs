using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Popup : MonoBehaviour
{
    private GameObject _journalPopup;
    

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
        }
    }
    public void JournalPopup()
    {
        StartCoroutine(JournalFade());
        
    }

  
    IEnumerator JournalFade()
    {
        float alpha = _journalPopup.GetComponent<CanvasGroup>().alpha;
        DOTween.To(() => alpha, x => alpha = x, 1f, 2f).SetEase(Ease.InOutCubic).OnUpdate(() => _journalPopup.GetComponent<CanvasGroup>().alpha = alpha);
        yield return new WaitForSeconds(3f);
        DOTween.To(() => alpha, x => alpha = x, 0f, 2f).SetEase(Ease.InOutCubic).OnUpdate(() => _journalPopup.GetComponent<CanvasGroup>().alpha = alpha);
    }
}
