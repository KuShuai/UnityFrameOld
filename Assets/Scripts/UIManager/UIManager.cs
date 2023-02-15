using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoSingleton<UIManager>, IMonoSingleton
{
    public const string UIPanel = "UIPanel";

    private static Dictionary<string, int> uiLayer = new Dictionary<string, int>()
    {
        { "Back",5000},
        { "Normal",10000},
        { "Top",15000}
    };

    /// <summary>
    /// UI根节点
    /// </summary>
    private static GameObject _root;

    private static Camera _uiCamera;

    /// <summary>
    /// 所有打开的UI
    /// </summary>
    public static Dictionary<string, GameObject> _dictUI;
    private static List<UIPanel> _panelStack;
    private static bool _reorder;

    public void Init()
    {
        _dictUI = new Dictionary<string, GameObject>();
        _panelStack = new List<UIPanel>();
        _reorder = false;
    }

    private void Update()
    {
        OnPanelStackChanged();
    }

    private void OnPanelStackChanged()
    {
        if (!_reorder)
        {
            return;
        }
        _reorder = false;

        int fixed_order = 1000;
        int normal_order = 20000;
        int top_order = 30000;

        for (int i = 0; i < _panelStack.Count; i++)
        {
            var panel = _panelStack[i];
                
            if (panel._uiConfig._layer == UILayer.Fixed)
            {
                panel.SetOrder(fixed_order++);
            }
            else if (panel._uiConfig._layer == UILayer.Normal)
            {
                panel.SetOrder(normal_order++);
            }
            else if (panel._uiConfig._layer== UILayer.Top)
            {
                panel.SetOrder(top_order++);
            }
            else
            {
                panel.SetOrder(-10);
            }
        }
    }

    public void SingletonInit()
    {
    }

    /// <summary>
    /// 获取创建UI根节点
    /// </summary>
    private static void CreateRoot()
    {
        if (_root == null)
        {
            GameObject obj = ResourceManager.Instance.Load(RSPathUtil.UI(UIPanelConst.Root)) as GameObject;

            if (obj == null)
            {
                Debug.LogErrorFormat("找不到UI：{0}", UIPanelConst.Root);
                return;
            }

            _root = Instantiate(obj);
            DontDestroyOnLoad(_root);
        }

        if (_root != null && _uiCamera == null)
        {
            _uiCamera = _root.GetComponentInChildren<Camera>();
        }
    }

    private static GameObject CreateUIObj(string name)
    {
        GameObject obj = ResourceManager.Instance.Load(RSPathUtil.UI(name)) as GameObject;
        if (obj == null)
        {
            return null;
        }

        GameObject clone = Instantiate(obj);
        clone.name = name;
        Canvas canvas = clone.GetComponent<Canvas>();
        if (canvas == null)
        {
            Debug.LogErrorFormat("UI对象缺少canvas组件：{0}", name);
            return null;
        }

        canvas.worldCamera = _uiCamera;

        canvas.sortingLayerName = "UI";
        var panel = clone.GetComponent<UIPanel>();
        if (panel != null)
        {
            panel.SetRender(canvas);
        }

        //添加屏幕适配组件
        //if (clone.GetComponent<UIAdaptive>() == null)
        //{
        //    clone.AddComponent<UIAdaptive>();
        //}

        return clone;
    }

    private static void AttachToParent(GameObject obj, GameObject root)
    {
        Vector3 localScale = obj.transform.localScale;
        obj.transform.SetParent(root.transform);

        obj.transform.localPosition = Vector3.zero;
        obj.transform.localRotation = Quaternion.identity;
        obj.transform.localScale = localScale;
    }



    public static GameObject OpenUI(string uiName)
    {
        if (_dictUI.ContainsKey(uiName))
            return null;
        CreateRoot();
        if (_root == null)
            return null;

        GameObject obj = CreateUIObj(uiName);
        if (obj == null)
            return null;

        AttachToParent(obj, _root);

        UIConfig uiConfig = UIConfigSingleton.Instance.GetUIConfig(uiName);
        if (uiConfig == null)
        {
            Destroy(obj);
            Debug.LogErrorFormat("obj config is null :{0}", uiName);
            return null;
        }

        UIPanel uipanel = obj.GetComponent<UIPanel>();
        uipanel.SetUp(uiConfig);

        _dictUI.Add(uiName, obj);
        _panelStack.Add(obj.GetComponent<UIPanel>());

        //刷新层级
        _reorder = true;

        return obj;
    }

    public static T OpenUI<T>(string uiName)where T : MonoBehaviour
    {
        GameObject obj = OpenUI(uiName);
        if (obj == null)
        {
            return null;
        }
        T ret = obj.GetComponent<T>();
        return ret;
    }

    public static T OpenUI<T>(UIPanelEnum panelEnum)where T : MonoBehaviour
    {
        UIConfig uiConfig = UIConfigSingleton.Instance.GetUIConfig(panelEnum);
        if (uiConfig == null)
        {
            return null;
        }
        return OpenUI<T>(uiConfig._name);
    }

    public static void CloseUI(UIPanelEnum uiEnum, bool removeQuene = true)
    {
        string uiName = UIConfigSingleton.Instance.GetUIConfig(uiEnum)._name;
        CloseUI(uiName, removeQuene);
    }

    public static void CloseUI(string uiName, bool removeQuene = true)
    {
        GameObject obj = null;
        if (!_dictUI.TryGetValue(uiName,out obj))
        {
            Debug.LogErrorFormat("没有找到对应关闭的UI ：{0}",uiName);
            return;
        }
        Debug.Log(uiName);
        _dictUI.Remove(uiName);
        _panelStack.Remove(obj.GetComponent<UIPanel>());

        if (obj == null)
        {
            Debug.LogErrorFormat("所关闭的UI已经销毁{0}", uiName);
            return;
        }

        UIPanel panel = obj.GetComponent<UIPanel>();
        if (panel != null)
        {
            panel.Close();
        }
        Destroy(obj);

        _reorder = true;

        ResourceManager.Instance.Unload(RSPathUtil.UI(uiName));
    }

    public void SingletonDestory()
    {
    }

}
