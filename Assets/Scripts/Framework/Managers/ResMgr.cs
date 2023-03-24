using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Networking;


public class ResMgr : UnitySingleton<ResMgr> {
    public override void Awake() {
        base.Awake();
        // this.gameObject.AddComponent<AssetBundleManager>();
    }
		
//     public T GetAssetCache<T>(string name) where T : UnityEngine.Object
//     {
// #if UNITY_EDITOR
//         // if (AssetBundleConfig.IsEditorMode)
//         {
//             // string path = AssetBundleUtility.PackagePathToAssetsPath(name);
// 			string path = "Assets/AssetsPackage/" + name;
//             // Debug.Log(path);
//             UnityEngine.Object target = UnityEditor.AssetDatabase.LoadAssetAtPath<T>(path);
//             return target as T;
//         }
// #endif
//         
//         UnityWebRequest request = UnityWebRequestAssetBundle.GetAssetBundle(name) ;
//         AssetBundle ab = (request.downloadHandler as DownloadHandlerAssetBundle).assetBundle;
//         // a(name);
//         //yield return uwr;
//         //AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(uwr);
//         //return AssetBundleManager.Instance.GetAssetCache(name) as T;
//     }


    public IEnumerator GetAssetbundle(string url, string name, AssetBundle bundle, Action<AssetBundle> callback)
    {

        var request = UnityWebRequestAssetBundle.GetAssetBundle(Application.streamingAssetsPath + "/" + name);//获取动态加载路径
        yield return request.SendWebRequest(); //等待发送web请求
        //AssetBundle bundle = (request.downloadHandler as DownloadHandlerAssetBundle).assetBundle;
        
        yield return bundle = DownloadHandlerAssetBundle.GetContent(request);//下载AssetBundle包
        callback(bundle);
        //yield return bundle;
        //var loadAsset = bundle.LoadAssetAsync<GameObject>(name);  //加载类型
        //yield return loadAsset; //等待完成
        //var prefab = loadAsset.asset;
        //var go = GameObject.Instantiate(prefab); //克隆需要加载的包
        //callback(go);
        //bundle.Unload(false);
        //Resources.UnloadUnusedAssets();//加载了包  就要卸载
    }
}
