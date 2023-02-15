using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPanel : UIWidget
{
    public UIConfig _uiConfig;

    private Canvas Renderer;
    public void SetRender(Canvas canvas)
    {
        Renderer = canvas;
    }

    public Canvas GetCanvas()
    {
        return Renderer;
    }

    public void SetCanvasStatus(bool enabled)
    {
        if (Renderer != null && Renderer.enabled != enabled)
        {
            Renderer.enabled = enabled;
        }
    }

    public void SetOrder(int order)
    {
        Renderer.sortingOrder = order;
    }

    public void SetUp(UIConfig uiConfig)
    {
        _uiConfig = uiConfig;
    }

}
