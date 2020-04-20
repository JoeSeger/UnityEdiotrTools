using UnityEditor;
using UnityEngine;

namespace Editor.Tools.SpriteTools.RenameSpriteSlice
{
    public static class RenameSpriteSliceSystem
    {
        public const string NoTexture2DWaring = "There is No Texture2D in Texture2D Field ";
        
        
        [MenuItem("Assets/Create/EditorTools/Sprite Slice System/Rename Sprite Sheet SliceSystem/Rename Sprite Sheet SliceData")]
        public static RenameSpriteSheetSliceData Create()
        
        {  var asset = ScriptableObject.CreateInstance<RenameSpriteSheetSliceData>();
            AssetDatabase.CreateAsset(asset, "Assets/Resources/SliceSpriteData/RenameSpriteSheetSliceData.asset");
            AssetDatabase.SaveAssets();
            return asset;
        }

    }
}
