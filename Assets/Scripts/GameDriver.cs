using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDriver : MonoBehaviour
{
    private bool CriDevelopMode;

    private void Awake()
    {
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
            VersionManager.Instance.SingletonInit();

            ResourceManager.CreateInstance();
        }
        yield return 0;

        EnterGame();
    }

    public void EnterGame()
    {
        UIManager.Instance.Init();
    }
}
