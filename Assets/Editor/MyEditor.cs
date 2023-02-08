using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.AddressableAssets.Settings;
using UnityEngine;

public class MyEditor : Editor
{
    private static AddressableAssetSettings setting = AssetDatabase.LoadAssetAtPath<AddressableAssetSettings>("Assets/AddressableAssetsData/AddressableAssetSettings.asset");

    [MenuItem("工具/创建AA分组")]
    public static void UpdateAAGroups()
    {
        string productsDir = $"{Application.dataPath}/";
        DirectoryInfo dirInfo = new DirectoryInfo(productsDir);

        foreach (var item in dirInfo.GetDirectories())
        {
            CreateGroup(item );   
        }
    }

    private static void CreateGroup(DirectoryInfo dirInfo)
    {
        string groupName = dirInfo.Name;
        AddressableAssetGroup group = FindGroup(groupName);
        if (group == null)
        {
            //创建group
            group = setting.CreateGroup(groupName, false, false, true, new List<AddressableAssetGroupSchema>());
        }

        //添加更新策略
        //添加打包策略
        //将指定目录下的文件和子目录添加到分组当中
        
    }

    private static AddressableAssetGroup FindGroup(string groupName)
    {
        return null;
    }
}
