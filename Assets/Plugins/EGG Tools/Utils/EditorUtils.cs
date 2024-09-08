using System.Reflection;
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
            var fieldPath = property.propertyPath.Split('.');

            FieldInfo fieldInfo = null;
            for (var i = 0; i < fieldPath.Length; i++)
            {
                var field = targetType.GetField(fieldPath[i], (BindingFlags)(-1));
                if (field == null)
                {
                    Debug.LogError($"Field {fieldPath[i]} not found in {targetType}\nEntire path: {property.propertyPath}");
                    break;
                }

                fieldInfo = field;
                targetType = field.FieldType;
            }

            return fieldInfo;
        }
    }
}