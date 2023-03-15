using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[XLua.LuaCallCSharp]
public class UIConfig
{
    public UIPanelEnum _enum;
    public string name;
    public UILayer layer;
    public bool fullscreen;
}

public class UIConfigSingleton : Singleton<UIConfigSingleton>, ISingleton
{
    Dictionary<UIPanelEnum, UIConfig> _allUIConfig;
    Dictionary<string, UIPanelEnum> _allUIName;
    public void SingletonInit()
    {
        _allUIConfig = new Dictionary<UIPanelEnum, UIConfig>();
        _allUIName = new Dictionary<string, UIPanelEnum>();

        //AddUIConfig(UIPanelEnum.UIPanel1, UIPanelConst.UIPanel1);
        //AddUIConfig(UIPanelEnum.UIPanel2, UIPanelConst.UIPanel2, UILayer.Fixed);
        //AddUIConfig(UIPanelEnum.UIPanel3, UIPanelConst.UIPanel3,UILayer.Top);
        AddUIConfig(UIPanelEnum.UIPanel4, UIPanelConst.UIPanel4);
    }

    public void Init()
    {

    }

    private void AddUIConfig(UIPanelEnum _enum,string _UIPanel = UIPanelConst.Empty, UILayer layer = UILayer.Normal,  bool fullscreen = true)
    {
        _allUIConfig.Add(_enum, new UIConfig()
        {

            _enum = _enum,
            name = _UIPanel,
            layer = layer,
            fullscreen = fullscreen
        });
        _allUIName.Add(_UIPanel, _enum);
    }

    public UIConfig GetUIConfig(int ui)
    {
        UIConfig rt = GetUIConfig((UIPanelEnum)ui);
        return rt;
    }

    public UIConfig GetUIConfig(UIPanelEnum ui)
    {
        UIConfig ret = null;
        _allUIConfig.TryGetValue(ui, out ret);
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
