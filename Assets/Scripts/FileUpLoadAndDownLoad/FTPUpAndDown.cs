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

            Debug.Log("�ϴ��ɹ�" + fileName);
        }
        catch (Exception e)
        {
            Debug.Log("�ϴ�ʧ��" + fileName + "error:" + e.Message);
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

        Debug.Log("���سɹ�" + fileName+"   loacalPath:"+ localPath);
        try
        {
        }
        catch (Exception e)
        {
            Debug.Log("����ʧ��" + fileName + "error:" + e.Message);
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
                    Debug.LogError(filePath + "�ļ��д���ʧ��");
                    return false;
                }
            }
        }
        Debug.LogWarning(filePath + "�ļ��д����ɹ�");
        return true;
    }

    /// <summary>
    /// �����ļ���
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
        //req.EnableSsl = true;//falseʱ�����е����ݺ������������
        try
        {
            req.UsePassive = true;//��ȡ�����ÿͻ���Ӧ�ó�������ݴ�����̵���Ϊ��
            req.UseBinary = true;
            req.KeepAlive = false;
            FtpWebResponse response = (FtpWebResponse)req.GetResponse();
            response.Close();
        }
        catch (Exception ex)
        {
            Debug.LogError("FTP �����ļ�����" + ftpPath+"||" + ex.GetType()+"|"+ ex.Message);
            req.Abort(); //��ֹ�첽FTP����
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

        req.Method = WebRequestMethods.Ftp.ListDirectory;//���ڻ�ȡFTP�������ϵ��ļ��ļ���б�
        //req.EnableSsl = true;//falseʱ�����е����ݺ������������
        try
        {
            req.UsePassive = true;//��ȡ�����ÿͻ���Ӧ�ó�������ݴ�����̵���Ϊ��
            req.UseBinary = true;
            req.KeepAlive = false;

            FtpWebResponse response = req.GetResponse() as FtpWebResponse;
            StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
            string line = reader.ReadToEnd();
            if (line == null)
            {
                Debug.Log("������");
                return false;
            }
            else if (line.IndexOf(folderName) > -1)
            {
                Debug.Log("����");
                return true;
            }
            else
            {
                Debug.Log(line);
                Debug.LogError("Ŀ¼������" + ftpPath + "FoderName" + folderName);
            }
            response.Close();
        }
        catch (Exception ex)
        {
            Debug.LogError("FTP Ŀ¼�쳣" + ftpPath + "||" + ex.GetType() + "|" + ex.Message);
            req.Abort(); //��ֹ�첽FTP����
            return false;
        }

        return false;
    }
}

//WebRequestMethods.Ftp.AppendFile = "APPE"; ���ڽ��ļ�׷�ӵ�FTP�������ϵ������ļ�
//WebRequestMethods.Ftp.DeleteFile = "DELE";����ɾ��FTP�������ϵ��ļ�
//WebRequestMethods.Ftp.DownloadFile = "RETR";���ڴ�FTP�����������ļ�
//WebRequestMethods.Ftp.GetDateTimestamp = "MDTM";���ڴ�FTP�������ϵ��ļ���������ʱ���
//WebRequestMethods.Ftp.GetFileSize = "SIZE";���ڼ���FTP�������ϵ��ļ���С
//WebRequestMethods.Ftp.ListDirectory = "NLST";���ڻ�ȡFTP�������ϵ��ļ��ļ���б�
//WebRequestMethods.Ftp.ListDirectoryDetails = "LIST";���ڻ�ȡFTP�������ϵ��ļ�����ϸ�б�
//WebRequestMethods.Ftp.MakeDirectory = "MKD";��FTP�������ϴ���Ŀ¼
//WebRequestMethods.Ftp.PrintWorkingDirectory = "PWD";��ʾ��ӡ��ǰ����Ŀ¼������
//WebRequestMethods.Ftp.RemoveDirectory = "RMD";��ʾ�Ƴ�Ŀ¼
//WebRequestMethods.Ftp.Rename = "RENAME";������Ŀ¼
//WebRequestMethods.Ftp.UploadFile = "STOR";�ļ����ص�FTP������
//WebRequestMethods.Ftp.UploadFileWithUniqueName = "STOU";��ʾ������Ψһ���Ƶ��ļ����ص�FTP������