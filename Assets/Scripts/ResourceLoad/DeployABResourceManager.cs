using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DeployABResourceManager : ResourceManager
{
    public class AssetLoadInfo
    {
        public string assetPath;
        public string mainBundle;
        public string[] dependencies;//“¿¿µ
    }

    public class AsyncAssetRequest
    {
        public AssetLoadInfo LoadInfo = null;
        public ProcRes Callback = null;
        public AssetBundleRequest loadRequest = null;

        public void AssetLoaded(Object asset)
        {
            if (Callback != null)
            {
                Callback(asset);
            }
        }
    }

    private Dictionary<int, Object> _resource = new Dictionary<int, Object>();
    private Dictionary<int, AssetBundle> _bundles = new Dictionary<int, AssetBundle>();

    private Dictionary<int, string> _bundles_index = new Dictionary<int, string>();
    private AssetBundleManifest _manifest = null;

    private List<AsyncAssetRequest> asyncAssetLoadRequest = new List<AsyncAssetRequest>();
    private Dictionary<int, AssetBundleCreateRequest> asyncBundleLoadRequest = new Dictionary<int, AssetBundleCreateRequest>();
    private List<int> asyncBundleLoadRequestRemoveList = new List<int>();

    private float last_time = 0;

    public override void Init()
    {
        base.Init();
        last_time = Time.realtimeSinceStartup;
        //try
        {
            AssetBundle index_bundle = _LoadBundle(ResourceManagerConfig.kIndexFileName);
            LoadIndexFile(index_bundle.LoadAsset<TextAsset>(ResourceManagerConfig.kIndexFileName));
            index_bundle.Unload(false);
            index_bundle = null;

            Debug.Log("begin to load mainfest");

            AssetBundle manifest_bundle = _LoadBundle("Bundles");
            _manifest = manifest_bundle.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
            manifest_bundle.Unload(false);
            manifest_bundle = null;

            Debug.Log("manifest loaded");

            //LoadShaders();//LoadBundle(ResourceManagerConfig.kShaderBundleName)
        }
        //catch (System.Exception ex)
        //{
        //    Debug.LogError(ex.Message);
        //    Debug.LogError(ex.StackTrace);
        //}
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="asset_path">UIPrefabs/PanelName</param>
    /// <returns></returns>
    public override Object Load(string asset_path)
    {
        Object obj = null;

        int path_hash = asset_path.GetHashCode();
        if (_resource.TryGetValue(path_hash,out obj))
        {
            if (obj == null)
            {
                _resource.Remove(path_hash);
            }
            else
            {
                return obj;
            }
        }

        var assetLoadInfo = GetAssetLoadInfo(asset_path);

        //if (!_CheckAssetLoadInfo(assetLoadInfo))
        //{
        //    return null;
        //}

        //for (int i = 0; i < assetLoadInfo.dependencies.Length; i++)
        //{

        //}
        //Debug.LogError(assetLoadInfo.mainBundle);
        var mainBundle = _LoadBundleSync(assetLoadInfo.mainBundle.ToLower());
        if (mainBundle == null)
        {
            Debug.LogWarningFormat("DeployResourceManager::Load load asset main bundle failed: {0}, {1}", asset_path, assetLoadInfo.mainBundle);
            return null;
        }

        obj = mainBundle.LoadAsset(asset_path);
        if (obj == null)
        {
            Debug.LogWarningFormat("DeployResourceManager::Load load asset failed from bundle {1}: {0}", asset_path, mainBundle);
            return null;
        }
        _resource.Add(path_hash, obj);

        return obj;
    }

    public override void Unload(string asset_path)
    {
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="bundle_name">  @"index"; </param>
    /// <returns></returns>
    private AssetBundle _LoadBundle(string bundle_name)
    {
        bundle_name = ResourceManagerConfig.FormatString("Bundles/{0}", bundle_name);   //==================Bundles/index

        string bundle_load_path = string.Format("{0}/{1}", Application.dataPath, bundle_name);

        if (string.IsNullOrEmpty(bundle_load_path))
        {
            Debug.LogErrorFormat("get load bundle path failed 1: {0}", bundle_name);
            return null;
        }

        var bundle = AssetBundle.LoadFromFile(bundle_load_path);
        if (bundle == null)
        {
            Debug.LogErrorFormat("load index bundle failed :{0}", bundle_load_path);
            return null;
        }
        return bundle;
    }

    private AssetBundle _LoadBundleSync(string bundle_name)
    {
        AssetBundle bundle = _GetBundle(bundle_name);
        if (bundle == null)
        {
            var bd_hash = bundle_name.GetHashCode();

            AssetBundleCreateRequest bdRequest = null;
            if (asyncBundleLoadRequest.TryGetValue(bd_hash,out bdRequest))
            {
                bundle = bdRequest.assetBundle;
            }
            else
            {
                string bundle_load_path = _FormatBundleLoadPath(bundle_name);
                bundle = AssetBundle.LoadFromFile(bundle_load_path);
            }

            if (bundle != null)
            {
                _bundles.Add(bd_hash, bundle);
            }
        }
        return bundle;
    }

    private AssetBundle _GetBundle(string bundle_name)
    {
        AssetBundle rt = null;
        var bd_hash = bundle_name.GetHashCode();
        _bundles.TryGetValue(bd_hash, out rt);
        return rt;
    }

    private void LoadIndexFile(TextAsset index_file)
    {
        Debug.Log("begin to load index file");
        string[] lines = index_file.text.Split('\n');
        char[] trim = new char[] { '\r', '\n' };
        if (lines != null && lines.Length > 0)
        {
            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i].Trim(trim);
                if (string.IsNullOrEmpty(line))
                    continue;

                string[] pair = line.Split(':');
                if (pair.Length != 2)
                    continue;

                _bundles_index.Add(pair[0].GetHashCode(), pair[1]);
            }
        }

        if (_bundles_index.Count != 0)
        {
            Debug.LogFormat("index file loaded final count {0}", _bundles_index.Count);
        }
        else
        {
            Debug.LogError("index file failed to load");
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="asset_path">UIPrefabs/PanelName</param>
    /// <returns></returns>
    private AssetLoadInfo GetAssetLoadInfo(string asset_path)
    {
        AssetLoadInfo rt = new AssetLoadInfo();

        rt.assetPath = asset_path;//UIPrefabs/PanelName
        rt.mainBundle = GetAssetBundleFileName(asset_path);
        rt.dependencies = _manifest.GetAllDependencies(rt.mainBundle);

        return rt;
    }

    private string GetAssetBundleFileName(string asset_path)
    {
        int asset_hash = asset_path.GetHashCode();
        string bundle_name;
        if (_bundles_index.TryGetValue(asset_hash,out bundle_name))
        {
            return bundle_name;
        }
        return string.Empty;
    }

    private string _FormatBundleLoadPath(string bundle_name)
    {
        bundle_name = ResourceManagerConfig.FormatString("/Bundles/{0}", bundle_name);
        string bundle_load_path = Application.dataPath+bundle_name;
        return bundle_load_path;
    }

    public override bool LoadLuaScript(string asset_name, out byte[] content)
    {
//#if UNITY_EDITOR
//        string full_path = ResourceManagerConfig.FormatString("{0}/../Lua/Scripts/{1}.lua", Application.dataPath, asset_name);
//        if (File.Exists(full_path))
//        {
//            content = File.ReadAllBytes(full_path);
//            return true;
//        }
//        content = null;
//        return false;
//#else
        string path = RSPathUtil.LuaScript(asset_name);//LuaScripts/XXX.lua
        var asset = Load<TextAsset>(path);
        content = asset != null ? asset.bytes : null;
        return asset != null;
//#endif
    }

}