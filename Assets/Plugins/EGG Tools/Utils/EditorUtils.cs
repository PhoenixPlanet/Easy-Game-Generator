using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

namespace EGG.Utils
{
    public static class EditorUtils
    {
        public static FieldInfo GetFieldInfo(this SerializedProperty property)
        {
            var targetObject = property.serializedObject.targetObject;

            var targetType = targetObject.GetType();
            var propertyPath = RemoveArraySuffix(property.propertyPath);
            var fieldPathList = propertyPath.Split('.');

            if (fieldPathList.Last() == "m_Script")
            {
                return null;
            }

            if (fieldPathList.Length == 1)
            {
                var field = FindFieldFrom(targetType, fieldPathList[0]);

                if (field == null)
                {
                    EGGLog.LogEGGDebug($"Field {fieldPathList[0]} not found in {targetObject?.GetType()}\nEntire path: {property.propertyPath}");
                }

                return field;
            }

            SerializedProperty targetProp = property.serializedObject.FindProperty(fieldPathList[0]);
            for (var i = 0; i < fieldPathList.Length - 1; i++)
            {
                if (targetProp == null) break;
                if (i != 0)
                {
                    targetProp = targetProp.FindPropertyRelative(fieldPathList[i]);
                }
            }

            var fieldInfo = FindFieldFrom(targetProp.boxedValue.GetType(), fieldPathList.Last());
            if (fieldInfo == null)
            {
                EGGLog.LogEGGDebug($"Field {fieldPathList.Last()} not found in {targetObject?.GetType()}\nEntire path: {property.propertyPath}");
            }

            return fieldInfo;
        }

        public static (FieldInfo, object) GetFieldFromSameParent(this SerializedProperty property, string path)
        {
            var targetObject = property.serializedObject.targetObject;

            var targetType = targetObject.GetType();
            var propertyPath = RemoveArraySuffix(property.propertyPath);
            var fieldPathList = propertyPath.Split('.');

            if (path == "m_Script")
            {
                return (null, null);
            }

            if (fieldPathList.Length == 1)
            {
                var field = FindFieldFrom(targetType, path);

                if (field == null)
                {
                    EGGLog.LogEGGDebug($"Field {fieldPathList[0]} not found in {targetObject?.GetType()}\nEntire path: {property.propertyPath}");
                }

                return (field, targetObject);
            }

            SerializedProperty targetProp = property.serializedObject.FindProperty(fieldPathList[0]);
            for (var i = 0; i < fieldPathList.Length - 1; i++)
            {
                if (targetProp == null) break;
                if (i != 0)
                {
                    targetProp = targetProp.FindPropertyRelative(fieldPathList[i]);
                }
            }

            var fieldInfo = FindFieldFrom(targetProp.boxedValue.GetType(), path);
            if (fieldInfo == null)
            {
                EGGLog.LogEGGDebug($"Field {path} not found in {targetObject?.GetType()}\nEntire path: {property.propertyPath}");
            }

            return (fieldInfo, targetProp.boxedValue);
        }

        public static bool IsSerializeReference(this SerializedProperty property)
        {
            return GetFieldInfo(property)?.GetCustomAttributes(true)?.Any(attr => attr is SerializeReference) ?? false;
        }

        public static bool IsArray(this SerializedProperty property)
        {
            return property.propertyType != SerializedPropertyType.String && property.isArray;
        }

        public static List<SerializedProperty> GetChildren(this SerializedProperty property)
        {
            var children = new List<SerializedProperty>();

            var prop = property.Copy();
            if (prop.NextVisible(true))
            {
                if (prop.depth <= property.depth)
                {
                    return children;
                }

                do
                {
                    children.Add(prop.Copy());
                } while (prop.NextVisible(false) && prop.depth > property.depth);
            }

            return children;
        }

        private static string RemoveArraySuffix(string propertyPath)
        {
            return Regex.Replace(propertyPath, @"\.Array\.data\[\d+\]+$", "");
        }

        private static FieldInfo FindFieldFrom(System.Type type, string fieldName)
        {
            FieldInfo field;
            do
            {
                field = type.GetField(fieldName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.FlattenHierarchy);
            } while (field == null && (type = type.BaseType) != null);

            return field;
        }
    }
}