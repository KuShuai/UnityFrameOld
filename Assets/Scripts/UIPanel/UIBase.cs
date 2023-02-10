using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIWidget : MonoBehaviour
{

    [System.Serializable]
    public class LinkItem
    {
        public string Name;
        public GameObject UIWidget;
    }

    [HideInInspector]
    public List<LinkItem> Links = new List<LinkItem>();

    private Dictionary<int, LinkItem> _linkItemMap = null;

    private List<EventID> _eventList = null;

    //uiFunction
    public virtual void OnPreLoad() { }
    public virtual void OnLoad() { }
    public virtual void OnUpdate() { }
    public virtual void OnEvent(EventID event_id,EventParam param) { }
    public virtual void OnClose() { }

    private bool _inited = false;

    private void Awake()
    {
        InitWidget();
        OnPreLoad();
    }

    void InitWidget()
    {
        if (_inited)
            return;
        _inited = true;

        _eventList = new List<EventID>();
        _linkItemMap = new Dictionary<int, LinkItem>();

        for (int i = 0; i < Links.Count; i++)
        {
            int key = Links[i].Name.GetHashCode();
            if (!_linkItemMap.ContainsKey(key))
            {
                _linkItemMap.Add(key, Links[i]);
            }
        }
    }

    private void Start()
    {
        OnLoad();
    }

    private void Update()
    {
        OnUpdate();
    }

    void EventHandler(EventID eventId, EventParam param)
    {
        OnEvent(eventId, param);
    }

    public void Close()
    {
        OnClose();

        for (int i = 0; i < _eventList.Count; i++)
        {
            EventManager.Instance.UnRegisterEvent(_eventList[i], EventHandler);
        }
    }

    public void RegisterEvent(EventID eventId)
    {
        _eventList.Add(eventId);
        EventManager.Instance.RegisterEvent(eventId, EventHandler);
    }

    public T GetUIWidget<T>(string uiWidget_name) where T : Component
    {
        LinkItem linkItem = null;
        _linkItemMap.TryGetValue(uiWidget_name.GetHashCode(), out linkItem);
        if (linkItem == null)
            Debug.LogErrorFormat("get uiWidget  null name£º {0}", uiWidget_name);
        return linkItem != null ? linkItem.UIWidget.GetComponent<T>() : null;
    }

    public Component GetUIWidget(Type type, string uiWidget_name)
    {
        LinkItem item = null;
        _linkItemMap.TryGetValue(uiWidget_name.GetHashCode(), out item);
        if (item == null)
            Debug.LogErrorFormat("get uiWidget  null name£º {0}", uiWidget_name);
        return item != null ? item.UIWidget.GetComponent(type) : null;
    }

    public GameObject GetUIWidget(string uiWidget_name)
    {
        LinkItem linkItem = null;
        _linkItemMap.TryGetValue(uiWidget_name.GetHashCode(), out linkItem);
        if (linkItem == null)
            Debug.LogErrorFormat("get uiWidget  null name£º {0}", uiWidget_name);
        return linkItem != null ? linkItem.UIWidget : null;
    }
}
