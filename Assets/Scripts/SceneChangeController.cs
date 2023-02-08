using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneChangeController : MonoSingleton<SceneChangeController>,IMonoSingleton
{
    public void SingletonInit()
    {
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            SceneManager.LoadScene("Simple");
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            SceneManager.LoadScene("SampleScene");
        }
    }
}
