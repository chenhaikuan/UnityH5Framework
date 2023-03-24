using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIControl : MonoBehaviour
{
    public Dictionary<string, GameObject> view = new Dictionary<string, GameObject>();

    private void LoadAllObjectsToView(GameObject root, string path) {
        foreach (Transform tf in root.transform) {
            if (this.view.ContainsKey(path + tf.gameObject.name)) {
                // Debug.LogWarning("Warning object is exist:" + path + tf.gameObject.name + "!");
                continue;
            }
            this.view.Add(path + tf.gameObject.name, tf.gameObject);
            this.LoadAllObjectsToView(tf.gameObject, path + tf.gameObject.name + "/");
        }
    }

    // 生成视图 view  path--->gameObject
    public virtual void Awake() {
        this.LoadAllObjectsToView(this.gameObject, "");
    }
    // end

    // 放我们的开发者来挂我们的按钮事件;
    public void addButtonListener(string viewName, UnityAction onclick) {
        Button bt = this.view[viewName].GetComponent<Button>();
        if (bt == null) {
            Debug.LogWarning("UI_manager add_button_listener: not Button Component!");
            return;
        }

        bt.onClick.AddListener(onclick);
    }

}
