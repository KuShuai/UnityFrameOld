using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIConfig
{
    public UIPanelEnum _enum;
    public string _name;
    public UILayer _layer;
    public int _header;
    public bool _fullscreen;

    public UIConfig(UIPanelEnum panelEnum, string name = UIPanelConst.Empty, UILayer layer =UILayer.Normal,int header = 0,bool fullscreen = true)
    {
        _enum = panelEnum;
        _name = name;
        _layer = layer;
        _header = header;
        _fullscreen = fullscreen;
    }
}

public class UIConfigSingleton : Singleton<UIConfigSingleton>, ISingleton
{
    Dictionary<UIPanelEnum, UIConfig> _allUIConfig;
    Dictionary<string, UIPanelEnum> _allUIName;
    public void SingletonInit()
    {
        _allUIConfig = new Dictionary<UIPanelEnum, UIConfig>();
        _allUIName = new Dictionary<string, UIPanelEnum>();

        AddUIConfig(UIPanelEnum.UIPanel1, UIPanelConst.UIPanel1);
        AddUIConfig(UIPanelEnum.UIPanel2, UIPanelConst.UIPanel2, UILayer.Fixed);
        AddUIConfig(UIPanelEnum.UIPanel3, UIPanelConst.UIPanel3,UILayer.Top);
        AddUIConfig(UIPanelEnum.UIPanel4, UIPanelConst.UIPanel4);
    }

    private void AddUIConfig(UIPanelEnum _enum,string _UIPanel = UIPanelConst.Empty, UILayer layer = UILayer.Normal, int header = 0, bool fullscreen = true)
    {
        _allUIConfig.Add(_enum, new UIConfig(_enum, _UIPanel, layer, header, fullscreen));
        _allUIName.Add(_UIPanel, _enum);
    }

    public UIConfig GetUIConfig(UIPanelEnum ui)
    {
        UIConfig ret = null;
        _allUIConfig.TryGetValue(ui,out ret);
        return ret;
    }

    public UIConfig GetUIConfig(string _UIPanel)
    {
        UIConfig ret = null;
        UIPanelEnum _enum = UIPanelEnum.None;
        _allUIName.TryGetValue(_UIPanel, out _enum);
        _allUIConfig.TryGetValue(_enum, out ret);
        return ret;
    }

    public void SingletonDestory()
    {
    }
}
