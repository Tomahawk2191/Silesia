using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ForceAspectRatioScr : MonoBehaviour
{

    [SerializeField] float aspectX = 16;
    [SerializeField] float aspectY = 9;

    void Start()
    {
        float targetAspect = aspectX / aspectY;

        float windowAspect = (float)Screen.width / (float)Screen.height;

        float scaleHeight = windowAspect / targetAspect;

        if (scaleHeight < 1.0f)
        {
            Rect rect = Camera.main.rect;

            rect.width = 1.0f;
            rect.height = scaleHeight;
            rect.x = 0;
            //rect.y = 0;
            rect.y = (1.0f - scaleHeight) / 2.0f;

            Camera.main.rect = rect;
        }
        else
        {
            float scaleWidth = 1.0f / scaleHeight;

            Rect rect = Camera.main.rect;

            rect.width = scaleWidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scaleWidth) / 2.0f;
            rect.y = 0;

            Camera.main.rect = rect;
        }
      
        
    }
}
