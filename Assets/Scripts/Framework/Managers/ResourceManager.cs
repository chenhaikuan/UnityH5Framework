using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class ResourceManager : UnitySingleton<ResourceManager> 
{
    private readonly Dictionary<string, AssetBundle> _loadedAssetBundles = new Dictionary<string, AssetBundle>();
    private readonly Dictionary<string, DateTime> _lastUsedTime = new Dictionary<string, DateTime>();
    
    // public IEnumerator LoadAssetBundleAsync(string bundleName, string path, Action<float> onProgress)
    // {
    //     if (_loadedAssetBundles.ContainsKey(bundleName))
    //     {
    //         _lastUsedTime[bundleName] = DateTime.Now;
    //         yield break;
    //     }
    //
    //     // Check if the AssetBundle is cached in the browser
    //     if (Caching.IsVersionCached(path, Hash128.Compute(bundleName)))
    //     {
    //         UnityWebRequest request = UnityWebRequestAssetBundle.GetAssetBundle(path, Hash128.Compute(bundleName));
    //         yield return request.SendWebRequest();
    //
    //         if (request.result != UnityWebRequest.Result.Success)
    //         {
    //             Debug.LogError($"Failed to load AssetBundle: {bundleName}");
    //             yield break;
    //         }
    //
    //         AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(request);
    //         if (bundle == null)
    //         {
    //             Debug.LogError($"Failed to load AssetBundle: {bundleName}");
    //             yield break;
    //         }
    //
    //         _loadedAssetBundles.Add(bundleName, bundle);
    //         _lastUsedTime.Add(bundleName, DateTime.Now);
    //
    //         yield break;
    //     }
    //
    //     UnityWebRequest request = UnityWebRequestAssetBundle.GetAssetBundle(path);
    //     yield return request.SendWebRequest();
    //
    //     if (request.result != UnityWebRequest.Result.Success)
    //     {
    //         Debug.LogError($"Failed to load AssetBundle: {bundleName}");
    //         yield break;
    //     }
    //
    //     AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(request);
    //     if (bundle == null)
    //     {
    //         Debug.LogError($"Failed to load AssetBundle: {bundleName}");
    //         yield break;
    //     }
    //
    //     // Add the AssetBundle to the cache
    //     Caching.Add(path, bundle);
    //
    //     _loadedAssetBundles.Add(bundleName, bundle);
    //     _lastUsedTime.Add(bundleName, DateTime.Now);
    // }
    public IEnumerator LoadAssetBundle(string bundleName, string path, Action<float> onProgress)
    {
        if (_loadedAssetBundles.ContainsKey(bundleName))
        {
            _lastUsedTime[bundleName] = DateTime.Now;
            yield break;
        }

        UnityWebRequest request = UnityWebRequestAssetBundle.GetAssetBundle(path + bundleName);
        var operation = request.SendWebRequest();

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);  
            if (onProgress != null)
                onProgress(progress);

            yield return null;
        }

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError($"Failed to load AssetBundle: {bundleName}");
            yield break;
        }

        var bundle = DownloadHandlerAssetBundle.GetContent(request);
        if (bundle == null)
        {
            Debug.LogError($"Failed to load AssetBundle: {bundleName}");
            yield break;
        }

        _loadedAssetBundles.Add(bundleName, bundle);
        _lastUsedTime.Add(bundleName, DateTime.Now);
    }
    
    public IEnumerator LoadAssetBundleAsync(string bundleName, string path, Action<AssetBundle> callback)
    {
        if (_loadedAssetBundles.ContainsKey(bundleName))
        {
            _lastUsedTime[bundleName] = DateTime.Now;
            yield break;
        }

        
        UnityWebRequest request = UnityWebRequestAssetBundle.GetAssetBundle(path + bundleName);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError($"Failed to load AssetBundle: {bundleName}");
            yield break;
        }

        var bundle = DownloadHandlerAssetBundle.GetContent(request);
        if (bundle == null)
        {
            Debug.LogError($"Failed to load AssetBundle: {bundleName}");
            yield break;
        }

        _loadedAssetBundles.Add(bundleName, bundle);
        _lastUsedTime.Add(bundleName, DateTime.Now);
        callback(bundle);
    }

    public void UnloadAssetBundle(string bundleName)
    {
        if (_loadedAssetBundles.TryGetValue(bundleName, out var bundle))
        {
            bundle.Unload(true);
            _loadedAssetBundles.Remove(bundleName);
            _lastUsedTime.Remove(bundleName);
        }
    }

    private void Update()
    {
        // Check all loaded AssetBundles and unload those that have not been used for a while
        var keysToRemove = new List<string>();
        foreach (var pair in _lastUsedTime)
        {
            if ((DateTime.Now - pair.Value).TotalMinutes > 10)
            {
                keysToRemove.Add(pair.Key);
            }
        }

        foreach (var key in keysToRemove)
        {
            UnloadAssetBundle(key);
        }
    }

    public T LoadAsset<T>(string bundleName, string assetName) where T : UnityEngine.Object
    {
        if (_loadedAssetBundles.TryGetValue(bundleName, out var bundle))
        {
            _lastUsedTime[bundleName] = DateTime.Now;
            var asset = bundle.LoadAsset<T>(assetName);
            if (asset == null)
            {
                Debug.LogError($"Failed to load asset: {assetName}");
            }
            return asset;
        }
        else
        {
            Debug.LogError($"AssetBundle not loaded: {bundleName}");
            return null;
        }
    }

    public T[] LoadAllAssets<T>(string bundleName) where T : UnityEngine.Object
    {
        if (_loadedAssetBundles.TryGetValue(bundleName, out var bundle))
        {
            _lastUsedTime[bundleName] = DateTime.Now;
            var assets = bundle.LoadAllAssets<T>();
            if (assets.Length == 0)
            {
                Debug.LogError($"Failed to load assets from bundle: {bundleName}");
            }
            return assets;
        }
        else
        {
            Debug.LogError($"AssetBundle not loaded: {bundleName}");
            return null;
        }
    }
}
