using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class AssetBundleBuilder : Editor
{
    private static string _resPath = "Assets/PrefabResources";
    private static string _bundlePath = Application.streamingAssetsPath;

    //private static BuildAssetBundleOptions _buildOption = BuildAssetBundleOptions.ChunkBasedCompression | BuildAssetBundleOptions.ForceRebuildAssetBundle;

    [MenuItem("Bundle/BuildUI")]
    static void BundleUI()
    {
        BuildUI();
    }

    private static void GenratrParams()
    {
        _bundlePath = Application.streamingAssetsPath + "_Win";
    }

    private static void BuildUI()
    {
        GenratrParams();
        List<AssetBundleBuild> builds = new List<AssetBundleBuild>();
        //builds.AddRange(GetBuildMap("Font", true));
        builds.AddRange(GetBuildMap("UIPrefabs", false));

        //BuildPipeline.BuildAssetBundles(_bundlePath, builds.ToArray(), _buildOption, EditorUserBuildSettings.activeBuildTarget);
        BuildPipeline.BuildAssetBundles(_bundlePath, builds.ToArray(), BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows64);
    }

    private static AssetBundleBuild[] GetBuildMap(string folder,bool single)
    {
        Directory.GetDirectories(_resPath + "/" + folder);
        string[] files = Directory.GetFiles(_resPath + "/" + folder);
        string file = "";

        List<AssetBundleBuild> builds = new List<AssetBundleBuild>();
        if (single)
        {
            AssetBundleBuild build = new AssetBundleBuild();

            List<string> assets = new List<string>();
            List<string> address = new List<string>();
            for (int n = 0; n < files.Length; n++)
            {
                file = files[n];
                string ext = Path.GetExtension(file);
                if (!CanBundle(ext))
                {
                    continue;
                }
                Debug.Log("get buidl file single " + file);

                assets.Add(file);
                address.Add(Path.GetFileNameWithoutExtension(file));
            }
            build.assetBundleName = folder + "/" + folder + ".assetbundle";
            build.assetNames = assets.ToArray();
            build.addressableNames = address.ToArray();

            builds.Add(build);
        }
        else
        {
            for (int n = 0; n < files.Length; n++)
            {
                AssetBundleBuild build = new AssetBundleBuild();

                file = files[n];
                string ext = Path.GetExtension(file);

                if (!CanBundle(ext))
                {

                    continue;
                }
                string assetName = Path.GetFileNameWithoutExtension(file);
                Debug.Log("==============");
                Debug.Log(folder + "/" + assetName + ".assetbundle");
                Debug.Log(file);
                Debug.Log(assetName);
                build.assetBundleName = folder + "/" + assetName + ".assetbundle";
                build.assetNames = new string[] { file };
                build.addressableNames = new string[] { assetName };

                builds.Add(build);
            }
        }
        return builds.ToArray();
    }

    private static bool CanBundle(string ext)
    {
        if (ext == ".meta")
        {
            return false;
        }
        return true;
    }

}
