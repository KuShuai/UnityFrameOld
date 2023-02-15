using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPanel : UIWidget
{
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
        Debug.LogError("sortingOrder " + order);
        Renderer.sortingOrder = order;
    }

    //public UIPanelEnum panelEnum { get; private set; }
    //public string PanelName { get; private set; }
    //public UILayer Layer{ get; private set; }
    //public int Header { get; private set; }
    //public bool Fullscreen { get; private set; }

    public UIConfig _uiConfig;

    public void SetUp(UIConfig uiConfig)
    {
        _uiConfig = uiConfig;
        //panelEnum = uiConfig._enum;
        //PanelName = uiConfig._name;
        //Layer = uiConfig._layer;
        //Header = uiConfig._header;
        //Fullscreen = uiConfig._fullscreen;
    }

}
