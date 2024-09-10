using UnityEditor;
using UnityEngine;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

using EGG.Attributes;
using EGG.Utils;
using System.Linq;
using System.Collections.Generic;
using System;

namespace EGG.EasyInspector
{
    public abstract partial class EGGPropertyDrawer : PropertyDrawer
    {
        protected SerializedProperty property;

        protected List<ModifierAttribute> modifierAttributes;

        public sealed override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            this.property = property;

            var allModifers = property.GetModifierAttributes();
            modifierAttributes = GetValidModifiers(allModifers);

            return EGGPropertyGUI();
        }

        protected virtual ModifierType[] InvalidModifiers => null;
        protected abstract VisualElement EGGPropertyGUI();

        private List<ModifierAttribute> GetValidModifiers(List<ModifierAttribute> modifiers)
        {
            if (modifiers == null || modifiers.Count == 0) return null;

            List<ModifierAttribute> validModifiers = new();
            HashSet<ModifierType> validTypes = new();

            foreach (var modifier in modifiers)
            {
                if (InvalidModifiers != null && InvalidModifiers.Contains(modifier.ModifierType)) continue;

                if (validTypes.Contains(modifier.ModifierType))
                {
                    if (EGGLog.LogEnabled)
                    {
                        EGGLog.LogWarning($"Multiple modifiers of type {modifier.ModifierType} found on the same field. Only one is allowed.");
                    }
                    continue;
                }

                validModifiers.Add(modifier);
                validTypes.Add(modifier.ModifierType);
            }

            if (EGGLog.LogEnabled && validModifiers.Count != modifiers.Count)
            {
                EGGLog.LogWarning($"Some modifiers are not valid for this property drawer:");
                EGGLog.LogWarning($"{string.Join(", ", modifiers.Except(validModifiers).Select(modifier => modifier.GetType().Name))}");
            }

            return validModifiers;
        }

        private bool HasModifier()
        {
            return modifierAttributes != null && modifierAttributes.Count != 0;
        }

        private ModifierAttribute GetModifier(ModifierType modifierType)
        {
            if (!HasModifier()) return null;
            return modifierAttributes.Find(modifier => modifier.ModifierType == modifierType);
        }

        private List<ModifierAttribute> GetModifiersByType(Type modifierType, bool inherit = true)
        {
            if (!HasModifier()) return null;

            if (inherit)
            {
                return modifierAttributes.FindAll(modifier => modifier.GetType().Is(modifierType));
            }
            else
            {
                return modifierAttributes.FindAll(modifier => modifier.GetType() == modifierType);
            }
        }
    }
}