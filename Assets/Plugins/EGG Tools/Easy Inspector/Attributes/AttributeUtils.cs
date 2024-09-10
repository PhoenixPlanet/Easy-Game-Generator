using System.Collections.Generic;
using System.Linq;

using EGG.Attributes;

using UnityEditor;

namespace EGG.Utils
{
    public static class AttributeUtils
    {
        public static List<ModifierAttribute> GetModifierAttributes(this SerializedProperty property)
        {
            var fieldInfo = property.GetFieldInfo();

            var modifierAttributes = fieldInfo.GetCustomAttributes(typeof(ModifierAttribute), true) as ModifierAttribute[];

            if (modifierAttributes == null || modifierAttributes.Length == 0)
            {
                return new List<ModifierAttribute>();
            }

            return modifierAttributes.ToList();
        }
    }
}