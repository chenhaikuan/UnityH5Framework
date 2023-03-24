#if UNITY_EDITOR 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;


public class UICreator : EditorWindow {
    private static string filePath = "/Scripts/Game/UIControllers/";

    [MenuItem("Tool/UICreator")]
    public static void CreateUI() {
        UICreator win = EditorWindow.GetWindow<UICreator>();
        win.titleContent.text = "UICreator";
        win.Show();
    }

    // �������ǵĽڵ�
    public void OnGUI() {
        GUILayout.Label("ѡ��һ��UI��ͼ");
        if (Selection.activeGameObject != null) {
            GUILayout.Label(Selection.activeGameObject.name);
            GUILayout.Label(filePath + Selection.activeGameObject.name + "_Ctrl.cs");

        }
        else {
            GUILayout.Label("û��ѡ�е�UI�ڵ㣬�޷�����");
        }

        if (GUILayout.Button("����UI�����ļ�")) {
            if (Selection.activeGameObject != null) {
                string className = Selection.activeGameObject.name + "_Ctrl";
                UICreatorUtil.GenUICtrlFile(filePath, className);

                AssetDatabase.Refresh();
            }
        }
    }

    public void OnSelectionChange() {
        this.Repaint();
    }
}
#endif
