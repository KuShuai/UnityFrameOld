using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class MyAssetBundle : Editor
{
    [MenuItem("Bundle/MyBuildUi")]
    static void MyABundleUI(){
        string BundleExportPath = Application.dataPath + "/../MyBundle";
        string BundlePath = "Assets/PrefabResources";

        if (Directory.Exists(BundleExportPath))
        {
            Directory.Delete(BundleExportPath, true);
        }
        Directory.CreateDirectory(BundleExportPath);

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

            for (int n = 0; n < files.Length; n++)
            {
                if (Path.GetExtension(files[n]) == ".meta")
                {
                    continue;
                }
                string assetName = Path.GetFileNameWithoutExtension(files[n]);
                Debug.Log(files[n]);
                assetNames.Add(files[n]);
                addressableNames.Add(assetName);

            }

            build.assetBundleName = dirValue[dirValue.Length - 1] + "/"+ dirValue[dirValue.Length - 1] + ".assetbundle";
            build.assetNames= assetNames.ToArray();
            build.addressableNames = addressableNames.ToArray();

            builds.Add(build);
        }

        BuildPipeline.BuildAssetBundles(BundleExportPath, builds.ToArray(), BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows64);

    }
}
