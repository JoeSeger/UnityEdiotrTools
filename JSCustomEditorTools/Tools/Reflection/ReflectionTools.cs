using System.Collections.Generic;
using System.Reflection;
using System;
using UnityEditor;

namespace CCZ.Code.Tools.Reflection
{
    public static class ReflectionTools
    {
        
        public static IEnumerable<FieldInfo> GrabAllFiledInfoFromType<T>() => GrabAllFiledInfoFromType(typeof(T));
        public static IEnumerable<FieldInfo> GrabAllFiledInfoFromType(Type currentType) => currentType.GetFields();
        
        public static IEnumerable<FieldInfo> GrabAllFiledInfoFromType(SerializedProperty property) => GrabAllFiledInfoFromType(property.GetType());

        public static IEnumerable<FieldInfo> GetFiledInfoBuyCurrentBindingFlags<T>() => GetFiledInfoBuyCurrentBindingFlags(typeof(T));
        public static IEnumerable<FieldInfo> GetFiledInfoBuyCurrentBindingFlags(Type objectType,
            BindingFlags currentBindingFlags = BindingFlags.Default)
        {
            return currentBindingFlags == BindingFlags.Default ? objectType.GetFields() : objectType.GetFields(currentBindingFlags);
        }
    }
}