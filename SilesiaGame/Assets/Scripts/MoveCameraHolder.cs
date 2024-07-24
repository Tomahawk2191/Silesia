using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCameraHolder : MonoBehaviour
{

    [SerializeField]
    private Transform cameraPosition; 

    // Update is called once per frame
    void Update()
    {
        transform.position = cameraPosition.position; 
    }
}
