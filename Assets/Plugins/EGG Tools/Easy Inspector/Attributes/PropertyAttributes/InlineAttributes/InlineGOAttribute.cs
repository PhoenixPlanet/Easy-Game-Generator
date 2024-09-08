using System;
using EGG.EditorStyle;

namespace EGG.Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public class InlineGOAttribute : InlineAttribute
    {
        public readonly System.Type[] components = null;

        public InlineGOAttribute(
            bool foldout = true,
            params System.Type[] components
        ) : base(foldout)
        {
            this.components = components;
        }

        public InlineGOAttribute(params System.Type[] components) : base()
        {
            this.components = components;
        }
    }
}