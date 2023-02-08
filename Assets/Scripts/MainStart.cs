using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainStart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ResourceManager.CreateInstance();
        UIManager.Instance.OpenUI("");
    }
}
