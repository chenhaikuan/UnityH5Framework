using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetBundleManager : MonoBehaviour
{
    private readonly Dictionary<string, AssetBundle> _loadedAssetBundles = new Dictionary<string, AssetBundle>();
    private readonly Dictionary<string, DateTime> _lastUsedTime = new Dictionary<string, DateTime>();

    public IEnumerator LoadAssetBundleAsync(string bundleName, string path, Action<float> onProgress)
    {
        if (_loadedAssetBundles.ContainsKey(bundleName))
        {
            _lastUsedTime[bundleName] = DateTime.Now;
            yield break;
        }

        var request = AssetBundle.LoadFromFileAsync(path);
        while (!request.isDone)
        {
            onProgress?.Invoke(request.progress);
            yield return null;
        }

        var bundle = request.assetBundle;
        if (bundle == null)
        {
            Debug.LogError($"Failed to load AssetBundle: {bundleName}");
            yield break;
        }

        _loadedAssetBundles.Add(bundleName, bundle);
        _lastUsedTime.Add(bundleName, DateTime.Now);
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
            return bundle.LoadAsset<T>(assetName);
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
            return bundle.LoadAllAssets<T>();
        }
        else
        {
            Debug.LogError($"AssetBundle not loaded: {bundleName}");
            return null;
        }
    }
}
