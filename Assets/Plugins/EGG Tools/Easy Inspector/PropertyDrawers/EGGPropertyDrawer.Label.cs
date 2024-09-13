using UnityEditor;
using UnityEngine;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

using EGG.Attributes;
using EGG.Utils;

namespace EGG.EasyInspector
{
    public abstract partial class EGGPropertyDrawer : PropertyDrawer
    {
        protected string GetLabelString()
        {
            var labelAttr = GetModifier(ModifierType.Label);

            if (labelAttr == null)
            {
                return property.displayName;
            }
            else
            {
                return (labelAttr as LabelAttribute).GetLabelStringOf(property);
            }
        }

        protected Label CreateLabelElement()
        {
            var label = new Label(GetLabelString());
            return label;
        }
    }
}