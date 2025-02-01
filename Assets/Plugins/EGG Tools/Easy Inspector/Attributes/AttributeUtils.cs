using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using EGG.Attributes;

using UnityEditor;

namespace EGG.Utils
{
    public static class AttributeUtils
    {
        // TODO: EditorUIElementsFactory로 옮겨서 fieldInfo, Attribute 등 정보를 캐시하는 것이 좋을 것 같음
        public static List<T> GetAttributes<T>(this SerializedProperty property) where T : System.Attribute
        {
            var fieldInfo = property.GetFieldInfo();
            if (fieldInfo == null) return new List<T>();

            var attributes = fieldInfo.GetCustomAttributes(typeof(T), true) as T[];

            if (attributes == null || attributes.Length == 0)
            {
                return new List<T>();
            }

            var attributesList = attributes.ToList();

            return attributesList;
        }

        public static List<MethodAttributePair> GetEGGMethods(this SerializedProperty property)
        {
            if (property.IsArray()) return new();

            Type targetType;
            var obj = property.boxedValue;

            if (obj == null)
            {
                var fieldInfo = property.GetFieldInfo();
                if (fieldInfo == null) return new();

                targetType = fieldInfo.FieldType;
            }
            else
            {
                targetType = obj.GetType();
            }

            var methodPairs = targetType.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static)
                .Where(m => m.GetCustomAttributes(typeof(EditorButtonAttribute), true).Length > 0)
                .Select(m => new MethodAttributePair { method = m, attribute = m.GetCustomAttribute<EditorButtonAttribute>(true) });

            return methodPairs.ToList();
        }

        public static ModifierAttribute GetModifier(this SerializedProperty property, ModifierType modifierType)
        {
            var modifierAttributes = property.GetAttributes<ModifierAttribute>();

            if (modifierAttributes == null || modifierAttributes.Count == 0) return null;

            return modifierAttributes.Find(modifier => modifier.ModifierType == modifierType);
        }

        public static bool IsEGGProperty(this SerializedProperty property)
        {
            var fieldInfo = property.GetFieldInfo();
            if (fieldInfo == null) return false;

            var eggPropertyAttrs = fieldInfo.GetCustomAttributes(typeof(EGGPropertyAttribute), true);
            var eggMethods = fieldInfo.FieldType.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static)
                .Where(m => m.GetCustomAttributes(typeof(EditorButtonAttribute), true).Length > 0);

            return (eggPropertyAttrs != null && eggPropertyAttrs.Length > 0) || eggMethods.Count() > 0;
        }
    }

    public record MethodAttributePair
    {
        public MethodInfo method;
        public EditorButtonAttribute attribute;
    }
}