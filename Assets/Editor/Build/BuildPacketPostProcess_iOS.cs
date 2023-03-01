using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using System.Collections.Generic;

#if (UNITY_5 || UNITY_2017_1_OR_NEWER) && (UNITY_IOS || UNITY_TVOS)
using UnityEditor.iOS.Xcode;
#endif

using System.Diagnostics;

public class BuildPacketPostProcess_iOS : ScriptableObject {

    private static Dictionary<string, bool> _frameworks = new Dictionary<string, bool>();
    private static List<string> _libs = new List<string>();

    public static void PostProc(string path) {
#if (UNITY_5 || UNITY_2017_1_OR_NEWER) && (UNITY_IOS || UNITY_TVOS)
        _frameworks.Clear();
        _libs.Clear();

        // modify project
        string projectPath = path + "/Unity-iPhone.xcodeproj/project.pbxproj";
        PBXProject project = new PBXProject();
        project.ReadFromString(File.ReadAllText(projectPath));
        string targetGuid = project.TargetGuidByName("Unity-iPhone");

        project.SetBuildProperty(targetGuid, "ENABLE_BITCODE", "NO");

        if (BuildGUI.SignRelease) {
            project.SetBuildProperty(targetGuid, "CODE_SIGN_IDENTITY", "iPhone Distribution");
            project.SetBuildProperty(targetGuid, "CODE_SIGN_STYLE", "Manual");
            project.SetBuildProperty(targetGuid, "PROVISIONING_PROFILE", "DC Distribut");
            project.SetBuildProperty(targetGuid, "DEVELOPMENT_TEAM", "QU24A97M3K");
        }
        //else if(Mode == BuildMode.Debug) {
        //    project.SetBuildProperty(_targetGuid, "CODE_SIGN_IDENTITY", "IPhone Developer");
        //    project.SetBuildProperty(_targetGuid, "CODE_SIGN_STYLE", "Automatic");
        //    project.SetBuildProperty(_targetGuid, "PROVISIONING_PROFILE", "Automatic");
        //    project.SetBuildProperty(_targetGuid, "DEVELOPMENT_TEAM", "QU24A97M3K");
        //}

        if(BuildGUI.Symbols.Contains(BuildPacket.SymbolBugly)) {
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
        else {
            BuildXCodeClass unityAppController = new BuildXCodeClass(path + "/Classes/UnityAppController.mm");
            unityAppController.Delete("#import <Bugly/Bugly.h>");
            unityAppController.Delete("    [Bugly startWithAppId:@\"efc934dce1\"];");
            unityAppController.WriteBack();
        }

        if (BuildGUI.Symbols.Contains(BuildPacket.SymbolGVoice))
        {
            AddFramework("SystemConfiguration.framework", false);
            AddFramework("CoreTelephony.framework", false);
            AddFramework("AudioToolbox.framework", false);
            AddFramework("CoreAudio.framework", false);
            AddFramework("AVFoundation.framework", false);
            AddFramework("Security.framework", false);
            AddFramework("libstdc++.6.0.9.tbd", false);

            //BuildXCodeClass unityAppController = new BuildXCodeClass(path + "/Classes/UnityAppController.mm");
            //unityAppController.WriteBelow("#include \"PluginBase/AppDelegateListener.h\"", "#import <GVoice/GVoice.h>");
            //unityAppController.WriteBelow("-> applicationDidFinishLaunching()", "    [[GVGCloudVoice sharedInstance] setAppInfo:\"1786265662\" withKey:\"fd34918cd2d8332bc9141f1ed34e1ff9\" andOpenID:" + PaintingLocal.Inst.UID + "];");
            //unityAppController.WriteBelow("-> applicationDidFinishLaunching()", "    [[GVGCloudVoice sharedInstance] initEngine];");
            //unityAppController.WriteBack();
        }
        //else
        //{
        //BuildXCodeClass unityAppController = new BuildXCodeClass(path + "/Classes/UnityAppController.mm");

        //unityAppController.Delete("#import <GVoice/GVoice.h>");
        //unityAppController.Delete("    [[GVGCloudVoice sharedInstance] setAppInfo:\"1786265662\" withKey:\"fd34918cd2d8332bc9141f1ed34e1ff9\" andOpenID:" + PaintingLocal.Inst.UID + "];");
        //unityAppController.Delete("    [[GVGCloudVoice sharedInstance] initEngine];");
        //unityAppController.WriteBack();
        //}

        // 把需要的framework和libs添加到工程中
        foreach (var pair in _frameworks) {
            project.AddFrameworkToProject(targetGuid, pair.Key, pair.Value);
        }
        foreach(var lib in _libs) {
            AddLibToProject(project, targetGuid, lib);
        }

        File.WriteAllText(projectPath, project.WriteToString());

        // modify plist
        string plistPath = path + "/Info.plist";
        PlistDocument plist = new PlistDocument();
        plist.ReadFromString(File.ReadAllText(plistPath));

        PlistElementDict rootDict = plist.root;
        
        rootDict.SetString("NSMicrophoneUsageDescription", "是否允许此游戏使用麦克风？");
        rootDict.SetString("NSPhotoLibraryUsageDescription", "是否允许此游戏使用相册？");
        //PlistElementDict nsAppTransportSecurity = rootDict.CreateDict("NSAppTransportSecurity");
        //nsAppTransportSecurity.SetBoolean("NSAllowsArbitraryLoads",true);

        plist.WriteToFile(plistPath);

        if (BuildGUI.MakeIPA) {
            string bashPath = PaintingEditorUtil.ParentFolder(path);
            string workDir = bashPath + "/IPABash";
            string exeFile = workDir + "/MakeIPA.sh";
            string exportOptionsPath = workDir + "/ExportOptions.plist";
            ExecuteProgram(exeFile, workDir, path + " " + exportOptionsPath + " " + BuildGUI.Version);
        }

#endif
    }

    private static void AddFramework(string framework, bool weak) {
        if(!_frameworks.ContainsKey(framework)) {
            _frameworks.Add(framework, weak);
        }
    }

    private static void AddLibs(string lib) {
        if(_libs.Contains(lib)) {
            _libs.Add(lib);
        }
    }

#if (UNITY_5 || UNITY_2017_1_OR_NEWER) && (UNITY_IOS || UNITY_TVOS)
    static void AddLibToProject(PBXProject project, string targetGuid, string lib) {
        string fileGuid = project.AddFile("usr/lib/" + lib, "Frameworks/" + lib, PBXSourceTree.Sdk);
        project.AddFileToBuild(targetGuid, fileGuid);
    }
#endif

    private static bool ExecuteProgram(string exeFile, string workDir, string args) {
        ProcessStartInfo info = new ProcessStartInfo();
        info.FileName = "osascript";
        info.UseShellExecute = true;
        info.RedirectStandardOutput = false;
        info.Arguments = string.Format("-e 'tell application \"Terminal\" \n activate \n do script \"sh {0} {1}\" \n end tell'", exeFile, args);
        UnityEngine.Debug.LogFormat("ExecuteProgram arguments {0}", info.Arguments);

        Process task = null;
        bool rt = true;
        try {
            UnityEngine.Debug.LogFormat("ExecuteProgram exeFile:{0} workDir:{1} args:{2}", exeFile, workDir, args);
            task = Process.Start(info);
            task.WaitForExit();
        }
        catch (Exception e) {
            UnityEngine.Debug.LogError("ExecuteProgram exception:" + e.ToString());
            return false;
        }
        finally {
            if (task != null && task.HasExited) {
                rt = (task.ExitCode == 0);
            }

            UnityEngine.Debug.LogFormat("ExecuteProgram finished");
        }

        return rt;
    }
}