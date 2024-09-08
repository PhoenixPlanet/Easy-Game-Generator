using EGG.EditorStyle;

namespace EGG.Attributes
{
    public class LabelAttribute : ModifierAttribute
    {
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

        public override ModifierType ModifierType => ModifierType.Label;
    }
}