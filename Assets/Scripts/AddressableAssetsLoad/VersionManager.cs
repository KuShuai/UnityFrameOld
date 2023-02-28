using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VersionManager : MonoSingleton<VersionManager>, IMonoSingleton
{
    public void SingletonInit()
    {
    }

    class FileLocate
    {
        public bool EXT;
    }
    class VersionFile
    {
        public Dictionary<int, FileLocate> FileInfo = new Dictionary<int, FileLocate>();

        public VersionFile()
        {
            Reset();
        }

        public void Reset()
        {
            FileInfo.Clear();
        }
    }

    //本地版本文件
    private VersionFile _local_version_file = new VersionFile();


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
