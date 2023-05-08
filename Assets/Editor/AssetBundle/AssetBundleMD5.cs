using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Security.Cryptography;
using System.Text;

public class AssetBundleMD5 : Editor
{
    /// <summary>
    /// 构建的assetbundle
    /// </summary>
    private static string BundleExportFolder = "BundlesMD5";

    /// <summary>
    /// pc、ios可读写 android 只读
    /// </summary>
    private static string BundleExportPath = string.Format("{0}/{1}", ResourceManagerConfig.StreamingAssetsPath, BundleExportFolder);


    [MenuItem("BBB/Md5Bundle")]
    static void Bundle()
    {
        //获取选中的物体
        GameObject target = Selection.activeGameObject;
        string BundlePath = Application.dataPath+ "/PrefabResources/UIPrefabs/UIPanel1.prefab";

        
        string info = GetPathMd5(BundlePath);

        
        string md5_file_path = string.Format("{0}{1}.txt", "Assets/PrefabResources/","AAA");
        File.WriteAllText(md5_file_path, info);
        AssetDatabase.Refresh();
        //file.Close();
    }
    private static MD5 md5 = new MD5CryptoServiceProvider();
    private static string GetBundleName(string path)
    {
        //return ResourceManagerConfig.FormatString("{0}.assetbundle", AssetDatabase.AssetPathToGUID(path));
        //MD5 加密
        byte[] md5Byte = md5.ComputeHash(Encoding.Default.GetBytes(path));
        string str = GetMD5(md5Byte) + ".assetbundle";
        return str;
    }
    private static string GetBundleName(FileStream path)
    {
        //return ResourceManagerConfig.FormatString("{0}.assetbundle", AssetDatabase.AssetPathToGUID(path));
        //MD5 加密
        byte[] md5Byte = md5.ComputeHash(path);
        string str = GetMD5(md5Byte) + ".assetbundle";
        return str;
    }
    public static string GetPathMd5(string path)
    {
        //return ResourceManagerConfig.FormatString("{0}.assetbundle", AssetDatabase.AssetPathToGUID(path));
        //MD5 加密
        FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
        byte[] md5Byte = md5.ComputeHash(fs);
        fs.Close();
        string str = GetMD5(md5Byte) + ".assetbundle";
        return str;
    }

    public static string GetMD5(byte[] retVal)
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < retVal.Length; i++)
        {
            sb.Append(retVal[i].ToString("x2"));//转化成小写的16进制 输出俩位，不足的补0
        }
        return sb.ToString();
    }


}
