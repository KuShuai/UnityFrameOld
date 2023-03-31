using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDriver : MonoBehaviour
{
    public static GameDriver Inst { private set; get; }
    private bool CriDevelopMode;

    public bool DevLoginMode = false;
    public string[] HostURL = null;
    public bool Login = false;

    private void Awake()
    {
        Inst = this;


        EventManager.Instance.SingletonInit();

        CriDevelopMode = false;
    }

    void Start()
    {
        StartCoroutine(InitialData());   
    }

    IEnumerator InitialData()
    {
        if (!CriDevelopMode)
        {
            //AppChannelHelper.LoadAppChannel();

            VersionManager.Instance.Init();
    
        }
        yield return 0;

        Debug.LogError("******检查版本");
        //检查版本
        //VersionManager.Instance.CheckingVersion();


    }

    public void EnterGame()
    {
        ResourceManager.CreateInstance();
        ResourceManager.Instance.Init();

        LuaScriptManager.Instance.Init();

        UIManager.Instance.Init();
    }
}
