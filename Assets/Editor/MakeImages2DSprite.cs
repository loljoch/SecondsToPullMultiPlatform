using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Linq;

public class MakeImages2DSprite
{
    [MenuItem("Assets/Compress Texture")]
    public static void Execute()
    {
        foreach(var selected in Selection.instanceIDs)
        {
            string path = AssetDatabase.GetAssetPath(selected);

            TextureImporter importer = (TextureImporter)TextureImporter.GetAtPath(path);
            importer.wrapMode = TextureWrapMode.Clamp;
            importer.textureType = TextureImporterType.Sprite;
            importer.filterMode = FilterMode.Point;
            importer.spritePixelsPerUnit = 64;

            AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);
        }
    }
}
