using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeployABResourceManager : ResourceManager
{
    private Dictionary<int, Object> _resource = new Dictionary<int, Object>();
    private Dictionary<int, AssetBundle> _bundles = new Dictionary<int, AssetBundle>();

    private Dictionary<int, string> _bundles_index = new Dictionary<int, string>();
    private AssetBundleManifest _manifest = null;

    private float last_time = 0;

    public override void Init()
    {
        base.Init();
        last_time = Time.realtimeSinceStartup;
    }

    public override Object Load(string asset_name)
    {
        Object obj = null;

        AssetBundle index_bundle = _LoadBundle(ResourceManagerConfig.kIndexFileName);
        LoadIndexFile(index_bundle.LoadAsset<TextAsset>(ResourceManagerConfig.kIndexFileName));

        return obj;
    }

    public override void Unload(string asset_path)
    {
    }

    private AssetBundle _LoadBundle(string bundle_name)
    {
        bundle_name = ResourceManagerConfig.FormatString("Bundles/{0}", bundle_name);

        string bundle_load_path = VersionManager.Instance.LocateFilePath(bundle_name);
        if (string.IsNullOrEmpty(bundle_load_path))
        {
            Debug.LogErrorFormat("get load bundle path failed 1: {0}", bundle_name);
            return null;
        }

        var bundle = AssetBundle.LoadFromFile(bundle_load_path);
        if (bundle == null)
        {
            Debug.LogErrorFormat("load bundle failed :{0}", bundle_load_path);
            return null;
        }
        return bundle;
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
}
