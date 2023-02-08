using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Util : MonoBehaviour
{
    public static T CreateObj<T>(string name) where T:Component
    {
        T ret = null;
        GameObject obj = new GameObject(name);
        obj.SetActive(true);
        ret = obj.AddComponent<T>();
        return ret;
    }

    public static T CreateDonDestroyObj<T>(string name) where T : Component
    {
        T ret = CreateObj<T>(name);
        DontDestroyOnLoad(ret.gameObject);
        return ret;
    }

}
