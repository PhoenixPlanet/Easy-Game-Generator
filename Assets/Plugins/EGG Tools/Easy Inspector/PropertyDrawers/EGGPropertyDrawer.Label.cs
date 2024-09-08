using UnityEditor;
using UnityEngine;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

using EGG.Attributes;

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
                var label = (labelAttr as LabelAttribute).label;
                if (label == null)
                {
                    return property.displayName;
                }
                else if (label.IsTextNullOrWhiteSpace)
                {
                    return label.GetStyledText(property.displayName);
                }
                else
                {
                    return label.StyledText;
                }
            }
        }

        protected Label CreateLabelElement()
        {
            var label = new Label(GetLabelString());
            return label;
        }
    }
}