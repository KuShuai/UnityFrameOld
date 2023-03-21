using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainStart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            UIManager.OpenUIPanel(UIPanelEnum.UIPanel1);
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            UIManager.OpenUIPanel(UIPanelEnum.UIPanel2);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            UIManager.OpenUIPanel(UIPanelEnum.UIPanel3);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            UIManager.OpenUIPanel(UIPanelEnum.UIPanel4);
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            UIManager.CloseUI(UIPanelEnum.UIPanel1);
            UIManager.CloseUI(UIPanelEnum.UIPanel2);
            UIManager.CloseUI(UIPanelEnum.UIPanel3);
            UIManager.CloseUI(UIPanelEnum.UIPanel4);
        }

        //if (Input.GetKeyDown(KeyCode.P))
        //{
        //    ReadStreamingAssetsExcelManager.Instance.CreateAllData();
        //}
    }
}
