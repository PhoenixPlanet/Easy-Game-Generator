using System.Collections.Generic;
using System.Linq;

using EGG.Attributes;

using UnityEditor;

namespace EGG.Utils
{
    public static class AttributeUtils
    {
        public static Dictionary<SerializedProperty, List<ModifierAttribute>> modifierAttributesCache = new();

        public static List<ModifierAttribute> GetModifierAttributes(this SerializedProperty property)
        {
            if (modifierAttributesCache.TryGetValue(property, out var cachedModifiers))
            {
                return cachedModifiers;
            }

            var fieldInfo = property.GetFieldInfo();
            if (fieldInfo == null) return new List<ModifierAttribute>();

            var modifierAttributes = fieldInfo.GetCustomAttributes(typeof(ModifierAttribute), true) as ModifierAttribute[];

            if (modifierAttributes == null || modifierAttributes.Length == 0)
            {
                modifierAttributesCache[property] = new List<ModifierAttribute>();
                return new List<ModifierAttribute>();
            }

            var attributesList = modifierAttributes.ToList();
            modifierAttributesCache[property] = attributesList;

            return attributesList;
        }

        public static ModifierAttribute GetModifier(this SerializedProperty property, ModifierType modifierType)
        {
            var modifierAttributes = property.GetModifierAttributes();

            if (modifierAttributes == null || modifierAttributes.Count == 0) return null;

            return modifierAttributes.Find(modifier => modifier.ModifierType == modifierType);
        }

        public static bool IsEGGProperty(this SerializedProperty property)
        {
            var fieldInfo = property.GetFieldInfo();
            if (fieldInfo == null) return false;

            var eggPropertyAttrs = fieldInfo.GetCustomAttributes(typeof(EGGPropertyAttribute), true);

            return eggPropertyAttrs != null && eggPropertyAttrs.Length > 0;
        }
    }
}