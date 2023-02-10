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

    /// <summary>
    /// 所有打开的UI
    /// </summary>
    public static Dictionary<string, GameObject> _dictUI = new Dictionary<string, GameObject>();
    private static List<UIPanel> _panelStack = new List<UIPanel>();
    private static bool _reorder = false;

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

        int panel_popUp_order = 0;
        int notfiy_order = 5000;
        int guide_order = 25000;
        int over_order = 27000;
        int top_order = 30000;

        int panel_count = 0;
        int blurbackOrder = -100;

        for (int i = 0; i < _panelStack.Count; i++)
        {
            //var panel = _panelStack[i];

            //if (panel.Layer == UILayer.Panel )
            //{
            //    panel.SetOrder(panel_popUp_order++ * 10);
            //}
            //else if (panel.layer == UILayer.NewbieGuide)
            //{
            //    panel.SetOrder(guide_order++);
            //}
            //else
            //{
            //    panel.SetOrder(-10);
            //}
        }
    }

    public void SingletonInit()
    {
    }

    /// <summary>
    /// 获取创建UI根节点
    /// </summary>
    private void CreateRoot()
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
    }

    private GameObject CreateUIObj(string name)
    {
        GameObject obj = null;
        return obj;
    }

    private void AttachToParent(GameObject obj, GameObject root)
    {

    }



    public GameObject OpenUI(string uiName)
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

        _dictUI.Add(uiName, obj);
        _panelStack.Add(obj.GetComponent<UIPanel>());

        //刷新层级
        _reorder = true;

        return obj;
    }

    public void CloseUI(string uiName)
    {

    }

    public void SingletonDestory()
    {
    }

}
