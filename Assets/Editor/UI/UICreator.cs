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

    // 绘制我们的节点
    public void OnGUI() {
        GUILayout.Label("选择一个UI视图");
        if (Selection.activeGameObject != null) {
            GUILayout.Label(Selection.activeGameObject.name);
            GUILayout.Label(filePath + Selection.activeGameObject.name + "_Ctrl.cs");

        }
        else {
            GUILayout.Label("没有选中的UI节点，无法生成");
        }

        if (GUILayout.Button("生成UI代码文件")) {
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
