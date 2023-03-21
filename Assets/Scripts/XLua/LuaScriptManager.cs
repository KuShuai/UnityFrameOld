using System.Collections.Generic;
using UnityEngine;
using XLua;

public class LuaScriptManager :Singleton<LuaScriptManager> ,ISingleton
{
    private LuaEnv _lua = null;

    private LuaFunction get_uiconfig;

    public void SingletonInit()
    {
    }

    public void Init()
    {
        _lua = new LuaEnv();
        _lua.AddLoader(LuaLoader);

        byte[] lua_code;
        ResourceManager.Instance.LoadLuaScript("Main", out lua_code);
        DoString_WithException(lua_code, "Main", _lua.Global);

        _lua.Global.Get("GetUIConfig", out get_uiconfig);
    }
    public void SingletonDestory()
    {
    }

    public LuaTable LoadScript(string script_path)
    {
        if (!string.IsNullOrEmpty(script_path))
        {
            byte[] lua_code;
            if (ResourceManager.Instance.LoadLuaScript(script_path,out lua_code))
            {
                LuaTable scriptEnv = _lua.NewTable();

                LuaTable meta = _lua.NewTable();
                meta.Set("__index", _lua.Global);
                scriptEnv.SetMetaTable(meta);
                meta.Dispose();

                string chunk_name = script_path;
                DoString_WithException(lua_code, chunk_name, scriptEnv);

                return scriptEnv;
            }
        }
        Debug.LogErrorFormat("load script failed path :{0}", script_path);
        return null;
    }

    public UIConfig GetUIConfig(int id)
    {
        UIConfig uIConfig = get_uiconfig?.Func<int, UIConfig>(id);

        if (uIConfig == null)
        {
            uIConfig = UIConfigSingleton.Instance.GetUIConfig(id);
        }

        return uIConfig;
    }


    public bool DoString_WithException(byte[] chunk,string chunkName = "chunk",LuaTable env = null)
    {
        try
        {
            _lua.DoString(chunk,chunkName,env);
            return true;
        }
        catch(System.Exception e)
        {
            Debug.LogErrorFormat("xlua exception :{0}\n{1}", e.Message, e.StackTrace);
        }
        return false;
    }

    private byte[] LuaLoader(ref string filePath)
    {
        filePath = filePath.Replace('.', '/');
        byte[] lua_code;
        ResourceManager.Instance.LoadLuaScript(filePath,out lua_code);
        return lua_code;
    }

    public static Dictionary<int, System.Type> kComponentTypes = new Dictionary<int, System.Type>()
    {
        { (int)EComponent.Canvas, typeof(Canvas) },
        { (int)EComponent.Image, typeof(UnityEngine.UI.Image) },
        { (int)EComponent.RawImage, typeof(UnityEngine.UI.RawImage) },
        { (int)EComponent.Text, typeof(UnityEngine.UI.Text) },
        { (int)EComponent.Button, typeof(UnityEngine.UI.Button) },
        { (int)EComponent.Slider, typeof(UnityEngine.UI.Slider) },
        { (int)EComponent.Toggle,typeof(UnityEngine.UI.Toggle)},
        { (int)EComponent.ToggleGroup,typeof(UnityEngine.UI.ToggleGroup)},
        { (int)EComponent.ScrollRect, typeof(UnityEngine.UI.ScrollRect) },
        { (int)EComponent.Transform, typeof(UnityEngine.Transform) },
        { (int)EComponent.RectTransform, typeof(UnityEngine.RectTransform) },
        { (int)EComponent.CanvasGroup, typeof(UnityEngine.CanvasGroup) },
        { (int)EComponent.InputField, typeof(UnityEngine.UI.InputField) },
        { (int)EComponent.Animator,typeof(UnityEngine.Animator)},
        { (int)EComponent.GridLayoutGroup, typeof(UnityEngine.UI.GridLayoutGroup) },
        { (int)EComponent.EventTrigger, typeof(UnityEngine.EventSystems.EventTrigger) },
        { (int)EComponent.PlayableDirector, typeof(UnityEngine.Playables.PlayableDirector) },
        { (int)EComponent.SpriteRenderer, typeof(UnityEngine.SpriteRenderer) },
        { (int)EComponent.TextMesh, typeof(UnityEngine.TextMesh) },
        { (int)EComponent.ContentSizeFitter, typeof(UnityEngine.UI.ContentSizeFitter) },
        { (int)EComponent.CanvasScaler, typeof(UnityEngine.UI.CanvasScaler) },
        { (int)EComponent.CinemachineBrain, typeof(Cinemachine.CinemachineBrain) },
    };
}