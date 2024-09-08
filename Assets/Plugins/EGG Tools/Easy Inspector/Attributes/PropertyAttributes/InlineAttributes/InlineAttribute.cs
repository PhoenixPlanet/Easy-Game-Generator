using EGG.EditorStyle;
using UnityEngine;

namespace EGG.Attributes
{
    public class InlineAttribute : EGGPropertyAttribute
    {
        public readonly bool foldout;

        public InlineAttribute(
            bool foldout = true
        )
        {
            this.foldout = foldout;
        }
    }
}