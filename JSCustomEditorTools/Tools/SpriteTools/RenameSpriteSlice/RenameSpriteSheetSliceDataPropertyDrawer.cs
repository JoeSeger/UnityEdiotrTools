using CCZ.Code._2DAnimator.Animation.SlicedSprites;
using Editor.Tools.UIElements;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Editor.Tools.SpriteTools.RenameSpriteSlice
{
    [CustomPropertyDrawer(typeof(RenameSpriteSheetSliceData))]
    public class RenameSpriteSheetSliceDataPropertyDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
      
            var root  = new VisualElement();
             PropertyDrawerTools.CreatePropertyFields(root, typeof(SlicedSpriteData), property);

             return root;
        }
    }
}