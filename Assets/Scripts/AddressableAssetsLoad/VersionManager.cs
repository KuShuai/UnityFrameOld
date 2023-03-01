using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class VersionManager : MonoSingleton<VersionManager>,IMonoSingleton
{
    //标记使用本地资源
    private bool _check_version;

    public void SingletonInit()
    {
        Debug.Log("VersionManager Init");

#if UNITY_EDITOR
        if (!Directory.Exists(ResourceManagerConfig.PersistentDataPath))
        {
            Directory.CreateDirectory(ResourceManagerConfig.PersistentDataPath);
        }
        if (!Directory.Exists(ResourceManagerConfig.StreamingAssetsPath))
        {
            Directory.CreateDirectory(ResourceManagerConfig.StreamingAssetsPath);
        }
        if (Directory.Exists(ResourceManagerConfig.TemporaryCachePath))
        {
            Directory.Delete(ResourceManagerConfig.TemporaryCachePath, true);
        }
        Directory.CreateDirectory(ResourceManagerConfig.TemporaryCachePath);
#endif
        if (Directory.Exists(ResourceManagerConfig.AssetDownloadPath))
        {
            Directory.Delete(ResourceManagerConfig.AssetDownloadPath,true);
        }
        Directory.CreateDirectory(ResourceManagerConfig.AssetDownloadPath);

        PreformAppVersion();
    }

    private void PreformAppVersion()
    {
#if UNITY_EDITOR
        bool develop = UnityEditor.EditorPrefs.GetBool("Develop", true);
        bool deploy_AA = UnityEditor.EditorPrefs.GetBool("Deploy_AA", false);
        bool deploy_AB = UnityEditor.EditorPrefs.GetBool("Deploy_AB", false);
        if (develop)
        {
            _check_version = true;
        }
        else if (deploy_AA)
        {
            //使用Addressable
            SearchVersionFile();
        }
        else if (deploy_AB)
        {
            //使用AssetsBound
        }
#else
#endif
    }
    private void SearchVersionFile()
    {
        string versionData = null;
#if UNITY_EDITOR //|| UNITY_IOS
        //从Bundle文件中读取版本信息----只读
        if (File.Exists(ResourceManagerConfig.kVersionFileInnerPath))
        {
            versionData = File.ReadAllText(ResourceManagerConfig.kVersionFileInnerPath);

            Debug.LogFormat("read version data {0}", ResourceManagerConfig.kVersionFileInnerPath);
        }
#endif
        if (string.IsNullOrEmpty(versionData))
        {
            _check_version = false;
            return;
        }
        else
        {
            //kVersionFileExtPath本地版本文件 -----可读写
            if (File.Exists(ResourceManagerConfig.kVersionFileExtPath))
            {
                string ext_version_file = File.ReadAllText(ResourceManagerConfig.kVersionFileExtPath);

                int inner_v1;
                int inner_v2;
                int inner_v3;
                int inner_v4;
                int ext_v1;
                int ext_v2;
                int ext_v3;
                int ext_v4;
                ReadVersion(versionData, out inner_v1, out inner_v2, out inner_v3, out inner_v4);
                ReadVersion(ext_version_file, out ext_v1, out ext_v2, out ext_v3, out ext_v4);

                //当版本号的前俩位不同时、或者第三位为0第四位不同时替换版本内容
                if (inner_v1 != ext_v1 || inner_v2 != ext_v2)
                {
                    // todo delete update files
                    File.WriteAllText(ResourceManagerConfig.kVersionFileExtPath, versionData);

                    Debug.Log("cp version to persistent data path");
                }
                else if (inner_v1 == ext_v1 && inner_v2 == ext_v2)
                {
                    if (ext_v3 == 0 && inner_v4 != ext_v4)
                    {
                        File.WriteAllText(ResourceManagerConfig.kVersionFileExtPath, versionData);

                        Debug.Log("cp version to persistent data path");
                    }
                }
            }
            else
            {
                File.WriteAllText(ResourceManagerConfig.kVersionFileExtPath, versionData);
                Debug.Log("cp version to persistent data path");
            }
        }

        if (File.Exists(ResourceManagerConfig.kVersionFileExtPath))
        {
            if (!ReadVersionFile(ResourceManagerConfig.kVersionFileExtPath,_local_version_file))
            {
                Debug.LogError("canot load version file");
                _check_version = false;
            }
            else
            {
                _check_version = true;
            }
        }
    }

    private void ReadVersion(string version_file_string, out int v1, out int v2, out int v3, out int v4)
    {
        string[] version_file_lines = version_file_string.Split('\n');

        LitJson.JsonData version_json = LitJson.JsonMapper.ToObject(version_file_lines[0]);

        string version = (string)version_json["client_version"];
        string[] versions = version.Split('.');
        v1 = int.Parse(versions[0]);
        v2 = int.Parse(versions[1]);
        v3 = int.Parse(versions[2]);
        v4 = (int)version_json["build_number"];
    }

    class FileLocate
    {
        public bool EXT;
    }
    class VersionFile
    {
        public string AppVersion;
        public int BuildVersion;

        public Dictionary<int, FileLocate> FileInfo = new Dictionary<int, FileLocate>();

        public VersionFile()
        {
            Reset();
        }

        public void Reset()
        {
            AppVersion = "0";
            BuildVersion = 0;
            FileInfo.Clear();
        }
    }

    //本地版本文件
    private VersionFile _local_version_file = new VersionFile();

    public string AppVersion
    {
        get
        {
#if UNITY_EDITOR
            bool develop = UnityEditor.EditorPrefs.GetBool("Develop", true);
            bool deploy_AA = UnityEditor.EditorPrefs.GetBool("Deploy_AA", false);
            bool deploy_AB = UnityEditor.EditorPrefs.GetBool("Deploy_AB", false);
            if (develop)
            {
                return "Editor AssetDataBase";
            }
            else if (deploy_AB)
            {
                return ResourceManagerConfig.FormatString("{0}.{1}", _local_version_file.AppVersion, _local_version_file.BuildVersion);
            }
            else if (deploy_AA)
            {
                return ResourceManagerConfig.FormatString("{0}.{1}", _local_version_file.AppVersion, _local_version_file.BuildVersion);
            }
#endif
            return ResourceManagerConfig.FormatString("{0}.{1}", _local_version_file.AppVersion, _local_version_file.BuildVersion);
        }
    }


    public string LocateFilePath(string file_name)
    {
        FileLocate file_info;
        if (_local_version_file.FileInfo.TryGetValue(file_name.GetHashCode(),out file_info))
        {
            return ResourceManagerConfig.FormatString("{0}/{1}", !file_info.EXT ? ResourceManagerConfig.kAssetLoadInnerPath : ResourceManagerConfig.kAssetLoadExtPath, file_name);
        }
        return file_name;
    }
}
