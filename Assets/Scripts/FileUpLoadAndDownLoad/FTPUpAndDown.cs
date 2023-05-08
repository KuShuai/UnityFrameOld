using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using LitJson;

public class FTPUpAndDown 
{

    private static JsonData jsonData;
    
    private static void Init() { 

        string data = File.ReadAllText(Application.streamingAssetsPath + "/FTPServer.json");
        jsonData = JsonMapper.ToObject(data);

    }


    public static void FtpUpLoadFile(string filePath, string fileName)
    {
        if (jsonData == null)
            Init();

        if (fileName.StartsWith("\\"))
        {
            fileName= fileName.Substring(1, fileName.Length-2);
        }
        string[] files = fileName.Replace("\\","/").Split('/');
        
        for (int i = 0; i < files.Length-1; i++)
        {
            string file = "";
            for (int y = 0; y < i+1; y++)
            {
                file += "/" + files[y];
            }
            if (!string.IsNullOrEmpty(file))
            {
                if (!FtpMakeDir(file))
                {
                    return;
                }
                
            }
        }
        FtpWebRequest req = FtpWebRequest.Create(new Uri("ftp://"+jsonData["ip"]+":"+ jsonData["port"] + "/" + fileName)) as FtpWebRequest;
        NetworkCredential n = new NetworkCredential(jsonData["name"].ToString(), jsonData["password"].ToString());
        req.Credentials = n;
        req.Proxy = null;
        req.KeepAlive = false;
        req.Method = WebRequestMethods.Ftp.UploadFile;
        req.UseBinary = true;
        Stream upLoadStream = req.GetRequestStream();
        try
        {
            using (FileStream file = File.OpenRead(filePath))
            {
                byte[] bytes = new byte[2048];
                int contentLength = file.Read(bytes, 0, bytes.Length);

                while (contentLength != 0)
                {
                    upLoadStream.Write(bytes, 0, contentLength);
                    contentLength = file.Read(bytes, 0, bytes.Length);
                }

                file.Close();
                upLoadStream.Close();
            }

            Debug.Log("上传成功" + fileName);
        }
        catch (Exception e)
        {
            Debug.Log("上传失败" + fileName + "error:" + e.Message);
        }
    }

    public static void FtpDownLoadFile(string fileName, string localPath)
    {
        if (jsonData == null)
            Init();
        FtpWebRequest req = FtpWebRequest.Create(new Uri("ftp://" + jsonData["ip"] + ":" + jsonData["port"] + "/" + fileName)) as FtpWebRequest;
        NetworkCredential n = new NetworkCredential(jsonData["name"].ToString(), jsonData["password"].ToString());
        req.Credentials = n;
        req.Proxy = null;
        req.KeepAlive = false;
        req.Method = WebRequestMethods.Ftp.DownloadFile;
        req.UseBinary = true;
        FtpWebResponse res = req.GetResponse() as FtpWebResponse;
        Stream downLoadStream = res.GetResponseStream();

        using (FileStream file = File.Create(localPath))
        {
            byte[] bytes = new byte[2048];
            int contentLength = downLoadStream.Read(bytes, 0, bytes.Length); ;

            while (contentLength != 0)
            {
                file.Write(bytes, 0, contentLength);
                contentLength = downLoadStream.Read(bytes, 0, contentLength);
            }

            file.Close();
            downLoadStream.Close();
        }

        Debug.Log("下载成功" + fileName+"   loacalPath:"+ localPath);
        try
        {
        }
        catch (Exception e)
        {
            Debug.Log("下载失败" + fileName + "error:" + e.Message);
        }
    }

    public static bool FtpMakeDirPath(string filePath)
    {
        string[] files = filePath.Split('/');
        for (int i = 0; i < files.Length; i++)
        {
            string file = "";
            for (int y = 0; y < i + 1; y++)
            {
                if (!string.IsNullOrEmpty(files[y]))
                {
                    file += "/" + files[y];
                }
            }
            if (!string.IsNullOrEmpty(file))
            {
                if (!FtpMakeDir(file))
                {
                    Debug.LogError(filePath + "文件夹创建失败");
                    return false;
                }
            }
        }
        Debug.LogWarning(filePath + "文件夹创建成功");
        return true;
    }

    /// <summary>
    /// 创建文件夹
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns></returns>
    private static bool FtpMakeDir(string fileName)
    {
        if (jsonData == null)
            Init();
        if (fileName.StartsWith("/"))
        {
            fileName = fileName.Remove(0, 1);
        }
        if (FtpDirExist(fileName))
        {
            return true;
        }
        string ftpPath = "ftp://" + jsonData["ip"] + ":" + jsonData["port"] + "/" + fileName;
        FtpWebRequest req = WebRequest.Create(ftpPath) as FtpWebRequest;

        NetworkCredential n = new NetworkCredential(jsonData["name"].ToString(), jsonData["password"].ToString());
        req.Credentials = n;

        req.Method = WebRequestMethods.Ftp.MakeDirectory;
        //req.EnableSsl = true;//false时，所有的数据和命令都会是明文
        try
        {
            req.UsePassive = true;//获取或设置客户端应用程序的数据传输过程的行为。
            req.UseBinary = true;
            req.KeepAlive = false;
            FtpWebResponse response = (FtpWebResponse)req.GetResponse();
            response.Close();
        }
        catch (Exception ex)
        {
            Debug.LogError("FTP 创建文件错误" + ftpPath+"||" + ex.GetType()+"|"+ ex.Message);
            req.Abort(); //终止异步FTP操作
            return false;
        }

        return true;
    }

    private static bool FtpDirExist(string fileName)
    {
        if (fileName.StartsWith("/"))
        {
            fileName = fileName.Remove(0, 1);
        }
        string[] paths = fileName.Split('/');
        fileName = "";
        for (int i = 0; i < paths.Length-1; i++)
        {
            if (!string.IsNullOrEmpty(fileName))
            {
                fileName += "/";
            }
            fileName += paths[i];
        }
        string folderName = paths[paths.Length - 1];
        string ftpPath = "ftp://" + jsonData["ip"] + ":" + jsonData["port"] + "/" + fileName;
        FtpWebRequest req = WebRequest.Create(ftpPath) as FtpWebRequest;

        NetworkCredential n = new NetworkCredential(jsonData["name"].ToString(), jsonData["password"].ToString());
        req.Credentials = n;

        req.Method = WebRequestMethods.Ftp.ListDirectory;//用于获取FTP服务器上的文件的简短列表
        //req.EnableSsl = true;//false时，所有的数据和命令都会是明文
        try
        {
            req.UsePassive = true;//获取或设置客户端应用程序的数据传输过程的行为。
            req.UseBinary = true;
            req.KeepAlive = false;

            FtpWebResponse response = req.GetResponse() as FtpWebResponse;
            StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
            string line = reader.ReadToEnd();
            if (line == null)
            {
                Debug.Log("不存在");
                return false;
            }
            else if (line.IndexOf(folderName) > -1)
            {
                Debug.Log("存在");
                return true;
            }
            else
            {
                Debug.Log(line);
                Debug.LogError("目录不存在" + ftpPath + "FoderName" + folderName);
            }
            response.Close();
        }
        catch (Exception ex)
        {
            Debug.LogError("FTP 目录异常" + ftpPath + "||" + ex.GetType() + "|" + ex.Message);
            req.Abort(); //终止异步FTP操作
            return false;
        }

        return false;
    }
}

//WebRequestMethods.Ftp.AppendFile = "APPE"; 用于将文件追加到FTP服务器上的现有文件
//WebRequestMethods.Ftp.DeleteFile = "DELE";用于删除FTP服务器上的文件
//WebRequestMethods.Ftp.DownloadFile = "RETR";用于从FTP服务器下载文件
//WebRequestMethods.Ftp.GetDateTimestamp = "MDTM";用于从FTP服务器上的文件检索日期时间戳
//WebRequestMethods.Ftp.GetFileSize = "SIZE";用于检索FTP服务器上的文件大小
//WebRequestMethods.Ftp.ListDirectory = "NLST";用于获取FTP服务器上的文件的简短列表
//WebRequestMethods.Ftp.ListDirectoryDetails = "LIST";用于获取FTP服务器上的文件的详细列表
//WebRequestMethods.Ftp.MakeDirectory = "MKD";在FTP服务器上创建目录
//WebRequestMethods.Ftp.PrintWorkingDirectory = "PWD";表示打印当前工作目录的名称
//WebRequestMethods.Ftp.RemoveDirectory = "RMD";表示移除目录
//WebRequestMethods.Ftp.Rename = "RENAME";重命名目录
//WebRequestMethods.Ftp.UploadFile = "STOR";文件上载到FTP服务器
//WebRequestMethods.Ftp.UploadFileWithUniqueName = "STOU";表示将具有唯一名称的文件上载到FTP服务器