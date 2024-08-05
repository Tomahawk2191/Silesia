using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressTracker : MonoBehaviour
{
    private float itemsCollected = 0f; 
    [HideInInspector] public float percentcomplete = 0f;
    [SerializeField] private float totalItems = 15f;

    public void Increment()
    {
        itemsCollected = itemsCollected + 1f; 
        percentcomplete = itemsCollected / totalItems;

    }


}
