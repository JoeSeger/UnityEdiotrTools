﻿using System;
 using System.Collections.Generic;
using System.Linq;
 using CCZ.Code._2DAnimator.Animation.SlicedSprites;
 using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Editor.Tools.SpriteTools.RenameSpriteSlice
{

    public class RenameSpriteSheetSliceData : ScriptableObject
    {
        
        public SlicedSpriteData currerntSlicedSpriteData;
        public Texture2D spriteSheetTexture;
        public bool isdirty;

        public List<SlicedSpriteData> currentSprites;

        public void Reset()
        {
            spriteSheetTexture = null;
            currentSprites = null;
            currerntSlicedSpriteData = null;
        }

        public void Set(Texture2D settingtextuer )
        {
            spriteSheetTexture = settingtextuer;
            currentSprites = GetSpriteDataList();
        }
        
        private bool CheckIfObjectIsInTextureField => spriteSheetTexture == null;

        public string Texture2DFieldKey => CheckIfObjectIsInTextureField
            ? RenameSpriteSliceSystem.NoTexture2DWaring
            : spriteSheetTexture.name;

     
        public List<SlicedSpriteData> GetSpriteDataList()
        { 
            var sprites = GetAllSpritesObjectsFromTexture(out var filepath) ;
            var id = 0;
            return (from Sprite sprite in sprites select new SlicedSpriteData
            {
                SpriteTexture = spriteSheetTexture,
                ID = id++,
                orginName = sprite.name,
                slicedSprite = sprite, 
                textureName = spriteSheetTexture.name, 
                filePath = filepath
            }).ToList();
        }
        
        
        
        
        
        
        public IEnumerable<Object> GetAllSpritesObjectsFromTexture(out string spriteSheetTextureAssetDataBasePath)
        {
             spriteSheetTextureAssetDataBasePath = AssetDatabase.GetAssetPath(spriteSheetTexture);
            return AssetDatabase.LoadAllAssetRepresentationsAtPath(spriteSheetTextureAssetDataBasePath);;
        }

     
      
    }
}