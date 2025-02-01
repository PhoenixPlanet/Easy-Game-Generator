using System;
using EGG.EditorStyle;

namespace EGG.Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public class InlineGOAttribute : InlineAttribute
    {
        public readonly System.Type[] components = null;

        public InlineGOAttribute(
            bool foldout,
            params System.Type[] components
        ) : base(foldout)
        {
            this.components = components;
        }

        public InlineGOAttribute(
            EasyColor color,
            params System.Type[] components
        ) : base(color: color)
        {
            this.components = components;
        }

        public InlineGOAttribute(
            bool foldout,
            EasyColor color,
            params System.Type[] components
        ) : base(foldout, color)
        {

            this.components = components;
        }

        public InlineGOAttribute(params System.Type[] components) : base()
        {
            this.components = components;
        }
    }
}