using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPanel4 : UIPanel
{
    private RectTransform R_Create_RectTransform = null;
    private InputField R_CreateSqliteName_InputField = null;
    private InputField R_CreateTableName_InputField = null;
    private RectTransform R_CreateItem_RectTransform = null;
    private Button R_CreateAdd_Button = null;
    private Button R_Create_Button = null;

    public override void OnAwake()
    {
        base.OnAwake();
        R_Create_RectTransform = GetUIWidget<RectTransform>("R_Create_RectTransform");
        R_CreateSqliteName_InputField = GetUIWidget<InputField>("R_CreateSqliteName_InputField");
        R_CreateTableName_InputField = GetUIWidget<InputField>("R_CreateTableName_InputField");
        R_CreateItem_RectTransform = GetUIWidget<RectTransform>("R_CreateItem_RectTransform");
        R_CreateAdd_Button = GetUIWidget<Button>("R_CreateAdd_Button");
        R_Create_Button = GetUIWidget<Button>("R_Create_Button");
    }

    public override void OnStart()
    {
        base.OnStart();
        R_CreateAdd_Button.onClick.AddListener(()=> {
            RectTransform rect = GameObject.Instantiate(R_CreateItem_RectTransform, R_Create_RectTransform);
            rect.SetSiblingIndex(R_Create_RectTransform.childCount - 2);
        });

        R_Create_Button.onClick.AddListener(()=> {
            List<string> keysName = new List<string>();
            List<string> keysType = new List<string>();
            for (int i = 2; i < R_Create_RectTransform.childCount-1; i++)
            {
                keysType.Add(R_Create_RectTransform.GetChild(i).GetChild(0).GetComponent<InputField>().text);
                keysName.Add(R_Create_RectTransform.GetChild(i).GetChild(1).GetComponent<InputField>().text);
            }
            SQLiteFunction.CreateOperation(R_CreateSqliteName_InputField.text,
                R_CreateTableName_InputField.text,
                keysName.ToArray(),
                keysType.ToArray()
                );
        });
    }

    public override void OnEvent(EventID event_id, EventParam param)
    {
        base.OnEvent(event_id, param);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }

    public override void OnClose()
    {
        base.OnClose();
    }
}
