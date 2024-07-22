using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POIFocusClicker : MonoBehaviour, IClicker
{
    [SerializeField]
    private POI poi;

    public void TriggerAction()
    {
        PoiManager.Instance.Push(poi);
    }
}
