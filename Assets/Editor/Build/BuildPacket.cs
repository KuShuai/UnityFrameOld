using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using UnityEditor.AnimatedValues;
using System.Xml.Linq;
using System;
using System.IO;

/// <summary>
/// 需要打包的场景信息
/// </summary>
public class BuildSceneInfo {
    public string Path;

    private bool _enable;
    public bool Enable {
        set {
            if(_enable != value) {
                EditorBuildSettingsScene[] scenes = EditorBuildSettings.scenes;
                foreach(var scene in scenes) {
                    if(scene.path == Path) {
                        scene.enabled = value;
                    }
                }
                EditorBuildSettings.scenes = scenes;
            }

            _enable = value;
        }
        get {
            return _enable;
        }
    }
}

/// <summary>
/// 打包平台信息
/// </summary>
public class BuildPlatInfo {
    public string Plat;
    public string Channel;
    public string BundleId;
    public bool Enable;
}

/// <summary>
/// GUI
/// </summary>
class BuildGUI : EditorWindow {
    private static BuildTarget _target;
    public static string ProductName = "DC";
    public static string Version = "1.0.0";
    public static bool ShowFPS = false;
    public static bool Debug = false;
    public static bool ShowLog = true;         // 是否输出Log

    public static bool SignRelease = false;     // ios是否使用release签名
    public static bool MakeIPA = false;         // ios是否直接生成ipa

    public static bool Project = false;         // android是否打包成成工程
    public static bool Split = false;           // android在传google play时要分包
    
    public static int BundleVersion = 1;        // ios如果版本号相同，则bundleversion必须比原来的大

    public static string BundleIdExt;             // 包名扩展

    public static string Symbols;

    private static List<BuildSceneInfo> _scenes = new List<BuildSceneInfo>();
    public static List<BuildPlatInfo> _infos = new List<BuildPlatInfo>();

    private static EditorWindow _window;
    private AnimBool _showScenes;
    private AnimBool _showPlats;

    public static void Init() {
        _target = EditorUserBuildSettings.activeBuildTarget;

        LoadConfig();

        _scenes.Clear();
        foreach(var scene in EditorBuildSettings.scenes) {
            BuildSceneInfo info = new BuildSceneInfo();
            info.Enable = scene.enabled;
            info.Path = scene.path;
            _scenes.Add(info);
        }

        _window = GetWindow(typeof(BuildGUI));
        Rect rect = _window.position;
        rect.x = 200;
        rect.y = 200;
        rect.height = 600;
        _window.position = rect;
        if (_target == BuildTarget.Android) {
            _window.titleContent = new GUIContent("Android平台");
        }
        else {
            _window.titleContent = new GUIContent("iOS平台");
        }

        _window.Show();
    }

    public static void LoadConfig() {
        _infos.Clear();

        string uri = Application.dataPath + "/Configs/BuildPacketConfig.xml";
        XElement xml = XElement.Load(uri);
        var elems = xml.DescendantsAndSelf("platform");
        var platElems = elems.Descendants();
        foreach(var elem in platElems) {
            BuildPlatInfo info = new BuildPlatInfo();
            string target = XmlParser.GetAttribString(elem, "target");
            if (_target == BuildTarget.Android && target == "iOS") {
                continue;
            }
            else if(_target == BuildTarget.iOS && target == "Android") {
                continue;
            }

            info.Plat = XmlParser.GetAttribString(elem, "name");
            info.Channel = XmlParser.GetAttribString(elem, "channel");
            info.BundleId = XmlParser.GetAttribString(elem, "bundleId");
            info.Enable = false;
            _infos.Add(info);
        }
    }

    void OnEnable() {
        _showScenes = new AnimBool(true);
        _showScenes.valueChanged.AddListener(Repaint);

        _showPlats = new AnimBool(true);
        _showPlats.valueChanged.AddListener(Repaint);
    }

    private void OnGUI() {
        EditorGUILayout.Space();
        if (_target == BuildTarget.Android) {
            Project = EditorGUILayout.Toggle("打包成工程：", Project);
            Split = EditorGUILayout.Toggle("是否分包：", Split);
        }
        if (_target == BuildTarget.iOS) {
            EditorGUILayout.IntField("Build：", BundleVersion);
            SignRelease = EditorGUILayout.Toggle("是否使用Release签名：", SignRelease);
            MakeIPA = EditorGUILayout.Toggle("生成IPA：", MakeIPA);
        }

        ProductName = EditorGUILayout.TextField("产品名称：", ProductName);
        Version = EditorGUILayout.TextField("版本号：", Version);
        ShowFPS = EditorGUILayout.Toggle("显示FPS：", ShowFPS);
        Debug = EditorGUILayout.Toggle("开启调试：", Debug);
        ShowLog = EditorGUILayout.Toggle("输出LOG：", ShowLog);

        BundleIdExt = EditorGUILayout.TextField("包名扩展：", BundleIdExt);

        EditorGUI.indentLevel = 0;
        EditorGUILayout.Space();
        if(GUILayout.Button("开始")) {
            BuildPacket.ProcBuild(_target, _infos);
        }

        EditorGUI.indentLevel = 0;
        EditorGUILayout.Space();
        _showPlats.target = EditorGUILayout.Foldout(_showPlats.target, "编辑平台");
        using (var group = new EditorGUILayout.FadeGroupScope(_showPlats.faded)) {
            if(group.visible) {
                EditorGUI.indentLevel = 1;
                foreach(var info in _infos) {
                    info.Enable = EditorGUILayout.ToggleLeft(info.Plat, info.Enable);
                }
            }
        }

        EditorGUI.indentLevel = 0;
        EditorGUILayout.Space();
        _showScenes.target = EditorGUILayout.Foldout(_showScenes.target, "编辑场景");
        using (var group = new EditorGUILayout.FadeGroupScope(_showScenes.faded)) {
            if(group.visible) {
                EditorGUI.indentLevel = 1;
                foreach(var scene in _scenes) {
                    scene.Enable = EditorGUILayout.ToggleLeft(scene.Path, scene.Enable);
                }
            }
        }
    }
}

public class BuildPacket : MonoBehaviour {
    public static string SymbolFPS = "SHOW_FPS";
    public static string SymbolDebug = "DEBUG";
    public static string SymbolShowLog = "SHOW_UNITY_LOG";

    public static string SymbolGVoice = "GVOICE";

    public static string SymbolBugly = "BUGLY";

    [MenuItem("Build/Start")]
    private static void BuildStart() {
        BuildGUI.Init();
    }

    private static string[] FindScenes(string channel) {
        List<string> scenes = new List<string>();
        foreach(EditorBuildSettingsScene scene in EditorBuildSettings.scenes) {
            if(!scene.enabled) {
                continue;
            }

            scenes.Add(scene.path);
        }

        return scenes.ToArray();
    }

    /// <summary>
    /// 打包
    /// </summary>
    /// <param name="target"></param>
    /// <param name="plats"></param>
    public static void ProcBuild(BuildTarget target, List<BuildPlatInfo> plats) {
        PreBuild(target);

        try {
            foreach (var plat in plats) {
                if (!plat.Enable) {
                    continue;
                }

                Build(target, plat);
            }
        }
        catch(Exception e) {
            Debug.LogException(e);
        }
        finally {
            PostBuild(target);
        }
    }

    /// <summary>
    /// build之前处理
    /// </summary>
    private static void PreBuild(BuildTarget target) {
        // 修改资源路径名称
        if (target == BuildTarget.Android) {
            AssetDatabase.RenameAsset("Assets/StreamingAssets_Android", "StreamingAssets");
        }
        else if (target == BuildTarget.iOS) {
            AssetDatabase.RenameAsset("Assets/StreamingAssets_iOS", "StreamingAssets");
        }
    }

    /// <summary>
    /// build单个平台
    /// </summary>
    /// <param name="target"></param>
    private static void Build(BuildTarget target, BuildPlatInfo info) {
        // 当前工作路径
        string applicationPath = Application.dataPath.Replace("/Assets", "");

        // 产品名称
        PlayerSettings.productName = BuildGUI.ProductName;

        // 根据平台设置宏
        BuildGUI.Symbols = info.Plat;

        AddSymbol(SymbolBugly);
        AddSymbol(SymbolGVoice);

        if (BuildGUI.ShowFPS) {
            AddSymbol(SymbolFPS);
        }

        BuildOptions option = BuildOptions.None;
        if(BuildGUI.Debug) {
            AddSymbol(SymbolDebug);
            option |= BuildOptions.Development | BuildOptions.ConnectWithProfiler;
        }

        if (BuildGUI.ShowLog) {
            AddSymbol(SymbolShowLog);
        }

        if(target == BuildTarget.Android) {
            BuildAndroid(info, applicationPath, BuildGUI.Symbols, option);
        }
        else if(target == BuildTarget.iOS) {
            BuildIOS(info, applicationPath, BuildGUI.Symbols, option);
        }
    }

    /// <summary>
    /// build之后处理
    /// </summary>
    /// <param name="target"></param>
    private static void PostBuild(BuildTarget target) {
        // 还原资源路径名称
        if (target == BuildTarget.Android) {
            AssetDatabase.RenameAsset("Assets/StreamingAssets", "StreamingAssets_Android");
        }
        else if (target == BuildTarget.iOS) {
            AssetDatabase.RenameAsset("Assets/StreamingAssets", "StreamingAssets_iOS");
        }
    }

    private static void AddSymbol(string symbol) {
        BuildGUI.Symbols += ";" + symbol;
    }

    #region android
    /// <summary>
    /// build安卓平台
    /// </summary>
    private static void BuildAndroid(BuildPlatInfo info, string applicationPath, string symbol, BuildOptions option) {
        SetAndroidIcon(info.Plat);

        PreProcAndroidPlugins(info.Plat, symbol);
        PostProcAndroidPlugins(info.Plat, symbol);

        ModifyMenifest(info.Plat);

        string targetDir = "";
        string targetName = "";

        PlayerSettings.Android.useAPKExpansionFiles = BuildGUI.Split;
        EditorUserBuildSettings.exportAsGoogleAndroidProject = BuildGUI.Project;
        if(BuildGUI.Project) {
            string projectName = applicationPath.Substring(applicationPath.LastIndexOf('/') + 1);
            targetDir = applicationPath.Replace(projectName, "AndroidProject");
            targetName = "Temp_" + BuildGUI.BundleIdExt;

            option |= BuildOptions.AcceptExternalModificationsToPlayer;
        }
        else {
            targetDir = applicationPath + "/AndroidAPK";
            targetName = string.Format(info.Plat + "_{0}_{1}_{2}_{3}_{4}_{5}.apk", System.DateTime.Now.Year, System.DateTime.Now.Month, System.DateTime.Now.Day, System.DateTime.Now.Hour, System.DateTime.Now.Minute, BuildGUI.Version);
        }

        PlayerSettings.Android.keystoreName = applicationPath + "/AndroidKeystore/dc.keystore";
        PlayerSettings.Android.keystorePass = "CookieDC";
        PlayerSettings.Android.keyaliasName = "DC";
        PlayerSettings.Android.keyaliasPass = "CookieGame.cn";

        PlayerSettings.Android.splashScreenScale = AndroidSplashScreenScale.ScaleToFill;
        PlayerSettings.applicationIdentifier = info.BundleId + BuildGUI.BundleIdExt;
        PlayerSettings.bundleVersion = BuildGUI.Version;
        PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android, symbol);
        PlayerSettings.Android.bundleVersionCode = int.Parse(BuildGUI.Version.Replace(".", ""));

        // 创建文件夹
        if (!Directory.Exists(targetDir)) {
            Directory.CreateDirectory(targetDir);
        }

        // 开始build
        UnityEditor.Build.Reporting.BuildReport ret = BuildPipeline.BuildPlayer(FindScenes(info.Plat), targetDir + "/" + targetName, BuildTarget.Android, option);
        if (ret.summary.result != UnityEditor.Build.Reporting.BuildResult.Succeeded) {
            Debug.LogError("BuildPlayer failure: " + ret.summary.totalErrors);
        }
    }

    /// <summary>
    /// 根据平台设置不同图标
    /// </summary>
    private static void SetAndroidIcon(string plat) {

    }

    /// <summary>
    /// 根据不同平台设置插件
    /// </summary>
    /// <param name="plat"></param>
    /// <param name="symbol"></param>
    private static void PreProcAndroidPlugins(string plat, string symbol) {
        string dataPath = Application.dataPath;
        string modifyPluginPath = dataPath.Replace("Assets", "PluginModify");
        string pluginPath = dataPath + "/Plugins";

        string buglyPluginPath = pluginPath + "/BuglyPlugins/Android";
        string buglyPluginScriptsPath = pluginPath + "/BuglyPlugins/Scripts";
        DeleteFolder(buglyPluginPath);
        DeleteFolder(buglyPluginScriptsPath);
        if (symbol.Contains(SymbolBugly))
        {
            CopyFolder(modifyPluginPath + "/BuglyPlugins/Android", buglyPluginPath);
            CopyFolder(modifyPluginPath + "/BuglyPlugins/Scripts", buglyPluginScriptsPath);
        }

        string scriptsPath = dataPath + "/Scripts";
        string gVoicePluginPath = pluginPath + "/GVoicePlugins/Android";
        string gVoicePluginScriptsPath = scriptsPath + "/GVoicePlugins/Scripts";
        DeleteFolder(gVoicePluginPath);
        DeleteFolder(gVoicePluginScriptsPath);
        if (symbol.Contains(SymbolGVoice))
        {
            CopyFolder(modifyPluginPath + "/GVoicePlugins/Android", gVoicePluginPath);
            CopyFolder(modifyPluginPath + "/GVoicePlugins/Scripts", gVoicePluginScriptsPath);
        }

        AssetDatabase.Refresh();
    }

    /// <summary>
    /// 根据
    /// </summary>
    /// <param name="plat"></param>
    /// <param name="symbol"></param>
    private static void PostProcAndroidPlugins(string plat, string symbol) {

    }

    /// <summary>
    /// 修改menifest
    /// </summary>
    /// <param name="plat"></param>
    private static void ModifyMenifest(string plat) {

    }
    #endregion

    #region ios
    /// <summary>
    /// buildIOS平台
    /// </summary>
    /// <param name="info"></param>
    private static void BuildIOS(BuildPlatInfo info, string applicationPath, string symbol, BuildOptions option) {
        SetIOSIcon(info.Plat);

        PreProcIOSPlugins(info.Plat, symbol);
        PostProcIOSPlugins(info.Plat, symbol);

        string targetDir = "";
        string targetName = "";

        targetDir = applicationPath + "/IOSBuild";
        targetName = info.Plat + BuildGUI.BundleIdExt;

        option |= BuildOptions.AcceptExternalModificationsToPlayer;

        PlayerSettings.applicationIdentifier = info.BundleId + BuildGUI.BundleIdExt;
        PlayerSettings.bundleVersion = BuildGUI.Version;
        PlayerSettings.iOS.buildNumber = BuildGUI.BundleVersion.ToString();
        PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.iOS, symbol);

        // 创建文件夹
        if (!Directory.Exists(targetDir)) {
            Directory.CreateDirectory(targetDir);
        }

        // 开始build
        UnityEditor.Build.Reporting.BuildReport ret = BuildPipeline.BuildPlayer(FindScenes(info.Plat), targetDir + "/" + targetName, BuildTarget.iOS, option);
        if (ret.summary.result != UnityEditor.Build.Reporting.BuildResult.Succeeded) {
            Debug.LogError("BuildPlayer failure: " + ret.summary.totalErrors);
        }
    }

    /// <summary>
    /// 根据平台设置不同图标
    /// </summary>
    private static void SetIOSIcon(string plat) {

    }

    /// <summary>
    /// 根据不同平台设置插件
    /// </summary>
    /// <param name="plat"></param>
    /// <param name="symbol"></param>
    private static void PreProcIOSPlugins(string plat, string symbol) {
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

        if (symbol.Contains(SymbolBugly)) {
            CopyFolder(modifyPluginPath + "/BuglyPlugins/iOS", buglyPluginPath);
            CopyFolder(modifyPluginPath + "/BuglyPlugins/Scripts", buglyPluginScriptsPath);
        }

        if (symbol.Contains(SymbolGVoice))
        {
            CopyFolder(modifyPluginPath+ "/GVoicePlugins/iOS", gVoicePluginsPath);
            CopyFolder(modifyPluginPath + "/GVoicePlugins/Scripts", gVoicePluginScriptsPath);
        }

        AssetDatabase.Refresh();
    }

    /// <summary>
    /// 根据
    /// </summary>
    /// <param name="plat"></param>
    /// <param name="symbol"></param>
    private static void PostProcIOSPlugins(string plat, string symbol) {

    }
    #endregion

    #region util
    /// <summary>
    /// 删除目录
    /// </summary>
    private static void DeleteFolder(string path) {
        if (Directory.Exists(path)) {
            Directory.Delete(path, true);
        }
    }

    /// <summary>
    /// 删除文件
    /// </summary>
    /// <param name="file"></param>
    private static void DeleteFile(string file) {
        if (File.Exists(file)) {
            File.Delete(file);
        }
    }

    /// <summary>
    /// 拷贝目录
    /// </summary>
    /// <param name="source"></param>
    /// <param name="target"></param>
    public static void CopyFolder(string source, string target, bool deleteTarget = true) {
        if (!Directory.Exists(source)) {
            Debug.LogErrorFormat("拷贝的目录不存在 {0}", source);
            return;
        }

        if (deleteTarget) {
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
    private static void CopyFolderRecusive(string source, string target) {
        //如果目标文件夹中没有源文件夹则在目标文件夹中创建源文件夹
        if (!Directory.Exists(target)) {
            Directory.CreateDirectory(target);
        }

        string[] files = Directory.GetFiles(source);
        for (int n = 0; n < files.Length; ++n) {
            string sourceFile = files[n];
            string fileName = Path.GetFileName(sourceFile);
            string targetFile = target + "/" + fileName;
            File.Copy(sourceFile, targetFile, true);
        }

        //创建DirectoryInfo实例
        DirectoryInfo dirInfo = new DirectoryInfo(source);
        //取得源文件夹下的所有子文件夹名称
        DirectoryInfo[] directorys = dirInfo.GetDirectories();
        for (int j = 0; j < directorys.Length; j++) {
            //获取所有子文件夹名
            string childDir = source + "/" + directorys[j].Name;
            //把得到的子文件夹当成新的源文件夹，从头开始新一轮的拷贝
            CopyFolderRecusive(childDir, target + "/" + directorys[j].Name);
        }
    }
    #endregion
}