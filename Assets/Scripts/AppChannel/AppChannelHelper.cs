using System;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using UnityEngine;

public class AppChannelHelper
{
    private static AppChannel _AppChannel=null;
    public static AppChannel GetAppChannel() { return _AppChannel; }

    public static void LoadAppChannel()
    {
        _AppChannel = new DevGame();

        _AppChannel.LoadConfig();
        _AppChannel.init();
    }

    public static void LoginCallBack(bool succeed)
    {
        if (succeed)
        {
            Debug.Log("SDK Login Succeed!");

            //if (PaintingTCPClient.Inst.Connected)
            //{
            //    LoginToGameServer();
            //}
            //else
            //{
            //    //PaintingTCPClient.Inst.Connect(PaintingTCPClient.Inst.ConnectIP, PaintingTCPClient.Inst.ConnectPort);
            //    //PaintingTCPClient.Inst.ConnectedEvent -= OnConnected;
            //    //PaintingTCPClient.Inst.ConnectedEvent += OnConnected;
            //}
        }
        else
        {
            //EventManager.Instance.SendEvent(EventID.EID_LoginFailed);
            Debug.LogError("SDK Login Failed!");
        }
    }

    public static void LogoutCallBack(bool succeed)
    {
        //PaintingTCPClient.Inst.SendMsg(new CGLogoutMessage());
        Debug.LogError("SDK  LogoutCallBack!");
    }
}

public class AppChannel
{
    public string account_id { get; set; }
    public string channel_id { get; set; }
    public string merchant_id { get; private set; }
    public string app_id { get; private set; }
    public string server_id { get; private set; }
    public string app_key { get; private set; }
    public string build_target { get; set; }

    public virtual bool thirdpartyLogin() { throw new NotImplementedException("AppChannel::thirdpartyLogin"); }

    public void LoadConfig()
    {
        TextAsset config_file = Resources.Load<TextAsset>("channel_config");
        Debug.LogError(config_file.text);
        JsonData config_json = JsonMapper.ToObject(config_file.text);

        account_id = (string)config_json["account_id"];
        channel_id = (string)config_json["channel_id"];
        merchant_id = (string)config_json["merchant_id"];
        app_id = (string)config_json["app_id"];
        server_id = (string)config_json["server_id"];
        app_key = (string)config_json["app_key"];
        build_target = (string)config_json["build_target"];
    }

    public virtual void login() { throw new NotImplementedException("AppChannel::login"); }
    public virtual void checkLogin() { throw new NotImplementedException("AppChannel::checkLogin"); }
    public virtual void isRealNameAuth() { throw new NotImplementedException("AppChannel::isRealNameAuth"); }
    public virtual void logout() { throw new NotImplementedException("AppChannel::logout"); }
    public virtual void getUserInfo() { throw new NotImplementedException("AppChannel::getUserInfo"); }
    public virtual void register() { throw new NotImplementedException("AppChannel::register"); }
    public virtual void notifyZone(string role_id, string role_name) { throw new NotImplementedException("AppChannel::notifyZone"); }
    public virtual void createRole(string role_name, string role_id) { throw new NotImplementedException("AppChannel::createRole"); }
    public virtual void pay(int uid, string username, string role, string serverId, int total_fee, int game_money, string out_trade_no, string subject, string body, string extension_info, string notify_url, string order_sign) { throw new NotImplementedException("AppChannel::pay"); }
    public virtual void init() { throw new NotImplementedException("AppChannel::init"); }
    public virtual void showToast(string content) { throw new NotImplementedException("AppChannel::showToast"); }

    public virtual void OnAppPause(bool pauseStatus) { }

}
