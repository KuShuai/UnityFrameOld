using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.AddressableAssets.Settings;
using UnityEngine;

public class MyEditor : Editor
{
    private static string productsDir = $"{Application.dataPath}/PrefabResources";

    private static AddressableAssetSettings setting = AssetDatabase.LoadAssetAtPath<AddressableAssetSettings>("Assets/AddressableAssetsData/AddressableAssetSettings.asset");

    [MenuItem("SetAddressablesGroups/ResetAll")]
    public static void SetAll()
    {
        SetAddGroups();
    }

    [MenuItem("SetAddressablesGroups/ClearAndBuild")]
    public static void ClearAndBuild()
    {
        AddressableAssetSettings.CleanPlayerContent();
        AddressableAssetSettings.BuildPlayerContent();
    }

    [MenuItem("SetAddressablesGroups/Build Addressable And Player")]
    public static void BuildAddressablesAndPlayer()
    {
        ClearAndBuild();

        var options = new BuildPlayerOptions();
        BuildPlayerOptions playerSettings = BuildPlayerWindow.DefaultBuildMethods.GetBuildPlayerOptions(options);

        BuildPipeline.BuildPlayer(playerSettings);
    }

    private static void SetAddGroups()
    {
        DirectoryInfo dirInfo = new DirectoryInfo(productsDir);

        foreach (var item in dirInfo.GetDirectories())
        {
            CreateGroup(item);
        }
        string[] allAssets = FindAllSchemaInGroupPath();
        for (int i = 0; i < allAssets.Length; i++)
        {
            SetAddressableAssetGroupSchema(allAssets[i]);
            Debug.LogWarningFormat("add assets {0} success.", allAssets[i]);
        }
    }

    private static void CreateGroup(DirectoryInfo dirInfo)
    {
        string groupName = dirInfo.Name;
        AddressableAssetGroup group = FindGroup(groupName);
        if (group == null)
        {
            setting.CreateGroup(groupName, false, false, true, setting.DefaultGroup.Schemas);
        }
        else
        {
            Debug.LogWarningFormat("Dir {0} Group is Exist!", groupName);
        }
    }

    private static AddressableAssetGroup FindGroup(string groupName)
    {
        return setting.FindGroup(groupName);
    }

    static List<string> allAssets;
    private static string[] FindAllSchemaInGroupPath()
    {
        if (allAssets == null)
        {
            allAssets = new List<string>();
        }
        allAssets.Clear();

        GetAssets();

        return allAssets.ToArray();
    }
    private static void GetAssets(string path = "")
    {
        string dir = productsDir;
        if (!string.IsNullOrEmpty(path))
        {
            dir += path;
        }
        DirectoryInfo dirInfo = new DirectoryInfo(dir);
        foreach (var item in dirInfo.GetDirectories())
        {
            if (!string.IsNullOrEmpty(item.Name))
            {
                GetAssets(path + "/" + item.Name);
            }
        }
        foreach (var item in dirInfo.GetFiles("*", SearchOption.TopDirectoryOnly))
        {
            if (Path.GetExtension(item.Name) != ".meta")
            {
                allAssets.Add(path + "/" + item.Name);
            }
        }
    }
    private static void SetAddressableAssetGroupSchema(string assetPath)
    {
        if (assetPath.StartsWith("/"))
            assetPath = assetPath.Remove(0, 1);

        string[] infos = assetPath.Split('/');

        SetAddressableAssetGroupSchema(infos[0], assetPath);
    }
    private static void SetAddressableAssetGroupSchema(string groupName, string assetPath)
    {
        AddressableAssetGroup group = FindGroup(groupName);
        if (!assetPath.StartsWith("/"))
            assetPath = "/" + assetPath;
        string guid = AssetDatabase.AssetPathToGUID("Assets/AAResources" + assetPath);
        if (group != null)
        {
            AddressableAssetEntry entry = setting.CreateOrMoveEntry(guid, group, false, false);
            if (entry != null)
            {
                string ext = assetPath.Split('.')[assetPath.Split('.').Length - 1];
                entry.address = assetPath.Remove(0, 1).Replace("." + ext, "");// Path.GetFileNameWithoutExtension(productsDir + assetPath);
                entry.SetLabel(groupName, true, false, false);

            }
        }
    }
}
