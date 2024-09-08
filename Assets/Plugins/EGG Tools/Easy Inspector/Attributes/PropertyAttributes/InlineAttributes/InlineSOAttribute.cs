using System;
using EGG.EditorStyle;

namespace EGG.Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public class InlineSOAttribute : InlineAttribute
    {
        public InlineSOAttribute(bool foldout = true) : base(foldout) { }
    }
}