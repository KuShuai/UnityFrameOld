using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

[LuaCallCSharp]
public class LuaInterface
{
    public static void DebugLog(string param,params object[] others)
    {
        Debug.LogFormat(param, others);
    }
}
