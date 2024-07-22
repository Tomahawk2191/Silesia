using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonSound : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] AudioSource source;
    [SerializeField] AudioClip clip;

    public void OnPointerEnter(PointerEventData eventData)
    {
        source.PlayOneShot(clip);
    }
}
