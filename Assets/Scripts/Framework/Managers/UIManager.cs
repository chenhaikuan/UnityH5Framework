using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// using UnityEngine.Events;
using System;



public class UIMgr : UnitySingleton<UIMgr> {
    private Dictionary<string, UIControl> _DicALLUIForms;
    public GameObject canvas;
	string ui_prefab_root = "GUI/UIPrefabs/";
	
    public override void Awake()
    {
        base.Awake();
        this.canvas = GameObject.Find("Canvas");
        if (this.canvas == null) {
            Debug.LogError("UI manager load  Canvas failed!!!!!");
        }
    }

    public void ShowUI(string name, Transform parent, Action<UIControl> callback) {
        //GameObject ui_prefab = ResMgr.Instance.GetAssetCache<GameObject>(this.ui_prefab_root + name + ".prefab");
        // AssetBundle bundle = null;
        ResourceManager resMgr = ResourceManager.Instance;
        StartCoroutine(resMgr.LoadAssetBundleAsync( name, Application.streamingAssetsPath + "/",(bundle)=>{
            UnityEngine.Object ui_prefab  = resMgr.LoadAsset<GameObject>(name,name);
            GameObject ui_view = GameObject.Instantiate(ui_prefab) as GameObject;

            // GameObject ui_view = GameObject.Instantiate(ui_prefab);
            ui_view.name = name;
            if (parent == null)
            {
                parent = this.canvas.transform;
            }
            ui_view.transform.SetParent(parent, false);

            Type type = Type.GetType(name + "_Ctrl");
            UIControl control = (UIControl)ui_view.GetComponent(type);
            if (null == control)
            {
                control = (UIControl)ui_view.AddComponent(type);
            }
            callback(control);

        }));
        
        //GameObject ui_prefab = ResMgr.Instance.GetAssetCache<GameObject>(this.ui_prefab_root + name + ".prefab");
       
    }
    
    public void CloseUI(string name)
    {
        UIControl uiCtrl;
        _DicALLUIForms.TryGetValue(name,out uiCtrl);
        if(null == uiCtrl ) return;
        
        MonoBehaviour.Destroy(uiCtrl.gameObject);
        // BaseUIForm baseUiForm;                          //窗体基类
        //
        // //参数检查
        // if (string.IsNullOrEmpty(uiFormName)) return;
        // //“所有UI窗体”集合中，如果没有记录，则直接返回
        // _DicALLUIForms.TryGetValue(uiFormName,out baseUiForm);
        // if(baseUiForm==null ) return;
        // //根据窗体不同的显示类型，分别作不同的关闭处理
        // switch (baseUiForm.CurrentUIType.UIForms_ShowMode)
        // {
        //     case UIFormShowMode.Normal:
        //         //普通窗体的关闭
        //         ExitUIForms(uiFormName);
        //         break;
        //     case UIFormShowMode.ReverseChange:
        //         //反向切换窗体的关闭
        //         PopUIFroms();
        //         break;
        //     case UIFormShowMode.HideOther:
        //         //隐藏其他窗体关闭
        //         ExitUIFormsAndDisplayOther(uiFormName);
        //         break;
        //
        //     default:
        //         break;
        // }
    }

}
