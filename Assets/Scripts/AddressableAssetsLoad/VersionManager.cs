using System.Collections;
using System.Collections.Generic;
using System.IO;
using LitJson;
using UnityEngine;
public class UpdateErrorCode
{
    //本地版本文件检查错误
    public static int Err_LocalVersionCheckFailed = 0x00000001;
    //服务器版本验证错误
    public static int Err_HostError = 0x00000002;
    //版本号格式错误
    public static int Err_VersionLengthFormatInvalid = 0x00000003;
    //版本超前
    public static int Err_GameVersionOvertop = 0x00000004;
    //版本有更新
    public static int Err_GameUpdateByPackage = 0x00000005;

    //无法链接host服务，检查网络链接
    public static int Err_HttpGetFailed_Host = 0x000000F0;
    //无法获取版本信息，检查网络链接
    public static int Err_HttpGetFailed_VersionFile = 0x000000F1;
    //无法下载文件，检查网络链接
    public static int Err_HttpGetFailed_Download = 0x000000F2;
    //无法获取公告信息
    public static int Err_HttpGetFailed_Bulletin = 0x000000F3;
    //
    public static int Err_HttpGetFailed_ListFile = 0x000000F4;
    //无法获取服务器状态
    public static int Err_HttpGetFailed_MaintainStatus = 0x000000F5;

    //无法写入游戏文件，确认存储空间
    public static int Err_FileWriteFailed = 0x0000F003;
    //无法安装游戏文件，确认存储空间
    public static int Err_FileSetupFailed = 0x0000F004;
}

public class VersionManager : MonoSingleton<VersionManager>,IMonoSingleton
{
    //标记使用本地资源
    private bool _check_version;

    public void SingletonInit()
    {
    }

    public void Init()
    {
            Debug.Log("VersionManager Init");

#if UNITY_EDITOR
        if (!Directory.Exists(ResourceManagerConfig.PersistentDataPath))
        {
            Directory.CreateDirectory(ResourceManagerConfig.PersistentDataPath);
        }
        if (!Directory.Exists(ResourceManagerConfig.StreamingAssetsPath))
        {
            Directory.CreateDirectory(ResourceManagerConfig.StreamingAssetsPath);
        }
        if (Directory.Exists(ResourceManagerConfig.TemporaryCachePath))
        {
            Directory.Delete(ResourceManagerConfig.TemporaryCachePath, true);
        }
        Directory.CreateDirectory(ResourceManagerConfig.TemporaryCachePath);
#endif
        if (Directory.Exists(ResourceManagerConfig.AssetDownloadPath))
        {
            Directory.Delete(ResourceManagerConfig.AssetDownloadPath,true);
        }
        Directory.CreateDirectory(ResourceManagerConfig.AssetDownloadPath);

        PreformAppVersion();
    }

    private void PreformAppVersion()
    {
#if UNITY_EDITOR
        bool develop = UnityEditor.EditorPrefs.GetBool("Develop", true);
        bool deploy_AA = UnityEditor.EditorPrefs.GetBool("Deploy_AA", false);
        bool deploy_AB = UnityEditor.EditorPrefs.GetBool("Deploy_AB", false);
        if (develop)
        {
            _check_version = true;
        }
        else if (deploy_AA)
        {
            //使用Addressable
        }
        else if (deploy_AB)
        {
            //使用AssetsBound
            SearchVersionFile();
        }
#else
#endif
    }

    private void SearchVersionFile()
    {
        string versionData = null;
#if UNITY_EDITOR //|| UNITY_IOS
        //从Bundle文件中读取版本信息----只读
        if (File.Exists(ResourceManagerConfig.kVersionFileInnerPath))
        {
            versionData = File.ReadAllText(ResourceManagerConfig.kVersionFileInnerPath);

            Debug.LogFormat("read version data {0}", ResourceManagerConfig.kVersionFileInnerPath);
        }
#endif
        if (string.IsNullOrEmpty(versionData))
        {
            _check_version = false;
            return;
        }
        else
        {
            //kVersionFileExtPath本地版本文件 -----可读写
            if (File.Exists(ResourceManagerConfig.kVersionFileExtPath))
            {
                //string ext_version_file = File.ReadAllText(ResourceManagerConfig.kVersionFileExtPath);

                //int inner_v1;
                //int inner_v2;
                //int inner_v3;
                //int inner_v4;
                //int ext_v1;
                //int ext_v2;
                //int ext_v3;
                //int ext_v4;
                //ReadVersion(versionData, out inner_v1, out inner_v2, out inner_v3, out inner_v4);
                //ReadVersion(ext_version_file, out ext_v1, out ext_v2, out ext_v3, out ext_v4);

                ////当版本号的前俩位不同时、或者第三位为0第四位不同时替换版本内容
                //if (inner_v1 != ext_v1 || inner_v2 != ext_v2)
                //{
                //    // todo delete update files
                //    File.WriteAllText(ResourceManagerConfig.kVersionFileExtPath, versionData);

                //    Debug.Log("cp version to persistent data path");
                //}
                //else if (inner_v1 == ext_v1 && inner_v2 == ext_v2)
                //{
                //    if (ext_v3 == 0 && inner_v4 != ext_v4)
                //    {
                //        File.WriteAllText(ResourceManagerConfig.kVersionFileExtPath, versionData);

                //        Debug.Log("cp version to persistent data path");
                //    }
                //}
            }
            else
            {
                File.WriteAllText(ResourceManagerConfig.kVersionFileExtPath, versionData);
                Debug.Log("cp version to persistent data path");
            }
        }

        if (File.Exists(ResourceManagerConfig.kVersionFileExtPath))
        {
            if (!ReadVersionFile(ResourceManagerConfig.kVersionFileExtPath,_local_version_file))
            {
                Debug.LogError("canot load version file");
                _check_version = false;
            }
            else
            {
                _check_version = true;
            }
        }
        else
        {
            Debug.LogError("canot open version file,not exist");
            _check_version = false;
        }
    }

    private bool ReadVersionFile(string path,VersionFile version_file)
    {
        string[] version_file_lines = File.ReadAllLines(path);
        return ParseVersionFile(version_file_lines, version_file);
    }

    private bool ParseVersionFile(string[] version_file_lines,VersionFile version_file)
    {
        char[] trim = new char[] { '\r', '\n' };
        version_file.Reset();
        if (version_file_lines != null && version_file_lines.Length >=1)
        {
            JsonData version_json = JsonMapper.ToObject(version_file_lines[0]);
            Debug.LogFormat("parse version file {0}", version_file_lines[0]);

            version_file.AppVersion = (string)version_json["client_version"];//版本
            version_file.BuildVersion = (int)version_json["build_number"];//
            version_file.BuildTime = (string)version_json["build_time"];

            Debug.LogFormat("local app version : {0}", version_file.AppVersion);
            Debug.LogFormat("local app build version : {0}", version_file.BuildVersion);
            Debug.LogFormat("local app build time : {0}", version_file.BuildTime);

            JsonData verify_urls_json = version_json["verify_urls"];
            version_file.VerifyDomains = new string[verify_urls_json.Count];
            for (int i = 0; i < verify_urls_json.Count; i++)
            {
                version_file.VerifyDomains[i] = (string)verify_urls_json[i];
                Debug.LogFormat("local verify path {0}", version_file.VerifyDomains[i]);
            }

            JsonData patch_urls_json = version_json["patch_urls"];
            version_file.PatchDomains = new string[patch_urls_json.Count];
            for (int i = 0; i < patch_urls_json.Count; i++)
            {
                version_file.PatchDomains[i] = (string)patch_urls_json[i];
                Debug.LogFormat("loac patchdomain {0} info is {1}", i, version_file.PatchDomains[i]);
            }

            for (int i = 1; i < version_file_lines.Length; i++)
            {
                string line = version_file_lines[i].Trim(trim);

                if (string.IsNullOrEmpty(line))
                {
                    continue;
                }
                string[] pair = line.Split(':');
                if (pair.Length != 3)
                {
                    continue;
                }

                bool ext_file = false;
                string res_path = Path.Combine(ResourceManagerConfig.PersistentDataPath, pair[0]);
                ext_file = File.Exists(res_path);
                version_file.FileInfo.Add(pair[0].GetHashCode(), new FileLocate(pair[0], pair[1], long.Parse(pair[2]), ext_file));
            }
        }

        bool succeed = version_file.FileInfo.Count != 0;
        if (succeed)
        {
            Debug.LogFormat("local version file loaded success count is :{0}", version_file.FileInfo.Count);
        }
        else
        {
            version_file.BuildVersion = 0;
            version_file.Reset();
            Debug.LogError("local version file not loaded!");
        }
        return succeed;
    }

    private void ReadVersion(string version_file_string, out int v1, out int v2, out int v3, out int v4)
    {
        string[] version_file_lines = version_file_string.Split('\n');

        LitJson.JsonData version_json = LitJson.JsonMapper.ToObject(version_file_lines[0]);

        string version = (string)version_json["client_version"];
        string[] versions = version.Split('.');
        v1 = int.Parse(versions[0]);
        v2 = int.Parse(versions[1]);
        if (versions.Length > 2)
        {
            v3 = int.Parse(versions[2]);
            v4 = (int)version_json["build_number"];
        }
        else
        {
            v3 = 0;
            v4 = 0;
        }
    }

    private bool ReadVersionFileString(string version_file_string, VersionFile version_file)
    {
        string[] version_file_lines = version_file_string.Split('\n');
        return ParseVersionFile(version_file_lines, version_file);
    }

    class FileLocate
    {
        public string Filename { private set; get; }
        public string MD5 { private set; get; }
        public long Size { private set; get; }
        public bool EXT { private set; get; }

        public FileLocate(string pair0,string pair1,long pair2,bool ext)
        {
            Filename = pair0;
            MD5 = pair1;
            Size = pair2;
            EXT = ext;
        }
    }
    class VersionFile
    {
        public string AppVersion;
        public int BuildVersion;
        public string BuildTime;

        //版本程序 验证
        public string[] VerifyDomains;
        //修补程序
        public string[] PatchDomains;

        public Dictionary<int, FileLocate> FileInfo = new Dictionary<int, FileLocate>();

        public VersionFile()
        {
            Reset();
        }

        public void Reset()
        {
            AppVersion = "0";
            BuildVersion = 0;
            FileInfo.Clear();
        }
    }

    //本地版本文件
    private VersionFile _local_version_file = new VersionFile();

    public string AppVersion
    {
        get
        {
#if UNITY_EDITOR
            bool develop = UnityEditor.EditorPrefs.GetBool("Develop", true);
            bool deploy_AA = UnityEditor.EditorPrefs.GetBool("Deploy_AA", false);
            bool deploy_AB = UnityEditor.EditorPrefs.GetBool("Deploy_AB", false);
            if (develop)
            {
                return "Editor AssetDataBase";
            }
            else if (deploy_AB)
            {
                return ResourceManagerConfig.FormatString("{0}.{1}", _local_version_file.AppVersion, _local_version_file.BuildVersion);
            }
            else if (deploy_AA)
            {
                return ResourceManagerConfig.FormatString("{0}.{1}", _local_version_file.AppVersion, _local_version_file.BuildVersion);
            }
#endif
            return ResourceManagerConfig.FormatString("{0}.{1}", _local_version_file.AppVersion, _local_version_file.BuildVersion);
        }
    }


    public string LocateFilePath(string file_name)
    {
        FileLocate file_info;
        if (_local_version_file.FileInfo.TryGetValue(file_name.GetHashCode(),out file_info))
        {
            return ResourceManagerConfig.FormatString("{0}/{1}", !file_info.EXT ? ResourceManagerConfig.kAssetLoadInnerPath : ResourceManagerConfig.kAssetLoadExtPath, file_name);
        }
        return file_name;
    }

    private Coroutine _versionCoroutine;

    /// <summary>
    /// 检查版本
    /// </summary>
    public void CheckingVersion()
    {
        if (_check_version)
        {
            _versionCoroutine = StartCoroutine(VerifyVersion());
        }
        else
        {
            EventManager.Instance.SendEvent(EventID.EID_RESUpdateFailed, new IntEventParam(UpdateErrorCode.Err_LocalVersionCheckFailed));
        }
    }

    // Update Part
    delegate void FDelegate_Void_WWW_Progress(WWW request);
    delegate void FDelegate_Void_WWW_Done(WWW request);

    public int HostErrorCode { get; private set; }
    public string ErrorString { get; private set; }

    private string _remoteAppBundleVersion = "0";
    public string RemoteAppBundleVersion
    {
        get { return _remoteAppBundleVersion; }
    }

    public float DownloadFileSize { get; private set; }

    public class BoardData
    {
        public int Seq;
        public int Tag;//0 none 1 new 2 hot
        public string Title;
        public string SubTitle;
        public string Content;
    }
    List<BoardData> _board_datas = new List<BoardData>();

    bool _maintain = false;

    IEnumerator VerifyVersion()
    {
        yield return null;
        string[] HostDomain = null;
        if (GameDriver.Inst.DevLoginMode)
        {
            HostDomain = GameDriver.Inst.HostURL;
        }
        else
        {
            HostDomain = _local_version_file.VerifyDomains;
        }
        if (HostDomain != null && HostDomain.Length != 0)
        {
            var channel = AppChannelHelper.GetAppChannel();
            Debug.LogError("A");
            yield return _HttpGet(HostDomain, GetHostURL(channel), UpdateErrorCode.Err_HttpGetFailed_Host, delegate (WWW request)
            {
                Debug.LogFormat("VerifyVersion remote file: {0}", request.text);

                //try
                {
                    LitJson.JsonData data = LitJson.JsonMapper.ToObject(request.text);

                    // code
                    int code = (int)data["Code"];
                    HostErrorCode = code;
                    if (HostErrorCode == 0)
                    {
                        LitJson.JsonData msgData = data["MsgData"];
                        // version
                        string version = (string)msgData["Version"];
                        _remoteAppBundleVersion = version;
                        Debug.LogFormat("remote version: {0}", _remoteAppBundleVersion);
                        // board
                        LitJson.JsonData boardData = msgData["Announcement"];
                        _board_datas.Clear();
                        if (boardData != null && boardData.IsArray)
                        {
                            for (int i = 0; i < boardData.Count; i++)
                            {
                                var boardItemData = boardData[i];

                                var item = new BoardData();

                                item.Seq = (int)boardItemData["Seq"];
                                item.Tag = (int)boardItemData["Tag"];
                                item.Title = (string)boardItemData["Title"];
                                item.SubTitle = (string)boardItemData["SmallTitle"];
                                item.Content = (string)boardItemData["Content"];

                                _board_datas.Add(item);
                            }
                        }
                        _board_datas.Sort((x, y) => { return x.Seq.CompareTo(y.Seq); });

                        //PaintingTCPClient.Inst.ConnectIP = (string)msgData["AgentAddress"];
                        //PaintingTCPClient.Inst.ConnectPort = (int)msgData["Port"];
                        _maintain = (int)msgData["ServerStatus"] == 0;

                        //Debug.LogFormat("Agent Address: {0}:{1}", PaintingTCPClient.Inst.ConnectIP, PaintingTCPClient.Inst.ConnectPort);

                        if (msgData.Keys.Contains("ServerList") && msgData["ServerList"].Keys.Contains("Info"))
                        {
                            LitJson.JsonData serverlist = msgData["ServerList"]["Info"];
                            for (int i = 0; i < serverlist.Count; i++)
                            {
                                LitJson.JsonData serverblockData = serverlist[i];
                                int block = (int)serverblockData["DistrictID"];
                                string block_name = (string)serverblockData["DistrictName"];

                                //GameServerData.Inst.AddBlock(block, block_name);

                                LitJson.JsonData serversData = serverblockData["Servers"];
                                for (int j = 0; j < serversData.Count; j++)
                                {
                                    LitJson.JsonData serverData = serversData[j];
                                    int id = (int)serverData["ID"];
                                    int status = (int)serverData["Status"];
                                    bool recommend = (bool)serverData["Recommend"];
                                    bool new_one = (bool)serverData["IsNew"];
                                    string name = (string)serverData["Name"];

                                    //GameServerData.Inst.AddServerItem(block, id, status, recommend, new_one, name);
                                }
                            }
                        }
                    }
                }
                //catch (System.Exception ex)
                //{
                //    HostErrorCode = 255;
                //    ErrorString = "host parse error!";
                //}
            });
        }
//#if SKIP_UPDATE || UNITY_EDITOR
//        SkipUpdate();
//#else
        //检查bundle版本更新
        yield return CheckVersion();
//#endif
    }

    public void SkipUpdate()
    {
        Debug.Log("version manager skip version checking");

        GameDriver.Inst.EnterGame();
    }

    IEnumerator _HttpGet(string[] domain,string router,int error,FDelegate_Void_WWW_Done done_func,FDelegate_Void_WWW_Progress progress_func = null)
    {
        //prigress_func("aaa");
        int retry = 0;
        Debug.Log("domin length" + domain.Length);
        while (retry < domain.Length)
        {
            //http://134.175.211.83/get_info?channel={1}&build_target={2}&build_number={3}.{4}&platform={5}
            string url = ResourceManagerConfig.FormatString("{0}/{1}", domain[retry], router);
            Debug.LogFormat("Http request :{0}", url);
            url = "http://192.168.3.125/";
            url = "http://kun.show.ghostry.cn/?int=5";
            url = @"File:///E:/Project/111/Frame/Info";

            WWW request = new WWW(url);

            //if (progress_func == null)
            //{
            //    yield return request;
            //}
            //else
            //{
            //    while (!request.isDone)
            //    {
            //        progress_func(request);
            //        yield return null;
            //    }
            //}

            yield return request;

            if (request.isDone)
            {
                Debug.LogError(request.text);
                Debug.Log("success");
                done_func(request);
                if (string.IsNullOrEmpty(request.error))
                {
                    Debug.Log("xxxx");
                }
                yield break;
            }
            Debug.LogError("======" + request.error + "======");
            Debug.LogErrorFormat("http request failed({0}) url {1}", retry, url);
            Debug.LogErrorFormat("http request failed({0}) error {1}", retry, request.error);

            retry++;
        }
        StopCoroutine(_versionCoroutine);
        //yield return null;
        //EventManager.Instance.SendEvent(EventID.EID_RESUpdateFailed, new IntEventParam(error));
    }

    private string GetHostURL(AppChannel channel)
    {
        return _GetHostURL(channel,"get_info");
    }

    private string _GetHostURL(AppChannel channel,string router)
    {
        string p = @"unknown";
        //UNITY_ANDROID p=@"android"
        //UNITY_IOS p=@"ios"
        //return "192.168.3.125";
        return ResourceManagerConfig.FormatString("{0}?channel={1}&build_target={2}&build_number={3}.{4}&platform={5}", router, channel.channel_id, channel.build_target, _local_version_file.AppVersion, _local_version_file.BuildVersion, p);
    }

    private string GetURLVersionFile()
    {
        return ResourceManagerConfig.FormatString("{0}/{1}", _remoteAppBundleVersion, ResourceManagerConfig.kVersionFileName);
    }

    private IEnumerator CheckVersion()
    {
        yield return null;

        Debug.Log("version manager checking version");

        do
        {
            if (HostErrorCode != 0)
            {
                EventManager.Instance.SendEvent(EventID.EID_RESUpdateFailed, new IntEventParam() { value = UpdateErrorCode.Err_HostError });
                Debug.LogError("服务器版本验证错误");
                break;
            }

            //_remoteAppBundleVersion = "0.289.2.20529";

            string[] Versions = _remoteAppBundleVersion.Split('.');
            string[] localVersions = _local_version_file.AppVersion.Split('.');
            if (Versions.Length != 4 || localVersions.Length != 3)
            {
                EventManager.Instance.SendEvent(EventID.EID_RESUpdateFailed, new IntEventParam() { value = UpdateErrorCode.Err_VersionLengthFormatInvalid });
                break;
            }

            int majorVersion = int.Parse(Versions[0]);
            int minorVersion = int.Parse(Versions[1]);
            int revisionVersion = int.Parse(Versions[2]);
            int buildNumber = int.Parse(Versions[3]);

            int local_majorVersion = int.Parse(localVersions[0]);
            int local_minorVersion = int.Parse(localVersions[1]);
            int local_revisionVersion = int.Parse(localVersions[2]);
            int local_buildNumber = _local_version_file.BuildVersion;

            bool need_update = false;

            //verisons 4位代表 后俩位位商店更新
            //前两位相等 第三位相等 第四位等于0  needupdate = true
            //                      第四位非0  无更新
            //          第三位偏小  need update = true
            //          第三位偏大  版本超前
            //前俩位不等 需要更新
            if (local_majorVersion == majorVersion &&
                local_minorVersion == minorVersion)
            {
                if (local_revisionVersion == revisionVersion)
                {
                    if (local_buildNumber == 0)
                    {
                        need_update = true;
                    }
                    else
                    {
                        // 无更新
                        EventManager.Instance.SendEvent(EventID.EID_RESUpdateFinish);
                    }
                }
                else if (local_revisionVersion < revisionVersion)
                {
                    need_update = true;
                }
                else
                {
                    // 版本超前
                    EventManager.Instance.SendEvent(EventID.EID_RESUpdateFailed, new IntEventParam() { value = UpdateErrorCode.Err_GameVersionOvertop });
                }
            }
            else
            {
                // 商店更新
                EventManager.Instance.SendEvent(EventID.EID_RESUpdateFailed, new IntEventParam() { value = UpdateErrorCode.Err_GameUpdateByPackage });
            }

            if (need_update)
            {
                yield return GetListFile();
                EventManager.Instance.SendEvent(EventID.EID_RESUpdateByResourceRequest);//提示资源更新
            }
        }
        while (false);
    }
    class DownloadInfo
    {
        public string path { get; private set; }
        public float size { get; private set; }

        public DownloadInfo(string path, float size) { this.path = path; this.size = size; }
        private DownloadInfo() { }
    }

    List<DownloadInfo> _add_list = new List<DownloadInfo>();

    private IEnumerator GetListFile()
    {
        Debug.Log("VersionManager  GetListFile ...");

        VersionFile tmpFile = new VersionFile();
        Debug.LogError("B");
        yield return _HttpGet(_local_version_file.PatchDomains, GetURLVersionFile(), UpdateErrorCode.Err_HttpGetFailed_ListFile, delegate (WWW request)
        {
            ReadVersionFileString(request.text, tmpFile);
        });
                     
        var it = tmpFile.FileInfo.GetEnumerator();
        float download_size_megabyte = 0;

        _add_list.Clear();
        while (it.MoveNext())
        {
            FileLocate local_file_info;
            if (_local_version_file.FileInfo.TryGetValue(it.Current.Key, out local_file_info))
            {
                FileLocate remote_file_info = it.Current.Value;
                if (local_file_info.MD5 != remote_file_info.MD5)
                {
                    float size = ToMegaByte(remote_file_info.Size);
                    _add_list.Add(new DownloadInfo(remote_file_info.Filename, size));
                    download_size_megabyte += size;
                }
            }       
            else
            {
                FileLocate remote_file_info = it.Current.Value;
                float size = ToMegaByte(remote_file_info.Size);
                _add_list.Add(new DownloadInfo(remote_file_info.Filename, remote_file_info.Size));
                download_size_megabyte += size;
            }
        }

        DownloadFileSize = download_size_megabyte;
    }

    float ToMegaByte(long size)
    {
        return size * 1.0f / 1024.0f / 1024.0f;
    }
}
