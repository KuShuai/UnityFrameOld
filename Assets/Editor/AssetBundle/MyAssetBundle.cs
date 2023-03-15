using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

public class MyAssetBundle : Editor
{
    private static StringBuilder IndexFileContent = new StringBuilder();
    [MenuItem("Bundle/My/Clear")]
    static void ClearBundle()
    {
        string BundleExportPath = Application.dataPath + "/Bundles";

        if (Directory.Exists(BundleExportPath))
        {
            Directory.Delete(BundleExportPath, true);
        }
        AssetDatabase.Refresh();
    }


    [MenuItem("Bundle/My/Build")]
    static void MyABundleUI(){

        IndexFileContent.Clear();

        string BundleExportPath = Application.dataPath + "/Bundles";
        string BundlePath = "Assets/PrefabResources";
        
        string LuaSrcPath = string.Format("{0}/../Lua/Scripts/", Application.dataPath);
        string LuaPrefabPath = string.Format("{0}/PrefabResources/LuaScripts/", Application.dataPath);

        if (Directory.Exists(BundleExportPath))
        {
            Directory.Delete(BundleExportPath, true);
        }
        Directory.CreateDirectory(BundleExportPath);


        // lua
        CopyLuaDirectory(LuaSrcPath, LuaPrefabPath);

        List<AssetBundleBuild> builds = new List<AssetBundleBuild>();

        string[] allDir = Directory.GetDirectories(BundlePath, "*");
        for (int i = 0; i < allDir.Length; i++)
        {
            string[] files = Directory.GetFiles(allDir[i], "*");
            if (files.Length == 0)
            {
                continue;
            }
            string[] dirValue = allDir[i].Split('\\');
            AssetBundleBuild build = new AssetBundleBuild();
            List<string> assetNames = new List<string>();
            List<string> addressableNames = new List<string>();

            build.assetBundleName = dirValue[dirValue.Length - 1] + "/" + dirValue[dirValue.Length - 1] + ".assetbundle";
            Debug.Log("==============================================");
            //Debug.Log("assetBundleName add:" + dirValue[dirValue.Length - 1] + "/" + dirValue[dirValue.Length - 1] + ".assetbundle");
            for (int n = 0; n < files.Length; n++)
            {
                if (Path.GetExtension(files[n]) == ".meta")
                {
                    continue;
                }
                string addressableName = GetAddressableName(files[n]);

                assetNames.Add(files[n]);
                Debug.LogError("assetNames:" + files[n]);
                addressableNames.Add(addressableName);
                Debug.LogError("addressableName:" + addressableName);
                
                IndexFileContent.AppendFormat("{0}:{1}", addressableName, build.assetBundleName);
                IndexFileContent.AppendLine();
            }

            build.assetNames= assetNames.ToArray();
            build.addressableNames = addressableNames.ToArray();

            builds.Add(build);
        }

        {
            string index_file_path = string.Format("{0}{1}.txt", "Assets/PrefabResources/", ResourceManagerConfig.kIndexFileName);
            File.WriteAllText(index_file_path, IndexFileContent.ToString());

            AssetBundleBuild indexbuild = new AssetBundleBuild();
            indexbuild.assetBundleName = ResourceManagerConfig.kIndexFileName;
            indexbuild.assetNames = new string[] { index_file_path };
            indexbuild.addressableNames = new string[] { ResourceManagerConfig.kIndexFileName };

            builds.Add(indexbuild);
        }
        BuildPipeline.BuildAssetBundles(BundleExportPath, builds.ToArray(), BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows64);

        Debug.Log("build success count " + builds.Count);
    }

    private static string GetAddressableName(string file_path)
    {
        string addressable_name = file_path;
        addressable_name = addressable_name.Replace("Assets/PrefabResources\\", "").Replace('\\', '/');
        int dot_pos = addressable_name.LastIndexOf('.');
        if (dot_pos != -1)
        {
            int count = addressable_name.Length - dot_pos;
            addressable_name = addressable_name.Remove(dot_pos, count);
        }
        return addressable_name;
    }

    private static bool CopyLuaDirectory(string SourcePath,string DestinationPath)
    {
        bool ret = false;

        try
        {
            SourcePath = SourcePath.EndsWith(@"/", System.StringComparison.CurrentCulture) ? SourcePath : SourcePath + @"/";
            DestinationPath = DestinationPath.EndsWith(@"/", System.StringComparison.CurrentCulture) ? DestinationPath : DestinationPath + @"/";

            if (Directory.Exists(SourcePath))
            {
                if (Directory.Exists(DestinationPath) == true)
                {
                    Directory.Delete(DestinationPath, true);
                }
                Directory.CreateDirectory(DestinationPath);

                foreach (var item in Directory.GetFiles(SourcePath))
                {
                    FileInfo fileInfo = new FileInfo(item);
                    string file_name = Path.GetFileNameWithoutExtension(fileInfo.Name);
                    fileInfo.CopyTo(DestinationPath + file_name + ".lua.txt");
                }
                foreach (string item in Directory.GetDirectories(SourcePath))
                {
                    DirectoryInfo dirInfo = new DirectoryInfo(item);
                    if (CopyLuaDirectory(item, DestinationPath + dirInfo.Name) == false)
                    {
                        ret = false;
                    }
                }
                ret = true;
            }
        }
        catch (System.Exception ex)
        {
            ret = false;
        }

        AssetDatabase.Refresh();
        return ret;
    }
}
