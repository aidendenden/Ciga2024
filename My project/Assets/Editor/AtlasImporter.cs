#if UNITY_EDITOR
using System.IO;
using UnityEditor;
using UnityEngine;


public class AtlasImporter : AssetPostprocessor
{
    public void OnPreprocessTexture()
    {
        string dirName = Path.GetDirectoryName(assetPath);
        string fileName = Path.GetFileName(assetPath);


        Debug.Log(assetPath);
        if (assetPath.Contains(".png") || assetPath.Contains(".jpg"))
        {
            TextureImporter textureImporter = (TextureImporter)assetImporter;
            textureImporter.filterMode = FilterMode.Point;
            EditorUtility.SetDirty(textureImporter);
            textureImporter.SaveAndReimport();
        }
        // if (Path.GetDirectoryName(dirName)!.Contains(Path.Combine("UI")) && fileName.Contains("_atlas"))
        // {
        //     TextureImporter textureImporter = (TextureImporter)assetImporter;
        //     textureImporter.maxTextureSize = 4096;
        //     textureImporter.textureType = TextureImporterType.Default;
        //     textureImporter.mipmapEnabled = false;
        //     textureImporter.isReadable = false;
        //     textureImporter.filterMode = FilterMode.Point;
        //     textureImporter.npotScale = TextureImporterNPOTScale.None;
        //     textureImporter.alphaIsTransparency = true;
        //     // 暂时不用设置纹理格式
        //     TextureImporterPlatformSettings settings = textureImporter.GetDefaultPlatformTextureSettings();
        //     // settings.format = TextureImporterFormat.ASTC_4x4;
        //     // settings.overridden = true;
        //     settings.textureCompression = TextureImporterCompression.Uncompressed;
        //     //settings.compressionQuality = 100;
        //     textureImporter.SetPlatformTextureSettings(settings);
        //     EditorUtility.SetDirty(textureImporter);
        //     textureImporter.SaveAndReimport();
        //     //AssetDatabase.Refresh();
        // }
    }
}

#endif