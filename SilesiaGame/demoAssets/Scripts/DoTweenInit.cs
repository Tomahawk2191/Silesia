using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoTweenInit : MonoBehaviour
{
    void Awake()
    {
        DOTween.Init().SetCapacity(100, 20);
    }
}
