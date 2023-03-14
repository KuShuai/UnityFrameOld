using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

[LuaCallCSharp]
public class LuaInterface_UI 
{
    public static UIPanel OpenPanel(int panel_name)
    {
        return UIManager.OpenUIPanel_LUA(panel_name);
    }

    public static void ClosePanel(int panel_name)
    {
        UIManager.CloseUI_LUA(panel_name);
    }
}
