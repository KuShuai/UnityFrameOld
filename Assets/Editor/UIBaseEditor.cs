using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(UIWidget), true)]
public class UIWidgetEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        UIWidget panel = target as UIWidget;
        SerializedObject so = new SerializedObject(target);

        SerializedProperty so_links = so.FindProperty("Links");

        if (GUILayout.Button("AttachAllWidget (R_*)"))
        {
            so_links.ClearArray();

            UIWidget[] uiWidgets = panel.gameObject.GetComponentsInChildren<UIWidget>();
            Dictionary<int, Transform> parentsDict = new Dictionary<int, Transform>();
            for (int i = 0; i < uiWidgets.Length; ++i)
            {
                parentsDict.Add(uiWidgets[i].transform.GetHashCode(), uiWidgets[i].transform);
            }

            Transform[] childs = panel.gameObject.GetComponentsInChildren<Transform>(true);
            for (int i = 0; i < childs.Length; i++)
            {
                GameObject obj = childs[i].gameObject;
                if (obj.name.StartsWith("R_"))
                {
                    Debug.Log("Attach:" + obj.name);

                    Transform trans = obj.transform.parent;
                    while (trans != null)
                    {
                        int hashCode = trans.GetHashCode();
                        if (parentsDict.ContainsKey(hashCode) && parentsDict[hashCode] == panel.transform)
                        {
                            AttachWidget(obj, panel.transform, so_links);
                            break;
                        }
                        trans = trans.parent;
                    }
                }
            }
        }



        if (GUILayout.Button("Copy All Widget Name"))
        {
            StringBuilder content = new StringBuilder();
            for (int i = 0; i < so_links.arraySize; i++)
            {
                var item = so_links.GetArrayElementAtIndex(i);
                var name = item.FindPropertyRelative("Name");
                string[] nameSplits = name.stringValue.Split('_');
                content.AppendLine(string.Format("private {0} {1} = null;", nameSplits[nameSplits.Length - 1], name.stringValue));
            }
            content.AppendLine();
            content.AppendLine("    public override void OnPreLoad()");
            content.AppendLine("    {");
            content.AppendLine("        base.OnPreLoad();");
            for (int i = 0; i < so_links.arraySize; i++)
            {
                var item = so_links.GetArrayElementAtIndex(i);
                var name = item.FindPropertyRelative("Name");
                string[] nameSplits = name.stringValue.Split('_');

                content.AppendLine(string.Format("    {0} = GetUIWidget<{1}>(\"{0}\");", name.stringValue, nameSplits[nameSplits.Length - 1]));
            }
            content.AppendLine("    }");
            GUIUtility.systemCopyBuffer = content.ToString();
        }

        if (GUILayout.Button("Copy All Item GetUIWidget"))
        {
            StringBuilder content = new StringBuilder();
            for (int i = 0; i < so_links.arraySize; i++)
            {
                var item = so_links.GetArrayElementAtIndex(i);
                var name = item.FindPropertyRelative("Name");
                string[] nameSplits = name.stringValue.Split('_');

                content.AppendLine(string.Format("    {0} = GetUIWidget<{1}>(\"{0}\");", name.stringValue, nameSplits[nameSplits.Length - 1]));
            }
            GUIUtility.systemCopyBuffer = content.ToString();
        }



        EditorGUILayout.Space();

        for (int i = 0; i < so_links.arraySize; i++)
        {
            SerializedProperty so_link_item = so_links.GetArrayElementAtIndex(i);
            if (DrawItem(so_link_item))
            {
                so_links.DeleteArrayElementAtIndex(i);
                break;
            }
        }
        so.ApplyModifiedProperties();//将对属性的修改应用到对象上so.ApplyModifiedPropertiesWithoutUndo();
    }

    void AttachWidget(GameObject go,Transform trans,SerializedProperty so_links)
    {
        if (go != null)
        {
            Transform t = go.transform.parent;
            bool inherited = false;
            do
            {
                if (t == trans)
                {
                    inherited = true;
                    break;
                }
                t = t.parent;
            } while (t != null);

            if (inherited)
            {
                so_links.InsertArrayElementAtIndex(so_links.arraySize);
                SerializedProperty so_link_item = so_links.GetArrayElementAtIndex(so_links.arraySize - 1);

                SerializedProperty so_id = so_link_item.FindPropertyRelative("Name");
                SerializedProperty so_uiWidget = so_link_item.FindPropertyRelative("UIWidget");

                so_id.stringValue = go.name;
                so_uiWidget.objectReferenceValue = (Object)go;
            }
        }
    }

    bool DrawItem(SerializedProperty so_link_item)
    {
        bool to_delete = false;

        EditorGUILayout.BeginHorizontal(GUI.skin.textArea);

        if (GUILayout.Button("X",GUILayout.Width(20)))
        {
            to_delete = true;
        }

        SerializedProperty so_id = so_link_item.FindPropertyRelative("Name");
        SerializedProperty so_uiWidget = so_link_item.FindPropertyRelative("UIWidget");

        EditorGUILayout.LabelField(so_id.stringValue, GUILayout.MinWidth(20));
        EditorGUILayout.ObjectField(so_uiWidget.objectReferenceValue, typeof(GameObject), true);

        EditorGUILayout.EndHorizontal();


        return to_delete;
    }
}

//SerizlizedObject 序列化对象
//UnityEngine.Object
//在Editor中所有UnityEngine.Object都将转化为SerializedObject进行处理
//在面对UnityEngine.Object时，它将另存为二进制或者YAML格式的文本数据。SerializedObject负责这些序列化


//SerializedProperty序列化属性