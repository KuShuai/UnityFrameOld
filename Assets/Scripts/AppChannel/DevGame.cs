using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DevGame : AppChannel
{
    public override bool thirdpartyLogin() { return false; }

    public override void login() { AppChannelHelper.LoginCallBack(true); }
    public override void checkLogin() { }
    public override void isRealNameAuth() { }
    public override void logout() { AppChannelHelper.LogoutCallBack(true); }
    public override void getUserInfo() { }
    public override void register() { }
    public override void notifyZone(string role_id, string role_name) { }
    public override void createRole(string role_name, string role_id) { }
    public override void pay(int uid, string username, string role, string serverId, int total_fee, int game_money, string out_trade_no, string subject, string body, string extension_info, string notify_url, string order_sign) { }
    public override void init()
    {
        //GameObject.Instantiate(Resources.Load("DebugLoginCanvas", typeof(GameObject)));
    }
    public override void showToast(string content) { }
}
