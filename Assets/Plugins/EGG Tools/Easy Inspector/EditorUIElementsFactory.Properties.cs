#if UNITY_EDITOR

using EGG.Attributes;
using EGG.Utils;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace EGG.Inspector
{
    public partial class EditorUIElementsFactory
    {
        public VisualElement GenerateAppropriateField(SerializedProperty property, List<EGGPropertyAttribute> attributes, List<ModifierAttribute> modifiers, List<MethodAttributePair> methods)
        {
            if (property.IsArray())
            {
                return GenerateListField(property, attributes, modifiers);
            }
            else if ((property.hasVisibleChildren || methods.Count > 0) && !property.IsSerializeReference())
            {
                if (attributes.Count == 0 && HasEGGAttributes(property))
                {
                    return new WithChildrenPropertyField(property, methods);
                }
                else
                {
                    return GeneratePropertyField(property, attributes, modifiers);
                }
            }
            else
            {
                return GeneratePropertyField(property, attributes, modifiers);
            }
        }

        public VisualElement GenerateAppropriateField(SerializedProperty property)
        {
            var attributes = property.GetAttributes<EGGPropertyAttribute>();
            var modifiers = property.GetAttributes<ModifierAttribute>();
            var methods = property.GetEGGMethods();

            return GenerateAppropriateField(property, attributes, modifiers, methods);
        }

        public VisualElement GeneratePropertyField(SerializedProperty property, List<EGGPropertyAttribute> attributes, List<ModifierAttribute> modifiers)
        {
            if (property.IsSerializeReference())
            {
                var propertyField = new AbstractPropertyField(property);
                if (attributes == null || attributes.Count == 0)
                {
                    ApplyModifiersToNativePropertyField(property, modifiers, propertyField);
                }
                return propertyField;
            }
            else if (attributes == null || attributes.Count == 0)
            {
                var propertyField = new PropertyField(property);
                ApplyModifiersToNativePropertyField(property, modifiers, propertyField);
                return propertyField;
            }
            else
            {
                var attribute = attributes[0];
                var propertyDrawer = EGGPropertyDrawer.CreateDrawer(attribute, modifiers);
                return propertyDrawer.CreatePropertyGUI(property.Copy());
            }
        }

        public VisualElement GenerateListField(SerializedProperty property, List<EGGPropertyAttribute> attributes, List<ModifierAttribute> modifiers)
        {
            if (!property.IsArray())
            {
                EGGLog.LogError("Property is not an array or list.");
                return null;
            }

            if (HasEGGAttributes(property))
            {
                return new PropertyList(property, attributes, modifiers);
            }
            else
            {
                var list = new PropertyField(property);
                ApplyModifiersToNativePropertyField(property, modifiers, list);
                return list;
            }
        }

        private void ApplyModifiersToNativePropertyField(SerializedProperty property, List<ModifierAttribute> modifiers, AbstractPropertyField propertyField)
        {
            if (modifiers == null || modifiers.Count == 0) return;

            foreach (var modifier in modifiers)
            {
                modifier.ApplyModifier(property, propertyField);
            }
        }

        private void ApplyModifiersToNativePropertyField(SerializedProperty property, List<ModifierAttribute> modifiers, PropertyField propertyField)
        {
            if (modifiers == null || modifiers.Count == 0) return;

            foreach (var modifier in modifiers)
            {
                modifier.ApplyModifier(property, propertyField);
            }
        }

        private bool AreChildrenHaveEGGAttributes(SerializedProperty property)
        {
            if (!property.hasVisibleChildren)
            {
                return false;
            }

            var children = property.GetChildren();
            foreach (var child in children)
            {
                if (child.propertyType == SerializedPropertyType.ArraySize)
                {
                    continue;
                }

                if (HasEGGAttributes(child))
                {
                    return true;
                }
            }

            return false;
        }

        private bool HasEGGAttributes(SerializedProperty property)
        {
            return property.IsEGGProperty() || AreChildrenHaveEGGAttributes(property) || property.IsSerializeReference();
        }
    }
}

#endif