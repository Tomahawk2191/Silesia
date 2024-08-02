using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpeningScript : MonoBehaviour
{
    private static int interactions = 0;
    private static DoorOpeningScript Instance { get; set; }

    public static void newInteraction()
    {
        interactions++;
        if (interactions == 3)
        {
            Debug.Log("openThedoor");
            Instance.transform.Rotate(0,90,0);
        }
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("Two instances of the door");
        }
        else
        {
            Instance = this;
        }
    }
}
