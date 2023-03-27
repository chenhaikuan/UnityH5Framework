using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class BuildWebGL : EditorWindow
{
    [MenuItem("Tool/BuildWebGL")]
    private static void BundleAssetsBundle_Webgl()
    {
        BuildAssetsBundle(BuildTarget.WebGL);
    }
    private static void BuildAssetsBundle(BuildTarget target)
    {
        string packagePath = Application.streamingAssetsPath;
        if (packagePath.Length <= 0 && !Directory.Exists(packagePath))
        {
            return;
        }
        AssetBundleManifest manifest = BuildPipeline.BuildAssetBundles(packagePath, BuildAssetBundleOptions.UncompressedAssetBundle, target);
        foreach (string bundleName in manifest.GetAllAssetBundles())
        {
            Debug.Log(bundleName);
        }
    }
}
