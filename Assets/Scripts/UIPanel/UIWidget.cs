using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

[LuaCallCSharp]
public class UIWidget : MonoBehaviour
{
    [CSharpCallLua]
    public delegate void FDelegate_EventID_EventParam(int id, EventParam eventParam);
    public string LuaFile = string.Empty;
    public LuaTable scriptEnv = null;

    [System.Serializable]
    public class LinkItem
    {
        public string Name;
        public GameObject UIWidget;
    }

    [HideInInspector]
    public List<LinkItem> Links = new List<LinkItem>();

    private Dictionary<int, LinkItem> _linkItemMap = null;

    private List<int> _eventList = null;


    private bool _inited = false;

    public object loadParamter = null;

    private Action _AwakeAction = null;
    private LuaFunction _StartFunc = null;
    private Action _StartAction = null;
    private Action _UpdateAction = null;
    private Action _CloseAction = null;
    private Action _DestroyAction = null;
    private FDelegate_EventID_EventParam _OnEventFunc = null;
    public void Awake()
    {
        InitWidgets();
    }

    void InitWidgets()
    {
        //所有的子节点上的脚本先加载
        var widgets = GetComponentsInChildren<UIWidget>();
        for (int i = 0; i < widgets.Length; i++)
        {
            if (widgets[i].transform == transform)
                continue;
            widgets[i].InitWidgets();
        }

        if (_inited)
            return;
        _inited = true;

        _eventList = new List<int>();
        _linkItemMap = new Dictionary<int, LinkItem>();

        for (int i = 0; i < Links.Count; i++)
        {
            int key = Links[i].Name.GetHashCode();
            if (!_linkItemMap.ContainsKey(key))
            {
                _linkItemMap.Add(key, Links[i]);
            }
        }

        LoadScript(LuaFile);
    }

    public void Start()
    {
        OnLoad();
        _StartAction?.Invoke();
        _StartFunc?.Action(loadParamter);
        loadParamter = null;
    }

    public void Update()
    {
        OnUpdate();
        _UpdateAction?.Invoke();
    }

    void EventHandler(int eventId, EventParam param)
    {
        OnEvent(eventId, param);
        _OnEventFunc?.Invoke(eventId, param);
    }

    public void Close()
    {
        OnClose();
        _CloseAction?.Invoke();

        for (int i = 0; i < _eventList.Count; i++)
        {
            EventManager.Instance.UnRegisterEvent_LUA(_eventList[i], EventHandler);
        }
        _eventList.Clear();
    }

    private void OnDestroy()
    {
        _DestroyAction?.Invoke();
        if (scriptEnv != null)
        {
            _AwakeAction = null;
            _StartFunc = null;
            _StartAction = null;
            _UpdateAction = null;
            _CloseAction = null;
            _DestroyAction = null;
            _OnEventFunc = null;

            scriptEnv.Dispose();
            scriptEnv = null;
        }
    }

    public void RegisterEvent(int eventId)
    {
        _eventList.Add(eventId);
        EventManager.Instance.RegisterEvent_LUA(eventId, EventHandler);
    }

    public T GetUIWidget<T>(string uiWidget_name) where T : Component
    {
        LinkItem linkItem = null;
        _linkItemMap.TryGetValue(uiWidget_name.GetHashCode(), out linkItem);
        if (linkItem == null)
            Debug.LogErrorFormat("get uiWidget  null name： {0}", uiWidget_name);
        return linkItem != null ? linkItem.UIWidget.GetComponent<T>() : null;
    }

    public Component GetUIWidget(Type type, string uiWidget_name)
    {
        LinkItem item = null;
        _linkItemMap.TryGetValue(uiWidget_name.GetHashCode(), out item);
        if (item == null)
            Debug.LogErrorFormat("get uiWidget  null name： {0}", uiWidget_name);
        return item != null ? item.UIWidget.GetComponent(type) : null;
    }

    public GameObject GetUIWidget(string uiWidget_name)
    {
        LinkItem linkItem = null;
        _linkItemMap.TryGetValue(uiWidget_name.GetHashCode(), out linkItem);
        if (linkItem == null)
            Debug.LogErrorFormat("get uiWidget  null name： {0}", uiWidget_name);
        return linkItem != null ? linkItem.UIWidget : null;
    }

    private void LoadScript(string script_path)
    {
        if (!string.IsNullOrEmpty(script_path))
        {
            scriptEnv = LuaScriptManager.Instance.LoadScript(script_path);
            if (scriptEnv != null)
            {
                scriptEnv.Set("self", this);

                scriptEnv.Get("Awake", out _AwakeAction);
                scriptEnv.Get("Start", out _StartAction);
                scriptEnv.Get("Update", out _UpdateAction);
                scriptEnv.Get("Close", out _CloseAction);
                scriptEnv.Get("Destroy", out _DestroyAction);
                scriptEnv.Get("StartFunc", out _StartFunc);
                scriptEnv.Get("OnEvent", out _OnEventFunc);
            }
        }

        OnPreLoad();
        _AwakeAction?.Invoke();
        //StartCoroutine(Self_Start());
    }

    //uifunction
    public virtual void OnPreLoad() { }
    public virtual void OnLoad() { }
    public virtual void OnShow(System.Object obj) { }
    public virtual void DisShow() { }
    public virtual void OnUpdate() { }
    public virtual void OnEvent(int event_id, EventParam param) { }
    public virtual void OnClose() { }
}
