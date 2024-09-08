using UnityEngine;

namespace EGG.Attributes
{
    public class PreviewAttribute : EGGPropertyAttribute
    {
        public readonly bool useSize;
        public readonly float size;
        public readonly bool matchHeight;

        public readonly float width;
        public readonly float height;

        public PreviewAttribute(float size = 64, bool matchHeight = true)
        {
            useSize = true;

            this.size = size;
            this.matchHeight = matchHeight;
        }

        public PreviewAttribute(float width, float height)
        {
            useSize = false;

            this.width = width;
            this.height = height;
        }
    }
}