using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TimerNode
{
    public TimerManager.TimerHandler callback;

    public float duration;//定时器触发时间间隔
    public float delay;//第一次触发延迟时间
    public int repeat;//触发次数
    public float passedTime;//过去的时间

    public object param;//存储参数

    public int timerId;
}


public class TimerManager : MonoSingleton<TimerManager>, IMonoSingleton
{
    public delegate void  TimerHandler(object param);

    private Dictionary<int, TimerNode> timers = null;
    private List<int> removeTimers = null;
    private List<TimerNode> addTimers = null;

    int timerId;
    public void SingletonInit()
    {
        timers = new Dictionary<int, TimerNode>();
        addTimers = new List<TimerNode>();
        removeTimers = new List<int>();
        timerId = 0;
    }

    private void Update()
    {
        float deltaTime = Time.deltaTime;

        for (int i = 0; i < removeTimers.Count; i++)
        {
            if (timers.ContainsKey(removeTimers[i]))
            {
                timers.Remove(removeTimers[i]);
            }
        }
        removeTimers.Clear();

        for (int i = 0; i < addTimers.Count; i++)
        {
            timers.Add(addTimers[i].timerId, addTimers[i]);
        }
        addTimers.Clear();

        foreach (var item in timers.Values)
        {
            item.passedTime += deltaTime;
            if (item.passedTime >= item.delay+item.duration)
            {
                //做一次触发
                item.callback(item.param);
                item.repeat--;
                item.passedTime -= (item.delay + item.duration);
                item.delay = 0;
                if (item.repeat == 0)
                {
                        //触发次数结束、删除timer
                        removeTimers.Add(item.timerId);
                }
            }
        }
    }


    /// <summary>
    /// 添加定时事件
    /// </summary>
    /// <param name="func">回调方法</param>
    /// <param name="delay">延迟时间</param>
    /// <param name="param">参数</param>
    /// <returns></returns>
    public int Register(TimerHandler func, float delay,  object param = null) {
        return Register(func, 0, -1, delay, param);
    }

    /// <summary>
    /// 添加定时事件
    /// </summary>
    /// <param name="func">回调方法</param>
    /// <param name="repeat">重复次数</param>
    /// <param name="duration">间隔时间</param>
    /// <param name="delay">延迟时间</param>
    /// <param name="param">参数</param>
    /// <returns></returns>
    public int Register(TimerHandler func, float duration, int repeat = -1, float delay = 0, object param = null)
    {
        TimerNode node = new TimerNode();
        node.callback = func;
        node.repeat = repeat;
        node.duration = duration;
        node.delay = delay;
        node.param = param;
        node.passedTime = duration;
        node.timerId = timerId;
        timerId++;

        addTimers.Add(node);
        return node.timerId;
    }

    public void UnRegister(int timerId)
    {
        removeTimers.Add(timerId);
    }

    public void UnReguster(TimerHandler func)
    {
        foreach (var item in timers.Values)
        {
            if (item.callback == func)
            {
                removeTimers.Add(item.timerId);
            }
        }
    }
}
