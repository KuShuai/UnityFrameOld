using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[CustomEditor(typeof(GameDriver))]
public class GameDriverEditor : Editor
{

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("ResourceManager Mode");
        EditorGUILayout.BeginVertical(GUI.skin.textArea);
        {
            EditorGUI.BeginChangeCheck();
            bool Deploy_AA = EditorPrefs.GetBool("Deploy_AA", false);
            Deploy_AA = EditorGUILayout.ToggleLeft("Deploy_AA", Deploy_AA);
            if (EditorGUI.EndChangeCheck())
            {
                EditorPrefs.SetBool("Deploy_AA", Deploy_AA);
                if (Deploy_AA)
                {
                    if (EditorPrefs.GetBool("Deploy_AB", false))
                    {
                        EditorPrefs.SetBool("Deploy_AB", false);
                    }
                    if (EditorPrefs.GetBool("Develop", true))
                    {
                        EditorPrefs.SetBool("Develop", false);
                    }
                }
            }
        }

        {
            EditorGUI.BeginChangeCheck();
            bool Deploy_AB = EditorPrefs.GetBool("Deploy_AB", false);
            Deploy_AB = EditorGUILayout.ToggleLeft("Deploy_AB", Deploy_AB);
            if (EditorGUI.EndChangeCheck())
            {
                EditorPrefs.SetBool("Deploy_AB", Deploy_AB);
                if (Deploy_AB)
                {
                    if (EditorPrefs.GetBool("Deploy_AA", false))
                    {
                        EditorPrefs.SetBool("Deploy_AA", false);
                    }
                    if (EditorPrefs.GetBool("Develop", true))
                    {
                        EditorPrefs.SetBool("Develop", false);
                    }
                }
            }
        }

        {
            EditorGUI.BeginChangeCheck();
            bool Develop = EditorPrefs.GetBool("Develop", true);
            Develop = EditorGUILayout.ToggleLeft("Develop", Develop);
            if (EditorGUI.EndChangeCheck())
            {
                EditorPrefs.SetBool("Develop", Develop);
                if (Develop)
                {
                    if (EditorPrefs.GetBool("Deploy_AA", false))
                    {
                        EditorPrefs.SetBool("Deploy_AA", false);
                    }
                    if (EditorPrefs.GetBool("Deploy_AB", false))
                    {
                        EditorPrefs.SetBool("Deploy_AB", false);
                    }
                }
            }
        }

        EditorGUILayout.EndVertical();



    }
}
