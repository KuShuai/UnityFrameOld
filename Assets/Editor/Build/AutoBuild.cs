using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Build;
using System;
using System.IO;
using System.Text;
using LitJson;

#if (UNITY_5 || UNITY_2017_1_OR_NEWER) && (UNITY_IOS || UNITY_TVOS)
using UnityEditor.iOS.Xcode;
#endif

public class AutoBuild
{
    public class ActiveBuildTargetChangeListener : IActiveBuildTargetChanged
    {
        public enum Op
        {
            Asset,
            Player,
        }

        public static Op kOp;

        public int callbackOrder { get { return 0; } }
        public void OnActiveBuildTargetChanged(BuildTarget previousTarget, BuildTarget newTarget)
        {
            Debug.Log("Switched build target to " + newTarget);

            if (kOp == Op.Asset)
            {
                BuildAssetBundle();
            }
            else if (kOp == Op.Player)
            {
                BuildPlayer();
            }
        }
    }

    private static string Product_Name = "TBD";
    private static string Branch_Name = "TBD";
    private static string Client_Version = "0.0.0";
    private static int Build_Number = 0;
    private static string Build_Time = "TBD";
    private static string Channel = "TBD";
    private static bool Dev_Build = false;
    private static string Macros = "";

    private static JsonData BuildConfig = null;

    public static string kUMengSymbol = @"_UMENG_";
    public static string kBSGameSDKAndroid = @"_BS_GAME_SDK_ANDROID_";
    public static string kBSGameSDKiOS = @"_BS_GAME_SDK_IOS_";

    static void ReadVersionFile()
    {
        string version_file = string.Format("{0}/../version_file.txt", Application.dataPath);
        string[] lines = File.ReadAllLines(version_file);

        Product_Name = lines[0];//产品名称
        Branch_Name = lines[1];//分支名称
        Client_Version = lines[2];//客户端版本
        Build_Number = int.Parse(lines[3]);//构建数
        Build_Time = lines[4];//构建时间
        Channel = lines[5];//通道
        Dev_Build = int.Parse(lines[6]) != 0;//发展？？？
        Macros = lines[7];//宏

        string config_path = string.Format("{0}/Configs/buildconfig.txt", Application.dataPath);
        BuildConfig = JsonMapper.ToObject(File.ReadAllText(config_path));
    }

    public static void BuildAssetBundle_Android()
    {
        ActiveBuildTargetChangeListener.kOp = ActiveBuildTargetChangeListener.Op.Asset;

        if (EditorUserBuildSettings.activeBuildTarget != BuildTarget.Android)
        {
            Debug.Log("BuildAssetBundle_Android Switch to Android");

            EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Android, BuildTarget.Android);
        }
        else
        {
            BuildAssetBundle();
        }
    }

    public static void BuildAssetBundle_iOS()
    {
        ActiveBuildTargetChangeListener.kOp = ActiveBuildTargetChangeListener.Op.Asset;

        if (EditorUserBuildSettings.activeBuildTarget != BuildTarget.iOS)
        {
            Debug.Log("BuildAssetBundle_iOS Switch to Android");

            EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.iOS, BuildTarget.iOS);
        }
        else
        {
            BuildAssetBundle();
        }
    }

    private static string ProjectPath = Application.dataPath.Substring(0, Application.dataPath.IndexOf("Assets", StringComparison.CurrentCulture));

    private static string BundleExportFolder = "Bundles";
    private static string BundleExportPath = string.Format("{0}/{1}/", ResourceManagerConfig.StreamingAssetsPath, BundleExportFolder);
    private static string BundleExportPathAux = string.Format("{0}/../BundleManifest/", Application.dataPath);
    private static string CriAudioExportPath = string.Format("{0}/CriAudio/", ResourceManagerConfig.StreamingAssetsPath);
    private static string CriVideoExportPath = string.Format("{0}/CriVideo/", ResourceManagerConfig.StreamingAssetsPath);

    private static string RESToBuildPath = "Assets/PrefabResources/";
    private static string CriAudioRESToBuildPath = "Assets/OrigResources/CriAudio/";
    private static string CriVideoRESToBuildPath = "Assets/OrigResources/CriVideo/";
    private static string UIPrefabPath = string.Format("{0}/PrefabResources/UI/", Application.dataPath);

    private static string LuaSrcPath = string.Format("{0}/../Lua/Scripts/", Application.dataPath);
    private static string LuaPrefabPath = string.Format("{0}/PrefabResources/LuaScripts/", Application.dataPath);

    private static BuildAssetBundleOptions BuildOption = BuildAssetBundleOptions.ChunkBasedCompression | BuildAssetBundleOptions.ForceRebuildAssetBundle;
    private static List<AssetBundleBuild> ToBuildList = new List<AssetBundleBuild>();

    private static StringBuilder IndexFileContent = new StringBuilder();
    private static StringBuilder VersionFileContent = new StringBuilder();

    static string GetBundleName(string path)
    {
        return ResourceManagerConfig.FormatString("{0}.assetbundle", AssetDatabase.AssetPathToGUID(path));
        //string readable_path = path.Replace(RESToBuildPath, "").Replace('/', '_').Replace('.', '_').Replace(' ', '_');
        //return ResourceManagerConfig.FormatString("{0}.assetbundle", readable_path);
    }
    [MenuItem("Auto Build/Build Bundles")]
    private static void BuildAssetBundle()
    {
        PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android, "");
        PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.iOS, "");

        Debug.Log("BuildAssetBundle !!!!!!!!!!!!!!!!!!!");

        ReadVersionFile();

        if (Directory.Exists(ResourceManagerConfig.StreamingAssetsPath))
        {
            Directory.Delete(ResourceManagerConfig.StreamingAssetsPath, true);
        }
        if (Directory.Exists(BundleExportPath))
        {
            Directory.Delete(BundleExportPath, true);
        }
        if (Directory.Exists(BundleExportPathAux))
        {
            Directory.Delete(BundleExportPathAux, true);
        }
        Directory.CreateDirectory(BundleExportPath);
        Directory.CreateDirectory(BundleExportPathAux);

        // cri
        string src_audio_assets_path = CriAudioRESToBuildPath;
        string src_viedo_assets_path = CriVideoRESToBuildPath;

        string dest_audio_assets_path = CriAudioExportPath;
        string dest_viedo_assets_path = CriVideoExportPath;

        CopyDirectory(src_audio_assets_path, dest_audio_assets_path);
        CopyDirectory(src_viedo_assets_path, dest_viedo_assets_path);

        // lua
        CopyLuaDirectory(LuaSrcPath, LuaPrefabPath);

        // bundle
        ToBuildList.Clear();

        IndexFileContent.Clear();
        VersionFileContent.Clear();

        JsonData bundle_to_gether_round = BuildConfig["bundles_to_gether_round"];

        string[] folders = Directory.GetDirectories(RESToBuildPath);
        foreach (var folder in folders)
        {
            string bundle_name = @"stringEmpty";
            bool all_in_one = false;
            if (bundle_to_gether_round != null)
            {
                for (int i = 0; i < bundle_to_gether_round.Count; ++i)
                {
                    bool match = string.Equals((string)bundle_to_gether_round[i], folder);
                    if (match)
                    {
                        all_in_one = true;
                        bundle_name = GetBundleName(folder);
                        break;
                    }
                }
            }
            string[] files = Directory.GetFiles(folder, "*", SearchOption.AllDirectories);

            if (all_in_one)
            {
                AssetBundleBuild build = new AssetBundleBuild();

                List<string> assets = new List<string>();
                List<string> address = new List<string>();

                foreach (var file in files)
                {
                    string file_ext = Path.GetExtension(file);
                    if (!CanBundle(file_ext))
                        continue;

                    string file_path_name = file.Replace(@"\", "/");

                    string addressableName = GetAddressableName(file_path_name);
                    assets.Add(file_path_name);
                    address.Add(addressableName);

                    WriteIndexFile(addressableName, bundle_name);
                }

                build.assetBundleName = bundle_name;
                build.assetNames = assets.ToArray();
                build.addressableNames = address.ToArray();

                ToBuildList.Add(build);
            }
            else
            {
                foreach (var file in files)
                {
                    string file_ext = Path.GetExtension(file);
                    if (!CanBundle(file_ext))
                        continue;

                    string file_path_name = file.Replace(@"\", "/");

                    bundle_name = GetBundleName(file_path_name);

                    AssetBundleBuild build = new AssetBundleBuild();

                    string addressableName = GetAddressableName(file_path_name);
                    build.assetBundleName = bundle_name;
                    build.assetNames = new string[] { file_path_name };
                    build.addressableNames = new string[] { addressableName };

                    WriteIndexFile(addressableName, bundle_name);

                    ToBuildList.Add(build);
                }
            }
        }
        {
            string index_file_path = string.Format("{0}{1}.txt", RESToBuildPath, ResourceManagerConfig.kIndexFileName);
            File.WriteAllText(index_file_path, IndexFileContent.ToString());
            AssetDatabase.ImportAsset(index_file_path);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            AssetBundleBuild indexbuild = new AssetBundleBuild();
            indexbuild.assetBundleName = ResourceManagerConfig.kIndexFileName;
            indexbuild.assetNames = new string[] { index_file_path
};
            indexbuild.addressableNames = new string[] { ResourceManagerConfig.kIndexFileName };
            ToBuildList.Add(indexbuild);
        }

        BuildPipeline.BuildAssetBundles(BundleExportPath, ToBuildList.ToArray(), BuildOption, EditorUserBuildSettings.activeBuildTarget);
        AssetDatabase.Refresh();

        StringBuilder versionprofile_json = new StringBuilder();
        GenVersionProfile(versionprofile_json);
        VersionFileContent.AppendLine(versionprofile_json.ToString());

        //File.WriteAllText(string.Format("{0}/{1}", ResourceManagerConfig.StreamingAssetsPath, ResourceManagerConfig.kVersionProfileName), VersionFileContent.ToString());

        string[] ab_files = Directory.GetFiles(BundleExportPath);

        foreach (var ab_file in ab_files)
        {
            if (Path.GetExtension(ab_file) == ".manifest")
            {
                string new_path = ab_file.Replace(BundleExportPath, BundleExportPathAux);
                File.Move(ab_file, new_path);
            }
            else
            {
                string md5 = GetMD5HashFromFile(ab_file);
                long size = GetFileSize(ab_file);
                WriteVersionFile(string.Format("Bundles/{0}", Path.GetFileName(ab_file)), md5, size);
            }
        }

        //音频 视频 打包
        //string[] cri_audio_files = Directory.GetFiles(dest_audio_assets_path);
        //string[] cri_video_files = Directory.GetFiles(dest_viedo_assets_path);

        //foreach (var path in cri_audio_files)
        //{
        //    string md5 = GetMD5HashFromFile(path);
        //    long size = GetFileSize(path);
        //    WriteVersionFile(string.Format("CriAudio/{0}", Path.GetFileName(path)), md5, size);
        //}

        //foreach (var path in cri_video_files)
        //{
        //    string md5 = GetMD5HashFromFile(path);
        //    long size = GetFileSize(path);
        //    WriteVersionFile(string.Format("CriVideo/{0}", Path.GetFileName(path)), md5, size);
        //}

        File.WriteAllText(string.Format("{0}/{1}", ResourceManagerConfig.StreamingAssetsPath, ResourceManagerConfig.kVersionFileName), VersionFileContent.ToString());
        File.WriteAllText(string.Format("{0}/{1}", BundleExportPathAux, ResourceManagerConfig.kIndexFileName), IndexFileContent.ToString());
    }

    private static bool CopyDirectory(string SourcePath, string DestinationPath)
    {
        bool ret = false;
        try
        {
            SourcePath = SourcePath.EndsWith(@"/", StringComparison.CurrentCulture) ? SourcePath : SourcePath + @"/";
            DestinationPath = DestinationPath.EndsWith(@"/", StringComparison.CurrentCulture) ? DestinationPath : DestinationPath + @"/";

            if (Directory.Exists(SourcePath))
            {
                if (Directory.Exists(DestinationPath) == false)
                    Directory.CreateDirectory(DestinationPath);

                foreach (string fls in Directory.GetFiles(SourcePath))
                {
                    FileInfo flinfo = new FileInfo(fls);
                    if (flinfo.Extension == ".meta")
                        continue;
                    flinfo.CopyTo(DestinationPath + flinfo.Name);
                }
                foreach (string drs in Directory.GetDirectories(SourcePath))
                {
                    DirectoryInfo drinfo = new DirectoryInfo(drs);
                    if (CopyDirectory(drs, DestinationPath + drinfo.Name) == false)
                        ret = false;
                }
            }
            ret = true;
        }
        catch (Exception ex)
        {
            ret = false;
        }
        return ret;
    }

    private static bool CopyLuaDirectory(string SourcePath, string DestinationPath)
    {
        bool ret = false;
        try
        {
            SourcePath = SourcePath.EndsWith(@"/", StringComparison.CurrentCulture) ? SourcePath : SourcePath + @"/";
            DestinationPath = DestinationPath.EndsWith(@"/", StringComparison.CurrentCulture) ? DestinationPath : DestinationPath + @"/";

            if (Directory.Exists(SourcePath))
            {
                if (Directory.Exists(DestinationPath) == false)
                    Directory.CreateDirectory(DestinationPath);

                foreach (string fls in Directory.GetFiles(SourcePath))
                {
                    FileInfo flinfo = new FileInfo(fls);
                    string file_name = Path.GetFileNameWithoutExtension(flinfo.Name);
                    flinfo.CopyTo(DestinationPath + file_name + ".lua.txt");
                }
                foreach (string drs in Directory.GetDirectories(SourcePath))
                {
                    DirectoryInfo drinfo = new DirectoryInfo(drs);
                    if (CopyLuaDirectory(drs, DestinationPath + drinfo.Name) == false)
                        ret = false;
                }
            }
            ret = true;
        }
        catch (Exception ex)
        {
            ret = false;
        }

        AssetDatabase.Refresh();

        return ret;
    }

    private static string GetAddressableName(string file_path)
    {
        string addressable_name = file_path;
        addressable_name = addressable_name.Replace(RESToBuildPath, "");
        int dot_pos = addressable_name.LastIndexOf('.');
        if (dot_pos != -1)
        {
            int count = addressable_name.Length - dot_pos;
            addressable_name = addressable_name.Remove(dot_pos, count);
        }
        return addressable_name;
    }
    private static string GetMD5HashFromFile(string fileName)
    {
        try
        {
            FileStream file = new FileStream(fileName, FileMode.Open);
            System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] retVal = md5.ComputeHash(file);
            file.Close();

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < retVal.Length; i++)
            {
                sb.Append(retVal[i].ToString("x2"));
            }
            return sb.ToString();
        }
        catch (Exception ex)
        {
            throw new Exception("GetMD5HashFromFile() fail, error:" + ex.Message);
        }
    }
    private static long GetFileSize(string fileName)
    {
        try
        {
            FileInfo fileInfo = new FileInfo(fileName);
            return fileInfo.Length;
        }
        catch (Exception ex)
        {
            throw new Exception("GetFileSize() fail, error:" + ex.Message);
        }
    }

    private static void WriteIndexFile(string key, string value)
    {
        IndexFileContent.AppendFormat("{0}:{1}", key, value);
        IndexFileContent.AppendLine();
    }

    private static void WriteVersionFile(string key, string value1, long value2)
    {
        VersionFileContent.AppendFormat("{0}:{1}:{2}", key, value1, value2);
        VersionFileContent.AppendLine();
    }

    private static JsonData GetCurrentChannel()
    {
        JsonData appchannel = BuildConfig["appchannels"];
        JsonData current_channel = appchannel["pc"]["pc"]; ;
#if UNITY_ANDROID
        current_channel = appchannel[Channel]["android"];
#elif UNITY_IOS
        current_channel = appchannel[Channel]["ios"];
#endif
        return current_channel;
    }

    private static void GenVersionProfile(StringBuilder version_json)
    {
        JsonWriter versionFileJsonWriter = new JsonWriter(version_json);
        versionFileJsonWriter.WriteObjectStart();

        versionFileJsonWriter.WritePropertyName("client_version");
        versionFileJsonWriter.Write(Client_Version);

        versionFileJsonWriter.WritePropertyName("build_number");
        versionFileJsonWriter.Write(Build_Number);

        versionFileJsonWriter.WritePropertyName("build_time");
        versionFileJsonWriter.Write(Build_Time);

        JsonData current_channel = GetCurrentChannel();
        JsonData verify_url = current_channel["verify_urls"];
        versionFileJsonWriter.WritePropertyName("verify_urls");
        versionFileJsonWriter.WriteArrayStart();
        for (int i = 0; i < verify_url.Count; ++i)
        {
            versionFileJsonWriter.Write((string)verify_url[i]);
        }
        versionFileJsonWriter.WriteArrayEnd();

        JsonData patch_urls = current_channel["patch_urls"];
        versionFileJsonWriter.WritePropertyName("patch_urls");
        versionFileJsonWriter.WriteArrayStart();
        for (int i = 0; i < patch_urls.Count; ++i)
        {
            versionFileJsonWriter.Write((string)patch_urls[i]);
        }
        versionFileJsonWriter.WriteArrayEnd();

        versionFileJsonWriter.WriteObjectEnd();
    }

    private static bool CanBundle(string ext)
    {
        if (ext == ".meta")
        {
            return false;
        }
        return true;
    }

    //private static bool BuildAllInOne(string folder_path, ref string bundle_name)
    //{
    //    if (folder_path.Contains(RESToBuildPath + ResourceManagerConfig.kFont))
    //    {
    //        bundle_name = ResourceManagerConfig.kFontBundleName;
    //        return true;
    //    }
    //    else if (folder_path.Contains(RESToBuildPath + ResourceManagerConfig.kShader))
    //    {
    //        bundle_name = ResourceManagerConfig.kShaderBundleName;
    //        return true;
    //    }
    //    else if (folder_path.Contains(RESToBuildPath + ResourceManagerConfig.kConfigs))
    //    {
    //        bundle_name = ResourceManagerConfig.kConfigsBundleName;
    //        return true;
    //    }
    //    else if (folder_path.Contains(RESToBuildPath + ResourceManagerConfig.kGestures))
    //    {
    //        bundle_name = ResourceManagerConfig.kGesturesBundleName;
    //        return true;
    //    }
    //    else if (folder_path.Contains(RESToBuildPath + ResourceManagerConfig.kLuaScripts))
    //    {
    //        bundle_name = ResourceManagerConfig.kLuaScriptsBundleName;
    //        return true;
    //    }
    //    return false;
    //}

    public static void BuildPlayer_Android()
    {
        ActiveBuildTargetChangeListener.kOp = ActiveBuildTargetChangeListener.Op.Player;

        if (EditorUserBuildSettings.activeBuildTarget != BuildTarget.Android)
        {
            Debug.Log("BuildPlayer_Android Switch to Android");
            EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Android, BuildTarget.Android);
        }
        else
        {
            BuildPlayer();
        }
    }

    public static void BuildPlayer_iOS()
    {
        ActiveBuildTargetChangeListener.kOp = ActiveBuildTargetChangeListener.Op.Player;

        if (EditorUserBuildSettings.activeBuildTarget != BuildTarget.iOS)
        {
            Debug.Log("BuildPlayer_Android Switch to iOS");
            EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.iOS, BuildTarget.iOS);
        }
        else
        {
            BuildPlayer();
        }
    }

    [MenuItem("Auto Build/Build Player")]
    static void BuildPlayer()
    {
        Debug.Log("BuildPlayer !!!!!!!!!!!!");

        // xlua gen
        //CSObjectWrapEditor.Generator.GenAll();

        EditorUserBuildSettings.androidCreateSymbolsZip = true;

        ReadVersionFile();

        string version_file = Path.Combine(Application.streamingAssetsPath, ResourceManagerConfig.kVersionFileName);
        if (!File.Exists(version_file))
        {
            VersionFileContent.Clear();
            StringBuilder versionprofile_json = new StringBuilder();
            GenVersionProfile(versionprofile_json);
            VersionFileContent.AppendLine(versionprofile_json.ToString());

            if (Directory.Exists(Application.streamingAssetsPath))
            {
                Directory.Delete(Application.streamingAssetsPath);
            }
            Directory.CreateDirectory(Application.streamingAssetsPath);

            File.WriteAllText(version_file, VersionFileContent.ToString());
        }

        // sdk paramter
        string appchannel_file = "Assets/Resources/channel_config.txt";
        JsonData current_channel = GetCurrentChannel();
        if (File.Exists(appchannel_file))
        {
            File.Delete(appchannel_file);
        }
        JsonData sdk_paramter = current_channel["sdk_paramter"];
        string appchannel_file_content = JsonMapper.ToJson(sdk_paramter);
        File.WriteAllText(appchannel_file, appchannel_file_content);

        AssetDatabase.Refresh();

        string full_version_string = string.Format("{0}.{1}", Client_Version, Build_Number);
        //PlayerSettings.bundleVersion = full_version_string;
        PlayerSettings.bundleVersion = Client_Version;
#if UNITY_ANDROID
        PlayerSettings.Android.bundleVersionCode = Build_Number;
        PlayerSettings.SetScriptingBackend(BuildTargetGroup.Android, ScriptingImplementation.IL2CPP);
        PlayerSettings.SetIl2CppCompilerConfiguration(BuildTargetGroup.Android, Il2CppCompilerConfiguration.Release);
#elif UNITY_IOS
        PlayerSettings.iOS.buildNumber = Build_Number.ToString();
        PlayerSettings.SetScriptingBackend(BuildTargetGroup.iOS, ScriptingImplementation.IL2CPP);
        PlayerSettings.SetIl2CppCompilerConfiguration(BuildTargetGroup.iOS, Il2CppCompilerConfiguration.Release);
#endif

        PlayerSettings.productName = Product_Name;

        StringBuilder macrosBuilder = new StringBuilder();

#if UNITY_ANDROID
        macrosBuilder.AppendFormat("{0};", "NOSDK");
#elif UNITY_IOS
        macrosBuilder.AppendFormat("{0};", "APPS");
#endif
        //macrosBuilder.AppendFormat("{0};", "HOTFIX_ENABLE");

        macrosBuilder.AppendFormat("{0};", "UNITY_POST_PROCESSING_STACK_V2");

        macrosBuilder.AppendFormat("{0};", BuildPacket.SymbolBugly);
        //macrosBuilder.AppendFormat("{0};", BuildPacket.SymbolGVoice);

        StringBuilder bundleIDBuilder = new StringBuilder();
        //bundleIDBuilder.Append("com.cookiegame.");
        //bundleIDBuilder.Append(Product_Name.ToLower());

        PlayerSettings.SplashScreen.show = true;
        PlayerSettings.SplashScreen.overlayOpacity = 0;
        PlayerSettings.SplashScreen.backgroundColor = Color.white;
        PlayerSettings.SplashScreen.animationMode = PlayerSettings.SplashScreen.AnimationMode.Static;
        PlayerSettings.SplashScreen.drawMode = PlayerSettings.SplashScreen.DrawMode.AllSequential;
        PlayerSettings.SplashScreen.unityLogoStyle = PlayerSettings.SplashScreen.UnityLogoStyle.LightOnDark;
        PlayerSettings.SplashScreen.showUnityLogo = false;

        string dataPath = Application.dataPath;
        string modifyPluginPath = dataPath.Replace("Assets", "PluginModify/");
        string pluginPath = dataPath + "/Plugins";
#if UNITY_ANDROID
        pluginPath += "/Android";
#elif UNITY_IOS
        pluginPath += "/iOS";
#endif

        string bundlename = (string)current_channel["bundlename"];
        string channel_macro = (string)current_channel["channel_macro"];
        string sdk_path = (string)current_channel["sdk_path"];

        bundleIDBuilder.Append(bundlename);
        macrosBuilder.AppendFormat("{0};", channel_macro);

        macrosBuilder.AppendFormat("{0}", Macros);
#if UNITY_ANDROID
        if (!string.IsNullOrEmpty(sdk_path))
        {
            BuildPacket.CopyFolder(modifyPluginPath + sdk_path, pluginPath, false);
        }
#endif
        JsonData splashes = current_channel["splashes"];
        PlayerSettings.SplashScreenLogo[] logos = new PlayerSettings.SplashScreenLogo[splashes.Count];
        for (int i = 0; i < splashes.Count; ++i)
        {
            string tex_asset_path = (string)splashes[i]["tex"];
            var duration_json = splashes[i]["duration"];
            float duration = 2.0f;
            if (duration_json.IsInt)
            {
                duration = (float)((int)duration_json);
            }
            else if (duration_json.IsDouble)
            {
                duration = (float)((double)duration_json);
            }
            PlayerSettings.SplashScreenLogo splash = PlayerSettings.SplashScreenLogo.Create(duration);
            splash.logo = AssetDatabase.LoadAssetAtPath<Sprite>(tex_asset_path);

            logos[i] = splash;
        }
        PlayerSettings.SplashScreen.logos = logos;

        string icon_asset_path = (string)current_channel["icon"];
        Texture2D icon = AssetDatabase.LoadAssetAtPath<Texture2D>(icon_asset_path);
        if (icon != null)
        {
#if UNITY_ANDROID
            Texture2D[] android_icons = PlayerSettings.GetIconsForTargetGroup(BuildTargetGroup.Android);
            for (int i = 0; i < android_icons.Length; ++i)
            {
                android_icons[i] = icon;
            }
            PlayerSettings.SetIconsForTargetGroup(BuildTargetGroup.Android, android_icons);
#elif UNITY_IOS
            Texture2D[] ios_icons = PlayerSettings.GetIconsForTargetGroup(BuildTargetGroup.iOS);
            for (int i = 0; i < ios_icons.Length; ++i)
            {
                ios_icons[i] = icon;
            }
            PlayerSettings.SetIconsForTargetGroup(BuildTargetGroup.iOS, ios_icons);
#endif
        }
        AssetDatabase.Refresh();

        string bundleId = bundleIDBuilder.ToString();
        _symbols = macrosBuilder.ToString();

        string projectPath = Application.dataPath.Replace("/Assets", "");

        List<string> scenes = new List<string>();
        foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
        {
            if (!scene.enabled)
            {
                continue;
            }

            scenes.Add(scene.path);
        }

        PlayerSettings.applicationIdentifier = bundleId;
#if UNITY_ANDROID
        SetAndroidIcon();

        PreProcAndroidPlugins(_symbols);
        PostProcAndroidPlugins(_symbols);

        ModifyMenifest();

        PlayerSettings.Android.useAPKExpansionFiles = false;
        EditorUserBuildSettings.exportAsGoogleAndroidProject = false;
        EditorUserBuildSettings.androidBuildSystem = AndroidBuildSystem.Gradle;

        string targetDir = projectPath + "/AndroidAPK";
        string targetName = string.Format("{0}_{1}_{2}.apk", PlayerSettings.productName, Branch_Name, full_version_string);

        PlayerSettings.Android.keystoreName = projectPath + "/AndroidKeystore/dc.keystore";
        PlayerSettings.Android.keystorePass = "CookieDC";
        PlayerSettings.Android.keyaliasName = "DC";
        PlayerSettings.Android.keyaliasPass = "CookieGame.cn";
        PlayerSettings.Android.splashScreenScale = AndroidSplashScreenScale.ScaleToFill;

        PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android, _symbols);

        // 创建文件夹
        if (!Directory.Exists(targetDir))
        {
            Directory.CreateDirectory(targetDir);
        }

        // 开始build
        BuildOptions option = BuildOptions.None;
        if (Dev_Build)
        {
            option |= BuildOptions.Development | BuildOptions.ConnectWithProfiler;
        }
        UnityEditor.Build.Reporting.BuildReport ret = BuildPipeline.BuildPlayer(scenes.ToArray(), targetDir + "/" + targetName, BuildTarget.Android, option);
        if (ret.summary.result != UnityEditor.Build.Reporting.BuildResult.Succeeded)
        {
            Debug.LogError("BuildPlayer failure: " + ret.summary.totalErrors);
        }
#elif UNITY_IOS
        SetIOSIcon();

        PreProcIOSPlugins(_symbols);
        PostProcIOSPlugins(_symbols);

        string targetDir = projectPath + "/IOSBuild";
        string targetName = "XCodeProject";

        PlayerSettings.iOS.appleEnableAutomaticSigning = false;
        PlayerSettings.iOS.iOSManualProvisioningProfileType = ProvisioningProfileType.Development;
        PlayerSettings.iOS.tvOSManualProvisioningProfileType = ProvisioningProfileType.Development;

        PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.iOS, _symbols);


        kMethod = "ad-hoc";
        kProvisioningProfiles = "QuQi Distribute";

        if (_symbols.Contains("_APP_STORE_"))
        {
            kMethod = "app-store";
            kProvisioningProfiles = "QuQiDCBiliAppStore";
        }

        kBundleName = bundleId;
        kCertificate = "835ACECD4156BC8BE92F25ED3978D0407A690773";
        kTeamID = "4V32Z89GQC";
        kSignIdentity = "iPhone Distribution: QuQi Entertainment(Beijing)Information Consulting Co., Ltd. (4V32Z89GQC)";

        if (_symbols.Contains(kBSGameSDKiOS))
        {
            kMethod = "development";
            kTeamID = "UL9WJVVZ42";
            kCertificate = "iPhone Developer";
            kProvisioningProfiles = "Dancing Club-Dev";
            kSignIdentity = "iPhone Developer";
        }

        Genplist(kMethod, kBundleName, kProvisioningProfiles, kCertificate, kTeamID);

        // 创建文件夹
        if (!Directory.Exists(targetDir))
        {
            Directory.CreateDirectory(targetDir);
        }

        // 开始build
        BuildOptions option = BuildOptions.AcceptExternalModificationsToPlayer;
        if (Dev_Build)
        {
            option |= BuildOptions.Development | BuildOptions.ConnectWithProfiler;
        }
        UnityEditor.Build.Reporting.BuildReport ret = BuildPipeline.BuildPlayer(scenes.ToArray(), targetDir + "/" + targetName, BuildTarget.iOS, option);
        if (ret.summary.result != UnityEditor.Build.Reporting.BuildResult.Succeeded)
        {
            Debug.LogError("BuildPlayer failure: " + ret.summary.totalErrors);
        }
#endif
    }

    static string kMethod = string.Empty;
    static string kBundleName = string.Empty;
    static string kProvisioningProfiles = string.Empty;
    static string kCertificate = string.Empty;
    static string kTeamID = string.Empty;
    static string kSignIdentity = string.Empty;

    //[MenuItem("Auto Build/Copy Asset To StreamingAsset")]
    static void CopyAssetBundle()
    {
        string dest = Application.streamingAssetsPath;
        if (Directory.Exists(dest))
        {
            Directory.Delete(dest, true);
        }
        Directory.CreateDirectory(dest);

        CopyDirectory(ResourceManagerConfig.StreamingAssetsPath, dest);
    }

    private static Dictionary<string, bool> _frameworks = new Dictionary<string, bool>();
    private static List<string> _libs = new List<string>();
    private static string _symbols = string.Empty;

    private static void AddFramework(string framework, bool weak)
    {
        if (!_frameworks.ContainsKey(framework))
        {
            _frameworks.Add(framework, weak);
        }
    }

    private static void AddLibs(string lib)
    {
        if (_libs.Contains(lib))
        {
            _libs.Add(lib);
        }
    }

#if (UNITY_5 || UNITY_2017_1_OR_NEWER) && (UNITY_IOS || UNITY_TVOS)
    [UnityEditor.Callbacks.PostProcessBuild]
    public static void OnPostprocessBuild(BuildTarget buildTarget, string path)
    {
        if (buildTarget == BuildTarget.iOS)
        {
            _frameworks.Clear();
            _libs.Clear();

            // modify project
            string projectPath = path + "/Unity-iPhone.xcodeproj/project.pbxproj";
            PBXProject project = new PBXProject();
            project.ReadFromString(File.ReadAllText(projectPath));
            //string targetGuid = project.TargetGuidByName("Unity-iPhone");
            string targetGuid = project.GetUnityMainTargetGuid();

            project.SetBuildProperty(targetGuid, "ENABLE_BITCODE", "NO");

            // release
            project.SetBuildProperty(targetGuid, "CODE_SIGN_IDENTITY", kSignIdentity);
            project.SetBuildProperty(targetGuid, "CODE_SIGN_IDENTITY[sdk=iphoneos*]", kSignIdentity);
            project.SetBuildProperty(targetGuid, "CODE_SIGN_STYLE", "Manual");
            project.SetBuildProperty(targetGuid, "PROVISIONING_PROFILE", kProvisioningProfiles);
            project.SetBuildProperty(targetGuid, "PROVISIONING_PROFILE_SPECIFIER", kProvisioningProfiles);
            project.SetBuildProperty(targetGuid, "DEVELOPMENT_TEAM", kTeamID);

            if (_symbols.Contains(BuildPacket.SymbolBugly))
            {
                AddFramework("Security.framework", false);
                AddFramework("SystemConfiguration.framework", false);
                AddFramework("JavaScriptCore.framework", true);
                AddLibs("libz.dylib");
                AddLibs("libc++.dylib");

                BuildXCodeClass unityAppController = new BuildXCodeClass(path + "/Classes/UnityAppController.mm");
                unityAppController.WriteBelow("#include \"PluginBase/AppDelegateListener.h\"", "#import <Bugly/Bugly.h>");
                unityAppController.WriteBelow("-> applicationDidFinishLaunching()", "    [Bugly startWithAppId:@\"efc934dce1\"];");
                unityAppController.WriteBack();
            }
            else
            {
                // without bugly
                BuildXCodeClass unityAppController = new BuildXCodeClass(path + "/Classes/UnityAppController.mm");
                unityAppController.Delete("#import <Bugly/Bugly.h>");
                unityAppController.Delete("    [Bugly startWithAppId:@\"efc934dce1\"];");
                unityAppController.WriteBack();
            }
            if (_symbols.Contains(BuildPacket.SymbolGVoice))
            {
                AddFramework("SystemConfiguration.framework", false);
                AddFramework("CoreTelephony.framework", false);
                AddFramework("AudioToolbox.framework", false);
                AddFramework("CoreAudio.framework", false);
                AddFramework("AVFoundation.framework", false);
                AddFramework("Security.framework", false);
                AddFramework("libstdc++.6.0.9.tbd", false);
            }
            if (_symbols.Contains(kUMengSymbol))
            {
                AddFramework("libz.dylib", false);
                AddFramework("libsqlite3.tbd", false);
                AddFramework("CoreTelephony.framework", false);
            }

            if (_symbols.Contains(kBSGameSDKiOS))
            {
                JsonData current_channel = GetCurrentChannel();
                string sdk_path = (string)current_channel["sdk_path"];
                string dataPath = Application.dataPath;
                string modifyPluginPath = dataPath.Replace("Assets", "PluginModify/");
                string bssdkpath = modifyPluginPath + sdk_path;

                //CopyFolderRecusive(bssdkpath, path);

                project.AddBuildProperty(targetGuid, "HEADER_SEARCH_PATHS", "$(SRCROOT)/../../PluginModify/" + sdk_path);
                DirectoryInfo TheFolder = new DirectoryInfo(bssdkpath);
                AddFileToProj(TheFolder, project, targetGuid, bssdkpath);
            }

            if (_symbols.Contains(kBSGameSDKiOS))
            {
                BuildXCodeClass unityAppController = new BuildXCodeClass(path + "/Classes/UnityAppController.mm");
                unityAppController.WriteBelow("#include <sys/sysctl.h>", "#import <BSGameSDK/BLDataSdk/include/DataSdkLib/BLDataSdk.h>");
                unityAppController.WriteBelow("#include <sys/sysctl.h>", "#import <BSGameSDK/BLGameSdkLib/include/BLGameSdk.h>");

                //string[] insertText = new string[] {
                //    "- (BOOL)application:(UIApplication*)application openURL: (NSURL*)url sourceApplication: (NSString*)sourceApplication annotation: (id)annotation",
                //    "{",
                //    "[[BLGameSdk defaultGameSdk] didGetToken:url];",
                //    "return YES;",
                //    "}",
                //};
                //unityAppController.WriteBefore("- (BOOL)application:(UIApplication*)app openURL:(NSURL*)url options:(NSDictionary<NSString*, id>*)options",
                //    insertText);

                unityAppController.WriteBelow("- (BOOL)application:(UIApplication*)app openURL:(NSURL*)url options:(NSDictionary<NSString*, id>*)options",
                    "[[BLGameSdk defaultGameSdk] didGetToken:url];");

                unityAppController.WriteBelow("-> applicationDidEnterBackground()", "[[BLDataSdk sharedDataSdk] applicationDidEnterBackground];");
                unityAppController.WriteBelow("-> applicationDidBecomeActive()", "[[BLDataSdk sharedDataSdk] applicationDidBecomeActive];");

                unityAppController.WriteBack();

                string linkerFlag = "OTHER_LDFLAGS";
                project.AddBuildProperty(targetGuid, linkerFlag, "-ObjC");
                project.AddBuildProperty(targetGuid, linkerFlag, "-lresolv");

                AddFramework("CoreData.framework", false);
                AddFramework("Security.framework", false);
                AddFramework("AdSupport.framework", false);
                AddFramework("StoreKit.framework", false);
                AddFramework("SystemConfiguration.framework", false);
                AddFramework("CoreGraphics.framework", false);
                AddFramework("UIKit.framework", false);
                AddFramework("Foundation.framework", false);
                // data
                AddFramework("CoreTelephony.framework", false);
                AddFramework("SystemConfiguration.framework", false);
            }

            // 把需要的framework和libs添加到工程中
            foreach (var pair in _frameworks)
            {
                project.AddFrameworkToProject(targetGuid, pair.Key, pair.Value);
            }
            foreach (var lib in _libs)
            {
                string fileGuid = project.AddFile("usr/lib/" + lib, "Frameworks/" + lib, PBXSourceTree.Sdk);
                project.AddFileToBuild(targetGuid, fileGuid);
            }

            File.WriteAllText(projectPath, project.WriteToString());

            // modify plist
            string plistPath = path + "/Info.plist";
            PlistDocument plist = new PlistDocument();
            plist.ReadFromString(File.ReadAllText(plistPath));

            PlistElementDict rootDict = plist.root;

            if (_symbols.Contains(kBSGameSDKiOS))
            {
                PlistElementArray queries_schemes = rootDict.CreateArray("LSApplicationQueriesSchemes");
                queries_schemes.AddString("bilibili.oauth.v2");

                PlistElementDict security_setting = rootDict.CreateDict("App Transport Security Settings");
                security_setting.SetBoolean("Allow Arbitrary Loads", true);

                rootDict.SetBoolean("App Uses Non-Exempt Encryption", false);

                PlistElementArray urlTypes = rootDict.CreateArray("CFBundleURLTypes");
                PlistElementDict item = urlTypes.AddDict();
                item.SetString("CFBundleTypeRole", "Editor");
                PlistElementArray urlScheme = item.CreateArray("CFBundleURLSchemes");
                urlScheme.AddString("com.bilibili.mxycjh");//bundle name
            }

            rootDict.SetString("NSMicrophoneUsageDescription", "是否允许此游戏使用麦克风？");
            rootDict.SetString("NSPhotoLibraryUsageDescription", "是否允许此游戏使用相册？");
            rootDict.SetString("NSPhotoLibraryAddUsageDescription", "是否允许此游戏保存到相册？");

            plist.WriteToFile(plistPath);
        }
    }
    static void AddFileToProj(DirectoryInfo dir, PBXProject project, string targetGuid, string bssdkpath)
    {
        foreach (FileInfo NextFile in dir.GetFiles())
        {
            if ((NextFile.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden)
                continue;

            string file = NextFile.FullName;
            string copy_path = file.Replace(bssdkpath, "");
            string ext = NextFile.Extension;

            if (ext == ".a")
            {
                project.AddBuildProperty(targetGuid, "LIBRARY_SEARCH_PATHS", NextFile.DirectoryName);
            }

            project.AddFileToBuild(targetGuid, project.AddFile(file, copy_path, PBXSourceTree.Source));
        }

        foreach (DirectoryInfo NextFolder in dir.GetDirectories())
        {
            if ((NextFolder.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden)
                continue;

            string file = NextFolder.FullName;
            string copy_path = file.Replace(bssdkpath, "");
            string ext = NextFolder.Extension;

            if (ext == ".xcdatamodeld")
            {
                string sectionGuid = project.GetSourcesBuildPhaseByTarget(targetGuid);
                project.AddFileToBuildSection(targetGuid, sectionGuid, project.AddFile(file, copy_path, PBXSourceTree.Source));
                continue;
            }
            else if (ext == ".bundle")
            {
                project.AddFileToBuild(targetGuid, project.AddFile(file, copy_path, PBXSourceTree.Source));
                continue;
            }

            AddFileToProj(NextFolder, project, targetGuid, bssdkpath);
        }
    }
#endif
    // ios plist
    //[MenuItem("AA/AA")]
    //private static void TestGenplist()
    //{
    //    Genplist("ad-hoc", "com.quqi.dc.apps", "QuQi Distribute", "835ACECD4156BC8BE92F25ED3978D0407A690773", "4V32Z89GQC");
    //}
    private static void Genplist(string method, string bundleName, string provisioningProfiles, string certificate, string teamID)
    {
        FileStream fs = new FileStream("IOSBuild/IPABash/ExportOptionsGen.plist", FileMode.Create, FileAccess.Write);
        StreamWriter w = new StreamWriter(fs, Encoding.UTF8);

        w.WriteLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
        w.WriteLine("<!DOCTYPE plist PUBLIC \"-//Apple//DTD PLIST 1.0//EN\" \"http://www.apple.com/DTDs/PropertyList-1.0.dtd\">");
        w.WriteLine("<plist version=\"1.0\">");
        w.WriteLine("<dict>");
        w.WriteLine("	<key>compileBitcode</key>");
        w.WriteLine("	<false/>");
        w.WriteLine("	<key>destination</key>");
        w.WriteLine("	<string>export</string>");
        w.WriteLine("	<key>method</key>");
        w.WriteLine("	<string>{0}</string>", method);
        w.WriteLine("	<key>provisioningProfiles</key>");
        w.WriteLine("	<dict>");
        w.WriteLine("		<key>{0}</key>", bundleName);
        w.WriteLine("		<string>{0}</string>", provisioningProfiles);
        w.WriteLine("	</dict>");
        w.WriteLine("	<key>signingCertificate</key>");
        w.WriteLine("	<string>{0}</string>", certificate);
        w.WriteLine("	<key>signingStyle</key>");
        w.WriteLine("	<string>manual</string>");
        w.WriteLine("	<key>stripSwiftSymbols</key>");
        w.WriteLine("	<true/>");
        w.WriteLine("	<key>teamID</key>");
        w.WriteLine("	<string>{0}</string>", teamID);
        w.WriteLine("	<key>thinning</key>");
        w.WriteLine("	<string>&lt;none&gt;</string>");
        w.WriteLine("</dict>");
        w.WriteLine("</plist>");

        w.Flush();
        fs.Close();
    }
    #region android
    /// <summary>
    /// 根据平台设置不同图标
    /// </summary>
    private static void SetAndroidIcon()
    {

    }

    /// <summary>
    /// 根据不同平台设置插件
    /// </summary>
    /// <param name="plat"></param>
    /// <param name="symbol"></param>
    private static void PreProcAndroidPlugins(string symbol)
    {
        string dataPath = Application.dataPath;
        string modifyPluginPath = dataPath.Replace("Assets", "PluginModify");
        string pluginPath = dataPath + "/Plugins";

        string buglyPluginPath = pluginPath + "/BuglyPlugins/Android";
        string buglyPluginScriptsPath = pluginPath + "/BuglyPlugins/Scripts";
        DeleteFolder(buglyPluginPath);
        DeleteFolder(buglyPluginScriptsPath);
        if (symbol.Contains(BuildPacket.SymbolBugly))
        {
            CopyFolder(modifyPluginPath + "/BuglyPlugins/Android", buglyPluginPath);
            CopyFolder(modifyPluginPath + "/BuglyPlugins/Scripts", buglyPluginScriptsPath);
        }

        string scriptsPath = dataPath + "/Scripts";
        string gVoicePluginPath = pluginPath + "/GVoicePlugins/Android";
        string gVoicePluginScriptsPath = scriptsPath + "/GVoicePlugins/Scripts";
        DeleteFolder(gVoicePluginPath);
        DeleteFolder(gVoicePluginScriptsPath);
        if (symbol.Contains(BuildPacket.SymbolGVoice))
        {
            CopyFolder(modifyPluginPath + "/GVoicePlugins/Android", gVoicePluginPath);
            CopyFolder(modifyPluginPath + "/GVoicePlugins/Scripts", gVoicePluginScriptsPath);
        }

        string gUMengPluginPath = pluginPath + "/UMeng/Android";
        string gUMengPluginScriptsPath = scriptsPath + "/UMeng/UmengGameAnalytics/";
        DeleteFolder(gUMengPluginPath);
        DeleteFolder(gUMengPluginScriptsPath);
        if (symbol.Contains(kUMengSymbol))
        {
            CopyFolder(modifyPluginPath + "/UMeng/Android", gUMengPluginPath);
            CopyFolder(modifyPluginPath + "/UMeng/UmengGameAnalytics", gUMengPluginScriptsPath);
        }

        AssetDatabase.Refresh();
    }

    /// <summary>
    /// 根据
    /// </summary>
    /// <param name="plat"></param>
    /// <param name="symbol"></param>
    private static void PostProcAndroidPlugins(string symbol)
    {

    }

    /// <summary>
    /// 修改menifest
    /// </summary>
    /// <param name="plat"></param>
    private static void ModifyMenifest()
    {

    }
    #endregion

    #region ios
    /// <summary>
    /// 根据平台设置不同图标
    /// </summary>
    private static void SetIOSIcon()
    {

    }

    /// <summary>
    /// 根据不同平台设置插件
    /// </summary>
    /// <param name="plat"></param>
    /// <param name="symbol"></param>
    private static void PreProcIOSPlugins(string symbol)
    {
        string dataPath = Application.dataPath;
        string modifyPluginPath = dataPath.Replace("Assets", "PluginModify");
        string pluginPath = dataPath + "/Plugins";
        string scriptsPath = dataPath + "/Scripts";

        string buglyPluginPath = pluginPath + "/BuglyPlugins/iOS";
        string buglyPluginScriptsPath = pluginPath + "/BuglyPlugins/Scripts";
        DeleteFolder(buglyPluginPath);
        DeleteFolder(buglyPluginScriptsPath);

        string gVoicePluginsPath = pluginPath + "/GVoicePlugins/iOS";
        string gVoicePluginScriptsPath = scriptsPath + "/SDK/GVoiceScripts";
        DeleteFolder(gVoicePluginsPath);
        DeleteFolder(gVoicePluginScriptsPath);

        if (symbol.Contains(BuildPacket.SymbolBugly))
        {
            CopyFolder(modifyPluginPath + "/BuglyPlugins/iOS", buglyPluginPath);
            CopyFolder(modifyPluginPath + "/BuglyPlugins/Scripts", buglyPluginScriptsPath);
        }

        if (symbol.Contains(BuildPacket.SymbolGVoice))
        {
            CopyFolder(modifyPluginPath + "/GVoicePlugins/iOS", gVoicePluginsPath);
            CopyFolder(modifyPluginPath + "/GVoicePlugins/Scripts", gVoicePluginScriptsPath);
        }

        string gUMengPluginPath = pluginPath + "/UMeng/iOS";
        string gUMengPluginScriptsPath = scriptsPath + "/UMeng/UmengGameAnalytics";
        DeleteFolder(gUMengPluginPath);
        DeleteFolder(gUMengPluginScriptsPath);
        if (symbol.Contains(kUMengSymbol))
        {
            CopyFolder(modifyPluginPath + "/UMeng/iOS", gUMengPluginPath);
            CopyFolder(modifyPluginPath + "/UMeng/UmengGameAnalytics", gUMengPluginScriptsPath);
        }

        AssetDatabase.Refresh();
    }

    /// <summary>
    /// 根据
    /// </summary>
    /// <param name="plat"></param>
    /// <param name="symbol"></param>
    private static void PostProcIOSPlugins(string symbol)
    {

    }
    #endregion

    #region util
    /// <summary>
    /// 删除目录
    /// </summary>
    private static void DeleteFolder(string path)
    {
        if (Directory.Exists(path))
        {
            Directory.Delete(path, true);
        }
    }

    /// <summary>
    /// 删除文件
    /// </summary>
    /// <param name="file"></param>
    private static void DeleteFile(string file)
    {
        if (File.Exists(file))
        {
            File.Delete(file);
        }
    }

    /// <summary>
    /// 拷贝目录
    /// </summary>
    /// <param name="source"></param>
    /// <param name="target"></param>
    public static void CopyFolder(string source, string target, bool deleteTarget = true)
    {
        if (!Directory.Exists(source))
        {
            Debug.LogErrorFormat("拷贝的目录不存在 {0}", source);
            return;
        }

        if (deleteTarget)
        {
            DeleteFolder(target);
            Directory.CreateDirectory(target);
        }

        CopyFolderRecusive(source, target);
    }

    /// <summary>
    /// 实际复制操作
    /// </summary>
    /// <param name="source"></param>
    /// <param name="target"></param>
    private static void CopyFolderRecusive(string source, string target)
    {
        //如果目标文件夹中没有源文件夹则在目标文件夹中创建源文件夹
        if (!Directory.Exists(target))
        {
            Directory.CreateDirectory(target);
        }

        string[] files = Directory.GetFiles(source);
        for (int n = 0; n < files.Length; ++n)
        {
            string sourceFile = files[n];
            string fileName = Path.GetFileName(sourceFile);
            string targetFile = target + "/" + fileName;
            File.Copy(sourceFile, targetFile, true);
        }

        //创建DirectoryInfo实例
        DirectoryInfo dirInfo = new DirectoryInfo(source);
        //取得源文件夹下的所有子文件夹名称
        DirectoryInfo[] directorys = dirInfo.GetDirectories();
        for (int j = 0; j < directorys.Length; j++)
        {
            //获取所有子文件夹名
            string childDir = source + "/" + directorys[j].Name;
            //把得到的子文件夹当成新的源文件夹，从头开始新一轮的拷贝
            CopyFolderRecusive(childDir, target + "/" + directorys[j].Name);
        }
    }
    #endregion
}
