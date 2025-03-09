using System.IO;
using UnityEngine;

public class AssetBundles : MonoBehaviour
{
    string folderPath = "AssetBundles";
    string bundleFileName = "custom_unit";
    string customUnitName = "CustomUnit";

    string combinedPath;
    private AssetBundle customEnemyBundle;

    private void Start()
    {
        LoadBundle();
        LoadCustomUnit();
    }

    private void LoadCustomUnit()
    {
        if (customEnemyBundle == null)
            return;

        var go = customEnemyBundle.LoadAsset<GameObject>(customUnitName);
        if (go)
            Instantiate(go, Vector3.right * 2, Quaternion.identity);
    }


    private void LoadBundle()
    {
        combinedPath = Path.Combine(Application.streamingAssetsPath, folderPath, bundleFileName);
        if (!File.Exists(combinedPath))
        {
            Debug.LogError("AssetBundle not found: " + combinedPath);
            return;
        }

        customEnemyBundle = AssetBundle.LoadFromFile(combinedPath);
    }
}
