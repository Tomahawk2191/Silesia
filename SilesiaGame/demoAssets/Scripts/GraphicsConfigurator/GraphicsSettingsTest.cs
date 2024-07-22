using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GraphicsConfigurator.API.URP;
using UnityEngine.Rendering.Universal;
using ShadowResolution = UnityEngine.Rendering.Universal.ShadowResolution;
using TMPro;

public class GraphicsSettingsTest : MonoBehaviour
{
    [Header("Default settings")]
    public float renderScaleDefault = 0.25f;
    public float upsalingFilterIndexDefault = 2;


    [SerializeField] private TMP_Text text;

    private void Start()
    {
        Configuring.CurrentURPA.RenderScale(renderScaleDefault);
        //Configuring.CurrentURPA.UpscalingFilter(UpscalingFilterSelection.Point);
        Configuring.CurrentURPA.OpaqueDownsampling(Downsampling.None);
        text.text = renderScaleDefault.ToString();
    }

    public void RenderScaleChange(float scale)
    {
        if(scale / 20 >= 0.1 && scale / 20 <= 2)
        {
            Configuring.CurrentURPA.RenderScale(scale / 20);
            text.text = (scale / 20).ToString();
        }
    }

    public void UpscalingFilterChange(int change)
    {
        if(change == 0)
            Configuring.CurrentURPA.UpscalingFilter(UpscalingFilterSelection.Auto);
        else if (change == 1)
            Configuring.CurrentURPA.UpscalingFilter(UpscalingFilterSelection.Linear);
        else if (change == 2)
            Configuring.CurrentURPA.UpscalingFilter(UpscalingFilterSelection.Point);
        else if (change == 3)
            Configuring.CurrentURPA.UpscalingFilter(UpscalingFilterSelection.FSR);
    }

    public void MainLightShadowsToggle(bool value)
    {
        Configuring.CurrentURPA.MainLightShadowsCasting(value);
    }

    public void AddLightShadowsToggle(bool value)
    {
        Configuring.CurrentURPA.AdditionalLightsShadowsCasting(value);
    }
}
