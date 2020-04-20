using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Editor.Tools.SpriteTools
{
    public static class TextureTools
    {
        public static IEnumerable<object> GetAllSpritesObjectsFromTexture(this Texture2D texture2D, out string spriteSheetTextureAssetDataBasePath)
        {
            spriteSheetTextureAssetDataBasePath = AssetDatabase.GetAssetPath(texture2D);
            return AssetDatabase.LoadAllAssetRepresentationsAtPath(spriteSheetTextureAssetDataBasePath);;
        }
    }
}