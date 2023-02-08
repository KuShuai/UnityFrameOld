using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager :MonoBehaviour
{
    private static ResourceManager instance;
    public static ResourceManager Instance { get { return instance; } }
    public static void CreateInstance()
    {
        if (instance == null)
        {
            instance = Util.CreateDonDestroyObj<DevelopResourceManager>("Resource|Develop");
        }
    }

    public virtual void Init() { }
    public virtual void Release() { }
    
    public T Load<T>(string asset_name) where T : UnityEngine.Object
    {
        UnityEngine.Object obj = Load(asset_name);
        return obj as T;
    }

    public virtual UnityEngine.Object Load(string asset_name) { return null; }


}
