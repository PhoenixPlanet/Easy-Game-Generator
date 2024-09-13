#if UNITY_EDITOR

using EGG.Utils;
using UnityEditor;
using UnityEditor.UIElements;

namespace EGG.EasyInspector
{
    public partial class EasyInspector : Editor
    {
        private PropertyField GeneratePropertyField(SerializedProperty property)
        {
            var propertyField = new PropertyField(property);

            if (!property.IsEGGProperty())
            {
                ApplyModifiersToNativePropertyField(property, propertyField);
            }

            return propertyField;
        }

        private void ApplyModifiersToNativePropertyField(SerializedProperty property, PropertyField propertyField)
        {
            var modifiers = property.GetModifierAttributes();

            if (modifiers == null || modifiers.Count == 0) return;

            foreach (var modifier in modifiers)
            {
                modifier.ApplyModifier(property, propertyField);
            }
        }
    }
}

#endif