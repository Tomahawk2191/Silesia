using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumericOscillator : MonoBehaviour
{
    [SerializeField] float oscillationVal;

    void Update()
    {
        oscillationVal = MathF.Sin(Time.time) * 10; 
    }
}
