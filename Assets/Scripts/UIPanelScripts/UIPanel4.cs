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
    private InputField R_AddSqliteName_InputField = null;
    private InputField R_AddTableName_InputField = null;
    private Button R_AddOpen_Button = null;
    private RectTransform R_AddItem_RectTransform = null;
    private RectTransform R_Add_RectTransform = null;
    private Button R_Add_Button = null;
    private InputField R_DeleteSqliteName_InputField = null;
    private InputField R_DeleteTableName_InputField = null;
    private InputField R_DeleteWhereInfo_InputField = null;
    private Button R_DeleteItemAdd_Button = null;
    private Button R_Delete_Button = null;
    private InputField R_ChangeSqliteName_InputField = null;
    private InputField R_ChangeTableName_InputField = null;
    private InputField R_ChangeSetInfo_InputField = null;
    private InputField R_ChangeWhereInfo_InputField = null;
    private Button R_ChangeItemAdd_Button = null;
    private Button R_Change_Button = null;
    private InputField R_SelectSqliteName_InputField = null;
    private InputField R_SelectTableName_InputField = null;
    private InputField R_SelectSetInfo_InputField = null;
    private InputField R_SelectWhereInfo_InputField = null;
    private Button R_SelectItemAdd_Button = null;
    private Button R_Select_Button = null;
    private Text R_SelectResult_Text = null;

    public override void OnPreLoad()
    {
        base.OnPreLoad();

        R_Create_RectTransform = GetUIWidget<RectTransform>("R_Create_RectTransform");
        R_CreateSqliteName_InputField = GetUIWidget<InputField>("R_CreateSqliteName_InputField");
        R_CreateTableName_InputField = GetUIWidget<InputField>("R_CreateTableName_InputField");
        R_CreateItem_RectTransform = GetUIWidget<RectTransform>("R_CreateItem_RectTransform");
        R_CreateAdd_Button = GetUIWidget<Button>("R_CreateAdd_Button");
        R_Create_Button = GetUIWidget<Button>("R_Create_Button");
        R_AddSqliteName_InputField = GetUIWidget<InputField>("R_AddSqliteName_InputField");
        R_AddTableName_InputField = GetUIWidget<InputField>("R_AddTableName_InputField");
        R_AddOpen_Button = GetUIWidget<Button>("R_AddOpen_Button");
        R_AddItem_RectTransform = GetUIWidget<RectTransform>("R_AddItem_RectTransform");
        R_Add_RectTransform = GetUIWidget<RectTransform>("R_Add_RectTransform");
        R_Add_Button = GetUIWidget<Button>("R_Add_Button");
        R_DeleteSqliteName_InputField = GetUIWidget<InputField>("R_DeleteSqliteName_InputField");
        R_DeleteTableName_InputField = GetUIWidget<InputField>("R_DeleteTableName_InputField");
        R_DeleteWhereInfo_InputField = GetUIWidget<InputField>("R_DeleteWhereInfo_InputField");
        R_DeleteItemAdd_Button = GetUIWidget<Button>("R_DeleteItemAdd_Button");
        R_Delete_Button = GetUIWidget<Button>("R_Delete_Button");
        R_ChangeSqliteName_InputField = GetUIWidget<InputField>("R_ChangeSqliteName_InputField");
        R_ChangeTableName_InputField = GetUIWidget<InputField>("R_ChangeTableName_InputField");
        R_ChangeSetInfo_InputField = GetUIWidget<InputField>("R_ChangeSetInfo_InputField");
        R_ChangeWhereInfo_InputField = GetUIWidget<InputField>("R_ChangeWhereInfo_InputField");
        R_ChangeItemAdd_Button = GetUIWidget<Button>("R_ChangeItemAdd_Button");
        R_Change_Button = GetUIWidget<Button>("R_Change_Button");
        R_SelectSqliteName_InputField = GetUIWidget<InputField>("R_SelectSqliteName_InputField");
        R_SelectTableName_InputField = GetUIWidget<InputField>("R_SelectTableName_InputField");
        R_SelectSetInfo_InputField = GetUIWidget<InputField>("R_SelectSetInfo_InputField");
        R_SelectWhereInfo_InputField = GetUIWidget<InputField>("R_SelectWhereInfo_InputField");
        R_SelectItemAdd_Button = GetUIWidget<Button>("R_SelectItemAdd_Button");
        R_Select_Button = GetUIWidget<Button>("R_Select_Button");
        R_SelectResult_Text = GetUIWidget<Text>("R_SelectResult_Text");
    }






    /// <summary>
    /// 数据库添加的时候用到，存储所有的字段名称
    /// </summary>
    private object[] keyValue;

    public override void OnLoad()
    {
        base.OnLoad();

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

        R_AddOpen_Button.onClick.AddListener(()=> {

            R_Add_RectTransform.gameObject.SetActive(false);
            string info = SQLiteFunction.SelectOperation(R_AddSqliteName_InputField.text,
                R_AddTableName_InputField.text,
                out keyValue);
            if (string.IsNullOrEmpty(info))
            {
                return;
            }
            string[] infos = info.Split('|');
            int childCount = R_Add_RectTransform.childCount;
            Debug.Log(childCount);
            for (int i = 0; i < childCount-1; i++)
            {
                Debug.Log(R_Add_RectTransform.childCount + "     " + childCount);
                DestroyImmediate(R_Add_RectTransform.GetChild(0).gameObject);
            }
            for (int i = 0; i < infos.Length; i++)
            {
                string[] keysInfo = infos[i].Split(':');
                if (keysInfo.Length == 2)
                {
                    RectTransform rect = Instantiate(R_AddItem_RectTransform, R_Add_RectTransform);
                    rect.GetChild(0).GetComponent<Text>().text = keysInfo[0] + "(" + keysInfo[1].ToLower() + ")";
                    rect.gameObject.SetActive(true);
                }
            }
            R_Add_RectTransform.GetChild(0).SetAsLastSibling();

            R_Add_RectTransform.gameObject.SetActive(true);
        });

        R_Add_Button.onClick.AddListener(()=> {

            List<object> values = new List<object>();
            for (int i = 0; i < R_Add_RectTransform.childCount-1; i++)
            {
                values.Add(R_Add_RectTransform.GetChild(i).GetChild(1).GetComponent<InputField>().text);
            }
            SQLiteFunction.InsertOperation(R_AddSqliteName_InputField.text,
                R_AddTableName_InputField.text,
                keyValue,
                values.ToArray()
                );

        });

        R_Delete_Button.onClick.AddListener(()=> {

            SQLiteFunction.DeleteOperation(R_DeleteSqliteName_InputField.text,
                R_DeleteTableName_InputField.text,
                R_DeleteWhereInfo_InputField.text
                );
        
        });

        R_Change_Button.onClick.AddListener(()=> {
            SQLiteFunction.UpdateOperation(R_ChangeSqliteName_InputField.text,
                R_ChangeTableName_InputField.text,
                R_ChangeSetInfo_InputField.text,
                R_ChangeWhereInfo_InputField.text);
        });

        R_Select_Button.onClick.AddListener(()=> {

            R_SelectResult_Text.text = SQLiteFunction.SelectOperation(
                R_SelectSqliteName_InputField.text,
                R_SelectTableName_InputField.text,
                R_SelectSetInfo_InputField.text,
                R_SelectWhereInfo_InputField.text);
            
        });
    }

}
