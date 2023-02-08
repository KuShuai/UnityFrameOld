using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoSingleton<T>: MonoBehaviour where T : MonoBehaviour,IMonoSingleton
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject obj = new GameObject(typeof(T).Name);
                DontDestroyOnLoad(obj);
                instance = obj.AddComponent<T>();
                instance.SingletonInit();
                Debug.Log("Create");
            }
            return instance;
        }
    }
}
