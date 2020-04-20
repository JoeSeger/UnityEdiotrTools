﻿using System;
 using System.Collections.Generic;
 using CCZ.Code._2DAnimator.Animation.SlicedSprites;
 using CCZ.Code.Factions;
 using CCZ.Code.Factions.Units;
 using CCZ.Code.Factions.Units.Direction;
 using CCZ.Code.Factions.Units.States;
 using CCZ.Code.Tools.Strings;
 using Editor.Tools.UIElements;
using UnityEditor;
 using UnityEngine;
 using UnityEngine.UIElements;
 using Object = System.Object;

 namespace Editor.Tools.SpriteTools.RenameSpriteSlice
 {

     [CustomPropertyDrawer(typeof(SlicedSpriteData))]

     public class SpriteSliceDataPropertyDrawer : PropertyDrawer
     {

         public VisualElement root;
         public VisualElement propertyFiledContainer;


         public string GrabGenName(SerializedProperty property)
         {

             var factionEnm = (FactionData.Type) property.FindPropertyRelative("factionType").enumValueIndex - 1;
             var unitTypeEnum = (UnitData.Type) property.FindPropertyRelative("unitType").enumValueIndex - 1;
             var animatingDirectionEnum =
                 (DirectionData.Direction) property.FindPropertyRelative("animatingDirection").enumValueIndex - 1;

             var settingName = property.FindPropertyRelative("unitName");
             var nameCheck = !string.IsNullOrEmpty(settingName.stringValue);
             var unitName = nameCheck ? settingName.stringValue : "Please Set Unit Name";

             var animatingState = (UnitState.Type) property.FindPropertyRelative("animatingState").enumValueIndex - 1;


             return StringTools.ConstructedString(
                 StringTools.StringConstructionType.UnderScore,
                 factionEnm.ToString(),
                 unitTypeEnum.ToString(),
                 unitName,
                 animatingDirectionEnum.ToString(),
                 animatingState.ToString(),
                 property.FindPropertyRelative("animationFrameNumber").intValue.ToString()
             );
         }


         public override VisualElement CreatePropertyGUI(SerializedProperty property)
         {

             var generateNameButton = new Button()
             {
                 text = "Generate New Sprite Name "
             };
             var changeNameButton = new Button()
             {
                 text = "Change Sprite Asset Name to New name"
             };

             generateNameButton.RegisterCallback<MouseCaptureEvent>((evt) => { ClickedButtON(property); });
             changeNameButton.RegisterCallback<MouseCaptureEvent>((evt) => { ApplyRenames(property); });
             root = new VisualElement();
             propertyFiledContainer = new VisualElement();
             PropertyDrawerTools.CreatePropertyFields(
                 propertyFiledContainer,
                 typeof(SlicedSpriteData),
                 property


             );

             root.Add(propertyFiledContainer);
             root.Add(generateNameButton);
             root.Add(changeNameButton);

             return root;
         }



         public void ClickedButtON(SerializedProperty property)
         {

             var nameValue = property.FindPropertyRelative("currentName");
             nameValue.stringValue = GrabGenName(property);
             nameValue.serializedObject.ApplyModifiedProperties();
         }

         private static void ApplyRenames(SerializedProperty property)
         {
             var nameValue = property.FindPropertyRelative("currentName").stringValue;
             var ID = property.FindPropertyRelative("ID").intValue;
             var path = property.FindPropertyRelative("filePath").stringValue;
             var textuer = property.FindPropertyRelative("SpriteTexture").objectReferenceValue as Texture2D;
             var orginName = property.FindPropertyRelative("filePath").stringValue;

             var assets = AssetDatabase.LoadAllAssetRepresentationsAtPath(path);

             path = AssetDatabase.GetAssetPath (textuer);
             var textureImporter = AssetImporter.GetAtPath (path) as TextureImporter;
             SpriteMetaData[] sliceMetaData = textureImporter.spritesheet;


                sliceMetaData[ID].name = nameValue;
                 Debug.Log(sliceMetaData[ID].name);

            

             textureImporter.spritesheet = sliceMetaData;
             EditorUtility.SetDirty (textureImporter);
             textureImporter.SaveAndReimport ();

             AssetDatabase.ImportAsset (path, ImportAssetOptions.ForceUpdate);
             AssetDatabase.SaveAssets();
             AssetDatabase.Refresh();
         }
         
   
     }
 }
 // for(int i = 0; i < assets.Length; i++)
            // {
            //     if (AssetDatabase.IsSubAsset(assets[i]))
            //     {
            //      
            //             assets[i].name = "MMMMMM";
            //             EditorUtility.SetDirty(assets[i]);
            //         
            //     }
            // }
            // EditorUtility.SetDirty(textuer);
            // AssetDatabase.ImportAsset(path);


        
       
    


        // public FieldInfo[] geatAllFiledInfo() => typeof(SlicedSpriteData).GetFields( );
        //
        // public SerializedProperty[] getAllSerlizedPropertys(SerializedProperty property)
        // {
        //     var  allSerlizedPropertys = new List<SerializedProperty>();
        //
        //     var info = geatAllFiledInfo();
        //     
        //
        //     foreach (var fieldInfo in info)
        //     {
        //         Debug.Log(fieldInfo.Name);
        //         allSerlizedPropertys.Add( property.FindPropertyRelative(fieldInfo.Name)); 
        //     }
        //     
        //     return allSerlizedPropertys.ToArray();
        // }

    
        
        //
        // public void GenName(
        //     SerializedProperty settingProperty, 
        //     FactionData.Type faction,
        //     UnitData.Type unittype,
        //     string unitName,
        //     UnitState.Type state,
        //     int frameNum )
        // {
        //     settingProperty.stringValue = faction + "_" + unittype + "_" + unitName + "_" + state + "_" + frameNum;
        // }
