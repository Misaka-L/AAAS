using UnityEngine;
using UnityEditor;
using System.Collections;
using UnityEngine.UIElements;
using UnityEditor.Search;



public class AAASManager : EditorWindow
{
    int gameobjectButtonCount = 0;
    string toggleName = null;
    public GameObject[] targetGameObject = null;

    [MenuItem("Tools/CCES/AAAS")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(AAASManager));
    }

    void OnGUI()
    {        
        GUILayout.Label("���ý���");

        if (GUILayout.Button("�������󿪹�"))
        {
        }

        toggleName = EditorGUILayout.TextField("����", toggleName);

        //targetGameObject = (GameObject[])EditorGUILayout.ObjectField(targetGameObject, true);

        ScriptableObject target = this;
        SerializedObject so = new SerializedObject(target);
        SerializedProperty stringsProperty = so.FindProperty("targetGameObject");

        EditorGUILayout.PropertyField(stringsProperty, true);
        so.ApplyModifiedProperties();

        if (GUILayout.Button("����"))
        {
            Debug.Log(toggleName);
        }

        if (GUILayout.Button("�ر�"))
        {
            Close();
        }
    }
}