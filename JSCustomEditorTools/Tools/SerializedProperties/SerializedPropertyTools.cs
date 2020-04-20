using System;
using System.Collections.Generic;
using System.Linq;
using CCZ.Code.Tools.Reflection;
using UnityEditor;
using UnityEngine;

namespace Editor.Tools.SerializedProperties
{
    public static class SerializedPropertyTools
    {


        public static IEnumerable<string> GetAllSerializedPropertiesNames(this SerializedProperty property)
        {
            return property.GetAllSerializedProperties().Select(p => p.name).ToArray();
        }
        
        public static IEnumerable<SerializedProperty> GetAllSerializedProperties(this SerializedProperty property)
        {
            var serializedObject = property.serializedObject;
            var targetObject = serializedObject.targetObject;
            var type = targetObject.GetType();
            
             var filedInfoList = ReflectionTools.GrabAllFiledInfoFromType(type).ToArray();

             return filedInfoList.Select(fieldInfo => fieldInfo.Name).Select(
                 pnam => serializedObject.FindProperty(pnam)).ToArray();
        }
        
      
        public static object GetSerializedPropertyRootObject(SerializedProperty property)
        {
            var tar = property.serializedObject.targetObject;
            object obj = tar as Component;
            if (obj != null) return obj;
            obj = tar as ScriptableObject;
            if (obj != null) return obj;
            Debug.LogError("Could not get target object on " + property.displayName);
            return null;
        }

    }
}