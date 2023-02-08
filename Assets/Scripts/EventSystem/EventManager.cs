using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void EventCallBack(EventID event_id, EventParam param = null);
public delegate bool EventCallBackOnce(EventID event_id, EventParam param = null);

public class EventManager : MonoSingleton<EventManager>, IMonoSingleton
{
    class EventItem {
        public EventID eventId;
        public EventParam eventParam;

        public EventItem(EventID _eventID,EventParam _eventParam)
        {
            eventId = _eventID;
            eventParam = _eventParam;
        }
    }

    List<EventItem> _cachedEvent;

    Dictionary<EventID, EventCallBack> _eventCallback;

    class EventItemOnce
    {
        public EventCallBackOnce callback = null;
        public bool processed = false;

        public EventItemOnce(EventCallBackOnce _callback)
        {
            callback = _callback;
            processed = false;
        }
    }

    Dictionary<EventID, List<EventCallBackOnce>> _eventCallbackOnce_execute;

    public void Reset()
    {
        _cachedEvent.Clear();
        _eventCallback.Clear();
        _eventCallbackOnce_execute.Clear();
    }

    public void SingletonInit()
    {
        _eventCallback = new Dictionary<EventID, EventCallBack>();
        _eventCallbackOnce_execute = new Dictionary<EventID, List<EventCallBackOnce>>();

        _cachedEvent = new List<EventItem>();
    }

    private void Update()
    {
        for (int i = 0; i < _cachedEvent.Count; i++)
        {
            Notify(_cachedEvent[i].eventId, _cachedEvent[i].eventParam);
        }
        _cachedEvent.Clear();
    }

    public void RegisterEvent(EventID eventID,EventCallBack processer)
    {
        if (_eventCallback.ContainsKey(eventID))
        {
            _eventCallback[eventID] -= processer;
            _eventCallback[eventID] += processer;
        }
        else
        {
            _eventCallback.Add(eventID, processer);
        }
    }

    public void UnRegisterEvent(EventID eventID,EventCallBack processer)
    {
        if (_eventCallback.ContainsKey(eventID))
        {
            _eventCallback[eventID] -= processer;
            if (_eventCallback[eventID] == null)
            {
                _eventCallback.Remove(eventID);
            }
        }
    }

    public EventCallBackOnce RegisterEventOnce(EventID eventId, EventCallBackOnce callBackParam)
    {
        List<EventCallBackOnce> callBackOncesList = null;
        if (!_eventCallbackOnce_execute.TryGetValue(eventId,out callBackOncesList))
        {
            callBackOncesList = new List<EventCallBackOnce>();
            _eventCallbackOnce_execute.Add(eventId, callBackOncesList);
        }
        var func = new EventCallBackOnce(callBackParam);
        callBackOncesList.Add(func);

        return func;
    }

    public void UnregisterEventOnce(EventID eventId,EventCallBackOnce processer)
    {
        List<EventCallBackOnce> callBackOncesList;
        if (_eventCallbackOnce_execute.TryGetValue(eventId,out callBackOncesList))
        {
            for (int i = 0; i < callBackOncesList.Count; i++)
            {
                if (callBackOncesList[i] == processer)
                {
                    callBackOncesList.RemoveAt(i);
                    return;
                }
            }
        }
    }



    public void SendEvent(EventID eventID, EventParam param = null)
    {
        _cachedEvent.Add(new EventItem(eventID, param));
    }

    public void SendEventImmediately(EventID eventID, EventParam param = null)
    {
        Notify(eventID, param);
    }

    private void Notify(EventID eventId,EventParam param)
    {
        try
        {
            EventCallBack callBacks = null;
            if (_eventCallback.TryGetValue(eventId, out callBacks))
            {
                callBacks(eventId, param);
            }

            List<EventItemOnce> callBackOnces = null;
            if (_eventCallbackOnce_execute.TryGetValue(eventId, out callBackOnces))
            {
                _eventCallbackOnce_execute.Remove(eventId);

                for (int i = callBackOnces.Count - 1; i >= 0; --i)
                {
                    var it = callBackOnces[i];
                    if (it.callback != null)
                    {
                        if (it.callback(eventId, param))
                        {
                            callBackOnces.RemoveAt(i);
                        }
                    }
                    else
                    {
                        callBackOnces.RemoveAt(i);
                    }
                }
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError(new System.Exception(string.Format("Excute Event CallBack Error: {0},{1}\n{2}", eventId.ToString(), ex.Message, ex.StackTrace)));
        }
    }


    public void SingletonDestory()
    {
    }
}
