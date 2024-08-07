using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuLoop : MonoBehaviour
{
    private List<Transform> frames = new List<Transform>();
    private int currentFrame = 2;
    private int previousFrame = 1;

    private void Start()
    {
        foreach (Transform child in transform)
        {
                frames.Add(child);
                child.gameObject.SetActive(false);
        }
        frames[0].gameObject.SetActive(true);
        StartCoroutine(measureTime());
    }

    IEnumerator measureTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.2f);
            NextFrame();
        }
    }

    private void NextFrame()
    {
        frames[previousFrame].gameObject.SetActive(false);
        frames[currentFrame].gameObject.SetActive(true);
        
        previousFrame = currentFrame;
        currentFrame++;
        currentFrame %= 7;
    }
}
