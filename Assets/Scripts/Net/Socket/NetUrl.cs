using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BulletData", menuName = "ScriptableObject/子弹数据", order = 0)]
public class NetUrl : ScriptableObject
{
    private static NetUrl instance;
    public static NetUrl Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new NetUrl();
            }
            return instance;
        }
    }
    public string host;
}
