using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumericOscillator : MonoBehaviour
{

    [SerializeField] float oscillationVal = 1.0f;
    float distance = 0f; 



    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        oscillationVal = MathF.Sin(Time.time) * 10; 

    }

}
