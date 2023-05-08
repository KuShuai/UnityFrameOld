using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text;
using System.Net;
using System.Security.Policy;

public class UpLoadABFile : Editor
{
    private static StringBuilder _stringBuilder = new StringBuilder(1024);

    [MenuItem("Bundle/AB/UpLoad")]
    static void UpLoadAllABFile()
    {
        _stringBuilder.Clear();
        string[] files = Directory.GetFiles(ResourceManagerConfig.StreamingAssetsPath, "*",SearchOption.AllDirectories);
        for (int i = 0; i < files.Length; i++)
        {
            if (files[i].EndsWith(".assetbundle"))
            {
                //上传并记录
                //string md5 = AssetBundleMD5.GetPathMd5(files[i]);
                //_stringBuilder.Append(md5);
                string fileName = files[i].Replace(ResourceManagerConfig.StreamingAssetsPath, "");
                Debug.LogError(files[i]);
                FTPUpAndDown.FtpUpLoadFile(files[i], fileName);
            }
        }
    }
}
