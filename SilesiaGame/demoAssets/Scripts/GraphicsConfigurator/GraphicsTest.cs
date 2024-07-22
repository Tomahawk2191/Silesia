using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GraphicsConfigurator.API.URP;
using UnityEngine.Rendering.Universal;
using ShadowResolution = UnityEngine.Rendering.Universal.ShadowResolution;

public class GraphicsTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Configuring.CurrentURPA.RenderScale(0.2f);
        Configuring.CurrentURPA.UpscalingFilter(UpscalingFilterSelection.FSR);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
