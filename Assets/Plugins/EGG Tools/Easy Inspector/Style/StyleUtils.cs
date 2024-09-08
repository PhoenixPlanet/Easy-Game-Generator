using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace EGG.EditorStyle
{
    public static class StyleUtils
    {
        public const int INDENT_SIZE = 15;
        public const float COLOR_DARKEN_FACTOR = 0.6f;
        public const float COLOR_LIGHTEN_FACTOR = 1.3f;

        public static readonly Color PrimaryDarkColor = new Color(0.1f, 0.1f, 0.1f);

        public static Color EditorThemeColor => EditorGUIUtility.isProSkin ? new Color32(56, 56, 56, 255) : new Color32(194, 194, 194, 255);

        public static void ApplyTextStyle(this TextElement textElement, TextStyle style)
        {
            if (style.HasFlag(TextStyle.Bold))
            {
                if (style.HasFlag(TextStyle.Italic))
                {
                    textElement.style.unityFontStyleAndWeight = FontStyle.BoldAndItalic;
                }
                else
                {
                    textElement.style.unityFontStyleAndWeight = FontStyle.Bold;
                }
            }
            else if (style.HasFlag(TextStyle.Italic))
            {
                textElement.style.unityFontStyleAndWeight = FontStyle.Italic;
            }
            else
            {
                textElement.style.unityFontStyleAndWeight = FontStyle.Normal;
            }

            var text = textElement.text;
            if (style.HasFlag(TextStyle.Underline))
            {
                text = $"<u>{text}</u>";
            }
            if (style.HasFlag(TextStyle.StrikeThrough))
            {
                text = $"<s>{text}</s>";
            }
            textElement.text = text;
        }

        public static string ApplyTextStyle(this string text, TextStyle style, FontSize fontSize)
        {
            if (style.HasFlag(TextStyle.Bold))
            {
                text = $"<b>{text}</b>";
            }
            if (style.HasFlag(TextStyle.Italic))
            {
                text = $"<i>{text}</i>";
            }
            if (style.HasFlag(TextStyle.Underline))
            {
                text = $"<u>{text}</u>";
            }
            if (style.HasFlag(TextStyle.StrikeThrough))
            {
                text = $"<s>{text}</s>";
            }

            switch (fontSize)
            {
                case FontSize.Small:
                    text = $"<size=10>{text}</size>";
                    break;
                case FontSize.Medium:
                    text = $"<size=12>{text}</size>";
                    break;
                case FontSize.Large:
                    text = $"<size=14>{text}</size>";
                    break;
                case FontSize.XLarge:
                    text = $"<size=16>{text}</size>";
                    break;
                case FontSize.XXLarge:
                    text = $"<size=18>{text}</size>";
                    break;
                case FontSize.XXXLarge:
                    text = $"<size=20>{text}</size>";
                    break;
            }

            return text;
        }

        public static void DefaultHeaderStyle(this Label label)
        {
            label.style.unityFontStyleAndWeight = FontStyle.Bold;
            label.style.fontSize = (int)FontSize.Medium;
            label.style.paddingTop = 10;
            label.style.paddingBottom = 5;
        }
    }
}
