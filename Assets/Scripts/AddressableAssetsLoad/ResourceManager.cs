using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager :MonoBehaviour
{
    public delegate void ProcTextAsssetRes(byte[] bytes);

    private static ResourceManager instance;
    public static ResourceManager Instance { get { return instance; } }
    public static void CreateInstance()
    {
        if (instance == null)
        {
#if UNITY_EDITOR
            bool deploy_AA = UnityEditor.EditorPrefs.GetBool("Deploy_AA", false);
            bool deploy_AB = UnityEditor.EditorPrefs.GetBool("Deploy_AB", false);
            if (deploy_AA)
            {
                instance = Util.CreateDonDestroyObj<DeployAAResourceManager>("Resource|DeployAA");
            }
            else if (deploy_AB)
            {
                instance = Util.CreateDonDestroyObj<DeployABResourceManager>("Resource|DeployAB");
            }
            else
            {
                instance = Util.CreateDonDestroyObj<DevelopResourceManager>("Resource|Develop");
            }
#else
#endif
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

    public void LoadAndProcTextAsset(string path, ProcTextAsssetRes proc)
    {
        TextAsset file = Load(path) as TextAsset;
        if (file != null)
        {
            try
            {
                proc(file.bytes);
            }
            catch (System.Exception e) 
            {
                Debug.LogError(e);
            }
        }
    }


    public virtual void Unload(string asset_path) { }
}
