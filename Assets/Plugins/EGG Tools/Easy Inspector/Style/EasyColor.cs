using UnityEngine;

namespace EGG.EditorStyle
{
    public enum EasyColor
    {
        PrimaryDark,
        PrimaryLight,
        SecondaryDark,
        SecondaryLight,
        EditorThemeColor,
        White,
        Black,
        Red,
        Green,
        Blue,
        Yellow,
        Cyan,
        Magenta,
        Gray,
        DarkGray,
        LightGray
    }

    public static class EasyColorUtils
    {
        public static Color GetColor(EasyColor easyColor)
        {
            switch (easyColor)
            {
                case EasyColor.PrimaryDark:
                    return new Color(0.1f, 0.1f, 0.1f);
                case EasyColor.PrimaryLight:
                    return new Color(0.9f, 0.9f, 0.9f);
                case EasyColor.SecondaryDark:
                    return new Color(0.2f, 0.2f, 0.2f);
                case EasyColor.SecondaryLight:
                    return new Color(0.8f, 0.8f, 0.8f);
                case EasyColor.EditorThemeColor:
                    return StyleUtils.EditorThemeColor;
                case EasyColor.White:
                    return Color.white;
                case EasyColor.Black:
                    return Color.black;
                case EasyColor.Red:
                    return Color.red;
                case EasyColor.Green:
                    return Color.green;
                case EasyColor.Blue:
                    return Color.blue;
                case EasyColor.Yellow:
                    return Color.yellow;
                case EasyColor.Cyan:
                    return Color.cyan;
                case EasyColor.Magenta:
                    return Color.magenta;
                case EasyColor.Gray:
                    return Color.gray;
                case EasyColor.DarkGray:
                    return Color.grey;
                case EasyColor.LightGray:
                    return Color.grey * StyleUtils.COLOR_LIGHTEN_FACTOR;
                default:
                    return Color.white;
            }
        }
    }
}