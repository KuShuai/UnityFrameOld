using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainStart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ResourceManager.CreateInstance();
        UIManager.Instance.Init();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            UIManager.OpenUI<UIPanel1>(UIPanelEnum.UIPanel1);
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            UIManager.OpenUI<UIPanel2>(UIPanelEnum.UIPanel2);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            UIManager.OpenUI<UIPanel3>(UIPanelEnum.UIPanel3);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            UIManager.OpenUI<UIPanel4>(UIPanelEnum.UIPanel4);
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            UIManager.CloseUI(UIPanelEnum.UIPanel1);
            UIManager.CloseUI(UIPanelEnum.UIPanel2);
            UIManager.CloseUI(UIPanelEnum.UIPanel3);
            UIManager.CloseUI(UIPanelEnum.UIPanel4);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            ReadStreamingAssetsExcelManager.Instance.CreateAllData();
        }
    }
}
