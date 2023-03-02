using UnityEngine;
using System.Text;

public class ResourceManagerConfig
{
#if UNITY_EDITOR
    public static string PersistentDataPath = string.Format("{0}/../data", Application.dataPath);
    public static string StreamingAssetsPath = string.Format("{0}/../streaming", Application.dataPath);
    public static string TemporaryCachePath = string.Format("{0}/../temporary", Application.dataPath);
#else
    //Application.persistentDataPath 移动端唯一一个可读写操作的文件夹
    public static string PersistentDataPath = Application.persistentDataPath;
    //Application.streamingAssetsPath 只读
    public static string StreamingAssetsPath = Application.streamingAssetsPath;
    //Application.temporaryCachePath 临时数据缓存目录 公共路径
    public static string TemporaryCachePath = Application.temporaryCachePath;
#endif


    public const string kVersionFileName = @"version";

    //88fa71f0a6e0dfedbb46d91cc0b37a50
    public const string kIndexFileName = @"index";


    //load path
    /// <summary>
    /// 内部路径 
    /// </summary>
    public static string kAssetLoadInnerPath
    {
        get
        {
            return string.Format("{0}", StreamingAssetsPath);
        }
    }

    /// <summary>
    /// 外部路径
    /// </summary>
    public static string kAssetLoadExtPath
    {
        get
        {
            return string.Format("{0}", PersistentDataPath);
        }
    }


    /// <summary>
    /// version indx
    /// </summary>
    public static string kVersionFileInnerPath
    {
        get
        {
            return string.Format("{0}/{1}", StreamingAssetsPath, kVersionFileName);
        }
    }
    public static string kVersionFileExtPath
    {
        get { return string.Format("{0}/{1}", PersistentDataPath, kVersionFileName); }
    }



    public static string AssetDownloadPath
    {
        get {
            return string.Format("{0}/Download", TemporaryCachePath);
        }
    }


    /// =======================================================================================================
    // string format helper
    private static StringBuilder _stringBuilder = new StringBuilder(1024);
    public static string FormatString(string format, object arg0)
    {
        _stringBuilder.Clear();
        return _stringBuilder.AppendFormat(format, arg0).ToString();
    }

    public static string FormatString(string format, object arg0, object arg1)
    {
        _stringBuilder.Clear();
        return _stringBuilder.AppendFormat(format, arg0, arg1).ToString();
    }

    public static string FormatString(string format, object arg0, object arg1, object arg2)
    {
        _stringBuilder.Clear();
        return _stringBuilder.AppendFormat(format, arg0, arg1, arg2).ToString();
    }

    public static string FormatString(string format,params object[] args)
    {
        _stringBuilder.Clear();
        return _stringBuilder.AppendFormat(format, args).ToString();
    }
}
