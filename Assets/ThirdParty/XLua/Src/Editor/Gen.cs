using UnityEngine;
using UnityEditor;

namespace CSObjectWrapEditor
{
    public static class Gen
    {
        [MenuItem("XLua/DC Lua Wrap File Gen")]
        private static void GenLuaWrapFile()
        {
            Debug.Log("GenLuaWrapFile.ClearAll");
            Generator.ClearAll();
        }
        
        [UnityEditor.Callbacks.DidReloadScripts]
        private static void OnScriptsReloaded()
        {
            if (!System.IO.Directory.Exists(GeneratorConfig.common_path))
            {
                Debug.Log("GenLuaWrapFile.GenAll");
                Generator.GenAll();
            }
        }
    }

    public class ReloadLuaScriptEditor : EditorWindow
    {
        static ReloadLuaScriptEditor _instance = null;
        public static ReloadLuaScriptEditor Instance()
        {
            if (_instance == null)
            {
                _instance = GetWindow<ReloadLuaScriptEditor>();
            }
            return _instance;
        }

        [MenuItem("XLua/DoFile Reload", false, 2)]
        private static void OpenWindow()
        {
            //if (LuaScriptManager.Instance != null)
            //{
            //    LuaScriptManager.Instance.DoLuaScript();
            //}
        }
    }
}