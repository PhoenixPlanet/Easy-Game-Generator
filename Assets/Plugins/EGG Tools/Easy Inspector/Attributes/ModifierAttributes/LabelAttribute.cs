using EGG.EditorStyle;
using EGG.Inspector;
using UnityEditor;
using UnityEditor.UIElements;

namespace EGG.Attributes
{
    public class LabelAttribute : ModifierAttribute
    {
        public override ModifierType ModifierType => ModifierType.Label;
        public readonly EditorLabel label;

        public LabelAttribute(
            string text = null,
            TextStyle textStyle = TextStyle.None,
            FontSize fontSize = FontSize.Medium
        )
        {
            this.label = text == null ? null : new EditorLabel(text, textStyle, fontSize);
        }

        public LabelAttribute(EditorLabel label)
        {
            this.label = label;
        }

        public LabelAttribute(TextStyle textStyle, FontSize fontSize)
        {
            this.label = new EditorLabel(null, textStyle, fontSize);
        }

        public LabelAttribute(TextStyle textStyle)
        {
            this.label = new EditorLabel(null, style: textStyle);
        }

        public LabelAttribute(FontSize fontSize)
        {
            this.label = new EditorLabel(null, fontSize: fontSize);
        }

        public string GetLabelStringOf(SerializedProperty property)
        {
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

        public override void ApplyModifier(SerializedProperty property, AbstractPropertyField propertyField)
        {
            propertyField.SetLabel(GetLabelStringOf(property));
        }

        public override void ApplyModifier(SerializedProperty property, PropertyField objectField)
        {
            objectField.label = GetLabelStringOf(property);
        }
    }
}