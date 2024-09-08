using EGG.EditorStyle;

namespace EGG.Attributes
{
    public class LabelStyleAttribute : LabelAttribute
    {
        public LabelStyleAttribute(TextStyle textStyle, FontSize fontSize) : base(null, textStyle, fontSize) { }

        public LabelStyleAttribute(TextStyle textStyle) : base(null, textStyle) { }

        public LabelStyleAttribute(FontSize fontSize) : base(null, fontSize: fontSize) { }

        public override ModifierType ModifierType => ModifierType.LabelStyle;
    }
}