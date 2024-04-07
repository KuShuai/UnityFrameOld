using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Util : MonoBehaviour
{
    public static T CreateObj<T>(string name) where T:Component
    {
        T ret = null;
        GameObject obj = new GameObject(name);
        obj.SetActive(true);
        ret = obj.AddComponent<T>();
        return ret;
    }

    public static T CreateDonDestroyObj<T>(string name) where T : Component
    {
        T ret = CreateObj<T>(name);
        DontDestroyOnLoad(ret.gameObject);
        return ret;
    }

    public static void uDebug(string param)
    {
        Debug.Log(param);
    }

    /// <summary>
    /// ��γ��ת��ΪU3D���꣬��ȱ������ţ��Լ����һ����ƫ����
    /// </summary>
    /// <param name="longitude">����</param>
    /// <param name="latitude">γ��</param>
    /// <returns></returns>
    public static Vector3 ConvertToVector3(double longitude, double latitude)
    {
        const int radius = 6378137;
        const double minorRadius = 6356752.314245179d;

        const double d = Mathf.PI / 180;
        const double r = radius;
        var y = latitude * d;
        const double tmp = minorRadius / r;
        double e = Mathf.Sqrt((float)(1 - tmp * tmp)),
              con = e * Mathf.Sin((float)y);

        var ts = Mathf.Tan((float)(Mathf.PI / 4 - y / 2)) / Mathf.Pow((float)((1 - con) / (1 + con)), (float)(e / 2));
        y = -r * Mathf.Log(Mathf.Max(ts, (float)1E-10));

        var xValue = longitude * d * r;
        var yValue = y;

        return new Vector3((float)xValue, 30, (float)yValue);
    }

    public static void ClickGameObject(GameObject obj)
    {
        ExecuteEvents.Execute(obj, new PointerEventData(EventSystem.current), ExecuteEvents.pointerClickHandler);
    }

    public static long ConvertDateTimeToLong(DateTime dt)
    {
        DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
        TimeSpan toNow = dt.Subtract(dtStart);
        long timeStamp = toNow.Ticks;
        timeStamp = long.Parse(timeStamp.ToString().Substring(0, timeStamp.ToString().Length - 4));
        return timeStamp;
    }

    public static DateTime ConvertLongToDateTime(long d, string dd = "0000")
    {
        //DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1, 8, 0, 0));
        //TimeSpan toNow = new TimeSpan(long.Parse(d + dd));
        //DateTime rt = dtStart.Add(toNow);
        //return rt;
        DateTime dtStart = TimeZoneInfo.ConvertTime(new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), TimeZoneInfo.Local);
        long lTime = d * 10000;
        TimeSpan toNow = new TimeSpan(lTime);
        return dtStart.Add(toNow);
    }
}
