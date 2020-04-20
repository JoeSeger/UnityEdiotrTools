using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using JetBrains.Annotations;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;

namespace Editor.Tools.UIElements
{
    internal static class PropertyDrawerTools
    {
        public static readonly BindingFlags CurrentBindingFlags = BindingFlags.Default;

        public const BindingFlags CustomDefaultBindingFlags =
            BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
        //
        // public static FieldInfo[] GetFiledInfoBuyCurrentBindingFlags<T>() =>
        //     GetFiledInfoBuyCurrentBindingFlags(typeof(T));

        // private static FieldInfo[] GetFiledInfoBuyCurrentBindingFlags(Type objectType,
        //     BindingFlags currentBindingFlags = CustomDefaultBindingFlags)
        // {
        //     return objectType.GetFields();
        // }

        public static void CreatePropertyEditButtonFields(VisualElement container, Type objectType,
            SerializedProperty serializedObject, string buttonName, Action buttonAction, bool refresh = false,
            bool toHide = false, string[] checkingNames = null,
            BindingFlags currentBindingFlags = CustomDefaultBindingFlags)
        {
            SetPropertyFields(container, serializedObject,
                objectType.GetFields(), refresh, toHide, checkingNames);
            AddButton(buttonName, buttonAction, container);
        }

        public static void CreatePropertyEditButtonFields(VisualElement container, Type objectType,
            SerializedProperty serializedProperty, string buttonName, Action buttonAction,
            out List<PropertyField> propertyFields, bool refresh = false, bool toHide = false,
            string[] checkingNames = null, BindingFlags currentBindingFlags = CustomDefaultBindingFlags)
        {
            SetPropertyFields(container, serializedProperty,
                objectType.GetFields(), out propertyFields, refresh,
                toHide, checkingNames);
            AddButton(buttonName, buttonAction, container);
        }

        public static void CreatePropertyEditButtonFields(VisualElement container, Type objectType,
            SerializedObject serializedObject, string buttonName, Action buttonAction,
            out List<PropertyField> propertyFields, bool refresh = false, bool toHide = false,
            string[] checkingNames = null, BindingFlags currentBindingFlags = CustomDefaultBindingFlags)
        {
            SetPropertyFields(container, serializedObject,
                objectType.GetFields(), out propertyFields, refresh,
                toHide, checkingNames);
            AddButton(buttonName, buttonAction, container);
        }

        public static void AddButton(string buttonName, Action buttonAction, VisualElement addingContainer)
        {
            var editButton = new Button(buttonAction) {text = buttonName};
            addingContainer.Add(editButton);
        }

        public static void CreatePropertyEditButtonFields(VisualElement container, Type objectType,
            SerializedObject serializedObject, string buttonName, Action buttonAction, bool refresh = false,
            bool toHide = false, string[] checkingNames = null,
            BindingFlags currentBindingFlags = CustomDefaultBindingFlags)
        {
            SetPropertyFields(container, serializedObject,
                objectType.GetFields(), refresh, toHide, checkingNames);
            AddButton(buttonName, buttonAction, container);
        }

        public static void CreatePropertyFields<T>(VisualElement container, 
            bool refresh = false,
            bool toHide = false, 
            string[] checkingNames = null) where T : Object
        {
            var find = Resources.FindObjectsOfTypeAll<T>().FirstOrDefault();
            var serializedObject = new SerializedObject(find);
            SetPropertyFields(container, serializedObject, typeof(T).GetFields(), refresh, toHide, checkingNames);
        }

        public static void CreatePropertyFields(VisualElement container, Type objectType,
            SerializedProperty serializedProperty, bool refresh = false, bool toHide = false,
            BindingFlags currentBindingFlags = CustomDefaultBindingFlags, string[] hidingFields = null)
        {
            SetPropertyFields(container, serializedProperty,
                objectType.GetFields(), refresh, toHide, hidingFields);
        }

        private static void SetPropertyFields(VisualElement container, SerializedObject serializedObject,
            FieldInfo[] fields, bool refresh = false, bool toHide = false, string[] checkingNames = null)
        {
            if (fields == null) throw new ArgumentNullException(nameof(fields));
            if (refresh) container.Clear();

            foreach (var fieldInfo in fields)
            {
                if (checkingNames != null)
                {
                    var check = toHide
                        ? checkingNames.Contains(fieldInfo.Name)
                        : !checkingNames.Contains(fieldInfo.Name);

                    if (check) continue;
                    container.Add(new PropertyField(serializedObject.FindProperty(fieldInfo.Name)));
                }
                else
                {

                    container.Add(new PropertyField(serializedObject.FindProperty(fieldInfo.Name)));
                }
            }
        }

        private static void SetPropertyFields(VisualElement container,
            SerializedProperty serializedProperty,
            FieldInfo[] fields, 
            out List<PropertyField> propertyFields, bool refresh = false, bool toHide = false,
            string[] checkingNames = null)
        {
            if (fields == null) throw new ArgumentNullException(nameof(fields));

            if (refresh) container.Clear();
            propertyFields = new List<PropertyField>();
            var newPropertyField = new PropertyField();
            foreach (var fieldInfo in fields)
            {
                if (checkingNames != null)
                {
                    var check = toHide
                        ? checkingNames.Contains(fieldInfo.Name)
                        : !checkingNames.Contains(fieldInfo.Name);

                    if (check) continue;
                    newPropertyField = new PropertyField(serializedProperty.FindPropertyRelative(fieldInfo.Name));
                }
                else
                {
                    newPropertyField = new PropertyField(serializedProperty.FindPropertyRelative(fieldInfo.Name));
                }
            }

            propertyFields.Add(newPropertyField);
            container.Add(newPropertyField);
        }

        private static void SetPropertyFields(
            VisualElement container,
            SerializedObject serializedObject,
            FieldInfo[] fields, out List<PropertyField> propertyFields, bool refresh = false, bool toHide = false,
            string[] checkingNames = null)
        {
            if (fields == null) throw new ArgumentNullException(nameof(fields));

            if (refresh) container.Clear();
            propertyFields = new List<PropertyField>();
            var newPropertyField = new PropertyField();
            foreach (var fieldInfo in fields)
            {
                if (checkingNames != null)
                {
                    var check = toHide
                        ? checkingNames.Contains(fieldInfo.Name)
                        : !checkingNames.Contains(fieldInfo.Name);

                    if (check) continue;
                    newPropertyField = new PropertyField(serializedObject.FindProperty(fieldInfo.Name));
                }
                else
                {
                    newPropertyField = new PropertyField(serializedObject.FindProperty(fieldInfo.Name));
                }
            }

            propertyFields.Add(newPropertyField);
            container.Add(newPropertyField);
        }

        private static void SetPropertyFields(VisualElement container, SerializedProperty serializedProperty,
            FieldInfo[] fields, bool refresh = false, bool toHide = false, string[] checkingNames = null)
        {
            if (fields == null) throw new ArgumentNullException(nameof(fields));
            if (refresh) container.Clear();

            foreach (var fieldInfo in fields)
            {
                if (checkingNames != null)
                {
                    var check = toHide
                        ? checkingNames.Contains(fieldInfo.Name)
                        : !checkingNames.Contains(fieldInfo.Name);

                    if (check) continue;

                    container.Add(new PropertyField(serializedProperty.FindPropertyRelative(fieldInfo.Name)));
                }
                else
                {
                    container.Add(new PropertyField(serializedProperty.FindPropertyRelative(fieldInfo.Name)));
                }
            }
        }

        public static void SetStringPropertyFieldsToPopUp(VisualElement container, Type objType,
            SerializedProperty serializedProperty, int defaultAMount, Action<string, int> changeEven,
            BindingFlags currentBindingFlags = CustomDefaultBindingFlags, bool refresh = false, bool toHide = false,
            string[] checkingNames = null, params string[] choices)
        {
            SetStringPropertyFieldsToPopUp(container, objType, serializedProperty, choices, defaultAMount, changeEven,
                currentBindingFlags, refresh, toHide, checkingNames);
        }

        public static void SetStringPropertyFieldsToPopUp(VisualElement container, Type objType,
            SerializedObject serializedObject, int defaultAMount, Action<string, int> changeEven,
            BindingFlags currentBindingFlags = CustomDefaultBindingFlags, bool refresh = false, bool toHide = false,
            string[] checkingNames = null, params string[] choices)
        {
            SetStringPropertyFieldsToPopUp(container, objType, serializedObject, choices, defaultAMount, toHide,
                refresh, changeEven, currentBindingFlags, checkingNames);
        }

        private static void SetStringPropertyFieldsToPopUp(VisualElement container, Type objType,
            SerializedProperty serializedProperty, [NotNull] string[] choices, int defaultAMount,
            Action<string, int> changeEvent, BindingFlags currentBindingFlags = CustomDefaultBindingFlags,
            bool refresh = false, bool toHide = false, string[] checkingNames = null)
        {
            if (choices == null) throw new ArgumentNullException(nameof(choices));
            if (refresh) container.Clear();
            var partPopUps = new VisualElement();
            var fields = objType.GetFields();
            foreach (var fieldInfo in fields)
            foreach (var name in checkingNames.Where(name => serializedProperty != null)
                .Where(name => serializedProperty != null)
                .Where(name => serializedProperty != null))
                SetPropertyFields(defaultAMount, serializedProperty?.FindPropertyRelative(name), fieldInfo, partPopUps,
                    container, choices, changeEvent, refresh, toHide, checkingNames);
        }

        private static void SetStringPropertyFieldsToPopUp(VisualElement container, Type objType,
            SerializedObject serializedObject, [NotNull] string[] choices, int defaultAmount, bool toHide, bool refresh,
            Action<string, int> changeEvent, BindingFlags currentBindingFlags, string[] checkingNames = null)
        {
            if (choices == null) throw new ArgumentNullException(nameof(choices));
            if (refresh) container.Clear();
            var partPopUps = new VisualElement();
            var fields = objType.GetFields(currentBindingFlags);
            foreach (var fieldInfo in fields)
            foreach (var name in checkingNames.Where(name => serializedObject != null)
                .Where(name => serializedObject != null)
                .Where(name => serializedObject != null))
                SetPropertyFields(defaultAmount, serializedObject?.FindProperty(name), fieldInfo, partPopUps, container,
                    choices, changeEvent, refresh, toHide, checkingNames);
        }

        private static void SetPropertyFields(
            int defaultAmount,
            SerializedProperty property, 
            FieldInfo fieldInfo,
            VisualElement partPopUps, VisualElement container, IReadOnlyList<string> choices,
            Action<string, int> changeEvent, bool refresh = false, bool toHide = false,
            IEnumerable<string> checkingNames = null)
        {
            var passingFiledName = fieldInfo.Name;
            var check = !toHide ? checkingNames.Contains(fieldInfo.Name) : !checkingNames.Contains(fieldInfo.Name);

            if (!check)
            {
                return;
            }

            property.stringValue = null;

            if (string.IsNullOrEmpty(property.stringValue) || refresh || property.stringValue == "Value")
            {
                property.stringValue = choices[defaultAmount];
                property.serializedObject.ApplyModifiedProperties();
            }

            var addingPopupFiled = new PopupField<string>(passingFiledName, choices.ToList(), defaultAmount)
            {
                bindingPath = property.propertyPath,
                name = passingFiledName + "_PopUpField",
                value = property.stringValue,
            };

            if (changeEvent != null)
            {
                addingPopupFiled.RegisterCallback<ChangeEvent<string>>((evt) =>
                    changeEvent(evt.newValue, addingPopupFiled.index));
            }

            if (partPopUps.Contains(addingPopupFiled)) return;

            addingPopupFiled.BindProperty(property);

            partPopUps.Add(addingPopupFiled);

            container.Add(partPopUps);
        }
    }
}
//
//
//[CustomEditor(typeof(AIDataList))]
//public class AiListCustomEditor : Editor
//{
//    private AIDataList AiDataList => (AIDataList) target;
//
//
//    public override VisualElement CreateInspectorGUI()
//    {
//       var aiDataListRoot = new VisualElement();
//        CreatePropertyFields(aiDataListRoot, typeof(AIDataList));
//        return aiDataListRoot;
//    }
//
//    protected void CreatePropertyFields(VisualElement container, Type objectType)
//    {
//        var fields = objectType.GetFields();
//        foreach (var fieldInfo in fields)
//        {
//            Debug.Log("fieldInfo name " + fieldInfo.Name);
//            container.Add(
//                new PropertyField(serializedObject.FindProperty(fieldInfo.Name)));
//        }
//    }
//}

//[AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
//public class VEPropertyDrawerAttribute : Attribute
//{

//}
//public class VisualElementPropertyDrawerEditorBase : Editor
//{
//    public override VisualElement CreateInspectorGUI()
//    {
//        var container = new VisualElement();

//        // Draw the legacy IMGUI base
//        var imgui = new IMGUIContainer(OnInspectorGUI);
//        container.Add(imgui);

//        // Find all properties that are marked [HideInInspector] that have
//        // a PropertyDrawer tagged with the [VEPropertyDrawer] attribute and create
//        // PropertyFields for each of them.
//        var type = target.GetType();
//        // Create property fields.
//        // Add fields to the container.
//        CreatePropertyFields(container, type);
//        return container;

//    }

//    protected void CreatePropertyFields(VisualElement container, Type objectType)
//    {
//        var fields = objectType.GetFields(
//              BindingFlags.GetField | BindingFlags.Instance | BindingFlags.Public);
//        foreach (var fieldInfo in fields)
//        {
//            var attr = fieldInfo.GetCustomAttribute<HideInInspector>();
//            if (attr == null || !IsPropertyDrawerTagged(fieldInfo.FieldType))
//                continue;

//            container.Add(
//                new PropertyField(serializedObject.FindProperty(fieldInfo.Name)));
//        }
//    }

//    protected bool IsPropertyDrawerTagged(Type propertyType)
//    {
//        var drawerType = GetPropertyDrawerType(propertyType);
//        if (drawerType == null)
//            return false;

//        var method = drawerType.GetMethod("CreatePropertyGUI",
//                        BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance, null,
//                        new[] { typeof(SerializedProperty) }, null);
//        return method != null;

//    }

//    /// <summary>
//    /// Use Reflection to access ScriptAttributeUtility to find the
//    /// PropertyDrawer type for a property type
//    /// </summary>
//    protected Type GetPropertyDrawerType(Type typeToDraw)
//    {
//        var scriptAttributeUtilityType = GetScriptAttributeUtilityType();

//        var getDrawerTypeForTypeMethod =
//                    scriptAttributeUtilityType.GetMethod(
//                        "GetDrawerTypeForType",
//                        BindingFlags.Static | BindingFlags.NonPublic, null,
//                        new[] { typeof(Type) }, null);

//        return (Type)getDrawerTypeForTypeMethod.Invoke(null, new[] { typeToDraw });
//    }

//    protected Type GetScriptAttributeUtilityType()
//    {
//        var asm = Array.Find(AppDomain.CurrentDomain.GetAssemblies(),
//                                          (a) => a.GetName().Name == "UnityEditor");

//        var types = asm.GetTypes();
//        var type = Array.Find(types, (t) => t.Name == "ScriptAttributeUtility");

//        return type;
//    }
//    public override void OnInspectorGUI()
//    {
//        DrawDefaultInspector();
//    }

//}