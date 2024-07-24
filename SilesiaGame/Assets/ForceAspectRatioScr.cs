using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ForceAspectRatioScr : MonoBehaviour
{

    [SerializeField] int width = 1920;
    [SerializeField] int height = 1080;
    //[SerializeField] RenderTexture outlineOut;
    [SerializeField] RawImage ppFilter;

    [SerializeField] Material pProcess;
    void Start()
    {
        //outlineOut.width = Screen.width;
        //outlineOut.height = Screen.height;
        //ppFilter.rectTransform.sizeDelta = new Vector2(Screen.width, Screen.height);
        //outlineCam.fieldOfView = Camera.main.fieldOfView;

        

        float targetAspect = (float)width / (float)height;

        float windowAspect = (float)Screen.width / (float)Screen.height;

        float scaleHeight = windowAspect / targetAspect;

        if (scaleHeight < 1.0f)
        {
            pProcess.SetVector("offset", new Vector2(0, 0));
            ppFilter.rectTransform.sizeDelta = new Vector2(Screen.width, Screen.height * scaleHeight);
            Rect rect = Camera.main.rect;

            rect.width = 1.0f;
            rect.height = scaleHeight;
            rect.x = 0;
            rect.y = 0;
            //rect.y = (1.0f - scaleHeight) / 2.0f;

            Camera.main.rect = rect;
        }
        else
        {
            float scaleWidth = 1.0f / scaleHeight;
            pProcess.SetVector("offset", new Vector2(scaleWidth, 0));
            ppFilter.rectTransform.sizeDelta = new Vector2(Screen.width* scaleWidth, Screen.height);

            Rect rect = Camera.main.rect;

            rect.width = scaleWidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scaleWidth) / 2.0f;
            rect.y = 0;

            Camera.main.rect = rect;
        }

        ppFilter.rectTransform.position = new Vector2(Screen.width / 2, Screen.height / 2);
        
        
    }
}
