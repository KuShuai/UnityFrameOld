//using UnityEngine;
//using System;
//using System.IO;

//public class PaintingResourceManager : MonoBehaviour
//{
//    public delegate void ProcRes(UnityEngine.Object res);
//    public delegate void ProcTextAsssetRes(byte[] bytes);

//    private static PaintingResourceManager _inst;
//    public static PaintingResourceManager Inst { get { return _inst; } }

//    public static void CreateInstance()
//    {
//        if (_inst == null)
//        {
//#if UNITY_EDITOR
//            bool developMode = UnityEditor.EditorPrefs.GetBool("DevelopMode", true);

//            if (developMode)
//            {
//                //_inst = PaintingUtil.CreateDonDestroyObj<DevelopResourceManager>("Develop|ResourceManager");//开发
//            }
//            else
//            {
//                //_inst = PaintingUtil.CreateDonDestroyObj<DeployResourceManager>("Deploy|ResourceManager");//部署
//            }
//#else
//            _inst = PaintingUtil.CreateDonDestroyObj<DeployResourceManager>("Deploy|ResourceManager");
//#endif
//        }
//    }

//    public virtual void Init() { }
//    public virtual void Release() { }

//    public void LoadAndProcTextAsset(string path, ProcTextAsssetRes proc)
//    {
        
//        TextAsset file = Load(path) as TextAsset;

//        if (file == null)
//        {
//            return;
//        }
//        try
//        {
//            proc(file.bytes);
//        }
//        catch (Exception e)
//        {
//            Debug.LogException(e);
//        }
//    }

//    public void LoadAndProc(string path, ProcRes proc)
//    {
//        UnityEngine.Object obj = Load(path);

//        if (obj == null)
//        {
//            return;
//        }
//        try
//        {
//            proc(obj);
//        }
//        catch (Exception e)
//        {
//            Debug.LogException(e);
//        }
//    }

//    public void LoadAsyncAddProc(string path, ProcRes proc)
//    {
//        throw new System.NotImplementedException();
//    }

//    public virtual UnityEngine.Object Load(string asset_name) { return null; }

//    public T Load<T>(string asset_name) where T : UnityEngine.Object
//    {
//        UnityEngine.Object obj = Load(asset_name);
//        return obj as T;
//    }

//    public UnityEngine.Object GetAssetInstance(string asset_name)
//    {
//        UnityEngine.Object obj = Load(asset_name);
//        return obj != null ? Instantiate(obj) : null;
//    }

//    public T GetAssetInstance<T>(string asset_name) where T : UnityEngine.Object
//    {
//        UnityEngine.Object obj = Load(asset_name);
//        return obj != null ? Instantiate(obj) as T : null;
//    }

//    //public GameObject LoadGesture(string asset_name)
//    //{
//    //    string path = PaintingRSPath.Gesture(asset_name);
//    //    return Load<GameObject>(path);
//    //}

//    public virtual bool LoadLuaScript(string asset_name, out byte[] content)
//    {
//        content = null;
//        return false;
//    }

//    public virtual void Unload(string asset_path) { }

//    public virtual void LoadShaders() { }
//    public virtual void LoadFont() { }

//    public virtual string LocateCriFilePath(string asset_name) { return null; }
//    public virtual string LocateCriAudioFilePath(string asset_name) { return null; }

//    public virtual void UnloadResources() { }

//    public virtual bool AssetExist(string asset_path) { return true; }
//}
