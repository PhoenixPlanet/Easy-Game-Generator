using System;

namespace EGG.EditorStyle
{
    [Flags]
    public enum TextStyle
    {
        None = 0,
        Bold = 1 << 0,
        Italic = 1 << 1,
        Underline = 1 << 2,
        StrikeThrough = 1 << 3,
    }

    public enum FontSize
    {
        Small = 10,
        Medium = 12,
        Large = 14,
        XLarge = 16,
        XXLarge = 18,
        XXXLarge = 20,
    }

    public record EditorLabel
    {
        public string rawText;
        public TextStyle style;
        public FontSize fontSize;

        public EditorLabel(string text, TextStyle style = TextStyle.None, FontSize fontSize = FontSize.Medium)
        {
            rawText = text;
            this.style = style;
            this.fontSize = fontSize;
        }

        public bool IsTextNullOrWhiteSpace => string.IsNullOrWhiteSpace(rawText);

        public string StyledText => StyleUtils.ApplyTextStyle(rawText, style, fontSize);

        public string GetStyledText(string text) => StyleUtils.ApplyTextStyle(text, style, fontSize);
    }
}