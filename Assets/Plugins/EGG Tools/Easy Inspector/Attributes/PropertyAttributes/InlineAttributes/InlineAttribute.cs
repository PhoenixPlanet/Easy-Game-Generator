using EGG.EditorStyle;
using UnityEngine;

namespace EGG.Attributes
{
    public class InlineAttribute : EGGPropertyAttribute
    {
        public readonly bool foldout;
        public readonly Color color;

        public InlineAttribute(
            bool foldout = true,
            EasyColor color = EasyColor.PrimaryDark
        )
        {
            this.foldout = foldout;
            this.color = EasyColorUtils.GetColor(color);
        }
    }
}