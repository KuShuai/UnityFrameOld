using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class DevelopResourceManager : ResourceManager
{
    private Dictionary<string, UnityEngine.Object> _resources = new Dictionary<string, UnityEngine.Object>();

    public override void Init()
    {
        base.Init();
    }

    private string GetAssetDataFileName(string asset_path)
    {
        string result = string.Empty;
        int fs_pos = asset_path.LastIndexOf("/");
        if (fs_pos == -1)
            return result;

        string asset_folder = asset_path.Remove(fs_pos, asset_path.Length - fs_pos);
        string asset_name = asset_path.Substring(fs_pos + 1, asset_path.Length - fs_pos - 1);

        string search_path = string.Format("Assets/PrefabResources/{0}/", asset_folder);

        if (!Directory.Exists(search_path))
            return result;

        string[] files = Directory.GetFiles(search_path, string.Format("{0}*.*", asset_name), SearchOption.TopDirectoryOnly);

        foreach (var file_name in files)
        {
            string name = Path.GetFileNameWithoutExtension(file_name);
            if (asset_name == name)
            {
                string ext = Path.GetExtension(file_name);
                return string.Format("Assets/PrefabResources/{0}{1}", asset_path, ext);
            }
        }

        return result;
    }

    public override Object Load(string asset_path)
    {
        if (string.IsNullOrEmpty(asset_path))
            return null;

        UnityEngine.Object obj = null;

        if (_resources.TryGetValue(asset_path,out obj))
        {
            if (obj == null)
            {
                Debug.LogWarningFormat("������Դ�����ٵ���û���б����Ƴ� {0}", asset_path);
                _resources.Remove(asset_path);
            }
            else
            {
                return obj;
            }
        }


        string full_path = GetAssetDataFileName(asset_path);
        if (full_path == null)
        {
            Debug.LogErrorFormat("get assetdatabase name failed : {0}", asset_path);
            return null;
        }
#if UNITY_EDITOR
        obj = AssetDatabase.LoadAssetAtPath(full_path, typeof(UnityEngine.Object));
#else

#endif
        if (obj == null)
        {
            Debug.LogErrorFormat("load asset failed from assetdatabase : {0}", full_path);
            return null;
        }

        _resources.Add(asset_path, obj);
        return obj;
    }

    public override void Unload(string asset_path)
    {
        base.Unload(asset_path);
        Object obj = null;
        if (_resources.TryGetValue(asset_path,out obj))
        {
            _resources.Remove(asset_path);
        }
    }

    public override bool LoadLuaScript(string asset_name, out byte[] content)
    {
        string full_path = ResourceManagerConfig.FormatString("{0}/../Lua/Scripts/{1}.lua", Application.dataPath, asset_name);
        if (File.Exists(full_path))
        {
            content = File.ReadAllBytes(full_path);
            return true;
        }
        content = null;
        return false;
    }
}
