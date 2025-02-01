using UnityEditor;
using UnityEngine;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

using EGG.Attributes;
using EGG.Utils;

namespace EGG.Inspector
{
    public abstract partial class EGGPropertyDrawer
    {
        protected string GetLabelString()
        {
            var labelAttr = GetModifier(ModifierType.Label);

            if (labelAttr == null)
            {
                return _property.displayName;
            }
            else
            {
                return (labelAttr as LabelAttribute).GetLabelStringOf(_property);
            }
        }

        protected Label CreateLabelElement()
        {
            var label = new Label(GetLabelString());
            return label;
        }
    }
}