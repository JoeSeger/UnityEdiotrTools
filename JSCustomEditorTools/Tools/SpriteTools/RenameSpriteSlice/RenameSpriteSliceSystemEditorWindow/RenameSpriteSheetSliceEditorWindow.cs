
using System;
using System.Collections.Generic;
using CCZ.Code._2DAnimator.Animation.SlicedSprites;
using Editor.Tools.SerializedProperties;
using UnityEditor;
using UnityEditor.Experimental;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;

namespace Editor.Tools.SpriteTools.RenameSpriteSlice.RenameSpriteSliceSystemEditorWindow
{
    public class RenameSpriteSheetSliceEditorWindow : EditorWindow
    {
        public static RenameSpriteSheetSliceData data
        {
            get
            {
                if (EditorPrefs.HasKey("SliceSpriteDataPath"))
                {
                    var objectPath = EditorPrefs.GetString("SliceSpriteDataPath");

                    _data =
                        AssetDatabase.LoadAssetAtPath(objectPath, typeof(RenameSpriteSheetSliceData)) as
                            RenameSpriteSheetSliceData;
                }
                else
                {
                    _data = RenameSpriteSliceSystem.Create();
                    var relPath = AssetDatabase.GetAssetPath(_data);
                    EditorPrefs.SetString("SliceSpriteDataPath", relPath);
                }

                return _data;
            }
        }

        [MenuItem("Tools/2D/SpriteSheet/RenameSpriteSheetSliceEditorWindow")]
        public static void OpenWindow()
        {
            var wnd = GetWindow<RenameSpriteSheetSliceEditorWindow>();
            wnd.titleContent = new GUIContent("RenameSpriteSheetSliceEditorWindow");
        }

        private void Awake()
        {
            data.spriteSheetTexture = null;
        }
        
        public void OnEnable()
        {
            data.Reset();
            var _root = rootVisualElement;
           var label = new Label("Rename Sprite Slice System");
           var spriteTextureObjectField = new ObjectField("Sprite Sheet Texture")
            {
                objectType = typeof(Texture2D),
            };



           spriteTextureObjectField.RegisterCallback<ChangeEvent<Object>>((evt) =>
            {
                OnChangeTexture2D((Texture2D) evt.newValue,_root);
            });
            
            _root.Add(label);
            _root.Add(spriteTextureObjectField);

       
            
        }


        private List<SlicedSpriteData> alldata;
        private ListView currentListViewContaner;

        private void OnChangeTexture2D(Texture2D newTexture2D,VisualElement root)
        {
            if (root.Contains(currentListViewContaner))
            {
                root.Remove(currentListViewContaner);
            }
    
            alldata = new List<SlicedSpriteData>();
            data.Set(newTexture2D);
            VisualElement MakeItem() => new PropertyField();

            var serializedObjectData = new SerializedObject(data);
            var spriteData = data.currentSprites;
            var spriteDatacheck = spriteData != null;
          
            Debug.Log("sprite check : :" + spriteDatacheck);
            var propertylist = serializedObjectData.FindProperty("currentSprites");

            for (var i = 0; i < propertylist.arraySize; i++)
            {
                var pro = propertylist.GetArrayElementAtIndex(i);
                alldata.Add(SerializedPropertyTools.GetSerializedPropertyRootObject(pro) as SlicedSpriteData);
            }

            // var stringdata = sss.Select(ssd => ssd.JsonString(true));
            void BindItemData(VisualElement e, int i) =>
                ((PropertyField) e).BindProperty(propertylist.GetArrayElementAtIndex(i));
            
           var currentListView  = new ListView(alldata, ItemHeight, MakeItem, BindItemData)
            {
                selectionType = SelectionType.Single,
            };
          
            //  listView.onItemChosen += SelectDataObject;
            //  listView.onSelectionChanged += SelectDataObjects;
            currentListView.style.flexGrow = 1.0f;
            currentListView.Refresh();
            currentListView.MarkDirtyRepaint();
            currentListViewContaner = currentListView;
            root.Add(currentListViewContaner);
            root.MarkDirtyRepaint();
        }

        private void OnDestroy()
        {
            data.Reset();
        }

        private void OnDisable()
        {
            data.Reset();
        }

        private static RenameSpriteSheetSliceData _data;
        private const int ItemHeight = 250;
    }
}