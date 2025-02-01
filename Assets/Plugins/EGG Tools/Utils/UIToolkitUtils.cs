using UnityEngine;
using UnityEngine.UIElements;

namespace EGG.Utils
{
    public static class UIToolkitUtils
    {
        public enum Edge
        {
            None = 0,
            Top = 1 << 0,
            Right = 1 << 1,
            Bottom = 1 << 2,
            Left = 1 << 3
        }

        public enum Corner
        {
            None = 0,
            TopLeft = 1 << 0,
            TopRight = 1 << 1,
            BottomRight = 1 << 2,
            BottomLeft = 1 << 3
        }

        #region Margin
        public static void SetMargin(this VisualElement element, float top, float right, float bottom, float left)
        {
            element.style.marginTop = top;
            element.style.marginRight = right;
            element.style.marginBottom = bottom;
            element.style.marginLeft = left;
        }

        public static void SetMargin(this VisualElement element, float margin)
        {
            SetMargin(element, margin, margin, margin, margin);
        }

        public static void SetMargin(this VisualElement element, Edge edge, float margin)
        {
            if (edge.HasFlag(Edge.Top))
            {
                element.style.marginTop = margin;
            }

            if (edge.HasFlag(Edge.Right))
            {
                element.style.marginRight = margin;
            }

            if (edge.HasFlag(Edge.Bottom))
            {
                element.style.marginBottom = margin;
            }

            if (edge.HasFlag(Edge.Left))
            {
                element.style.marginLeft = margin;
            }
        }

        public static void SetMarginVertical(this VisualElement element, float margin)
        {
            SetMargin(element, Edge.Top | Edge.Bottom, margin);
        }

        public static void SetMarginHorizontal(this VisualElement element, float margin)
        {
            SetMargin(element, Edge.Left | Edge.Right, margin);
        }

        public static void SetMarginLeft(this VisualElement element, float margin)
        {
            element.style.marginLeft = margin;
        }

        public static void SetMarginRight(this VisualElement element, float margin)
        {
            element.style.marginRight = margin;
        }

        public static void SetMarginTop(this VisualElement element, float margin)
        {
            element.style.marginTop = margin;
        }

        public static void SetMarginBottom(this VisualElement element, float margin)
        {
            element.style.marginBottom = margin;
        }
        #endregion

        #region Padding
        public static void SetPadding(this VisualElement element, float top, float right, float bottom, float left)
        {
            element.style.paddingTop = top;
            element.style.paddingRight = right;
            element.style.paddingBottom = bottom;
            element.style.paddingLeft = left;
        }

        public static void SetPadding(this VisualElement element, float padding)
        {
            SetPadding(element, padding, padding, padding, padding);
        }

        public static void SetPadding(this VisualElement element, Edge edge, float padding)
        {
            if (edge.HasFlag(Edge.Top))
            {
                element.style.paddingTop = padding;
            }

            if (edge.HasFlag(Edge.Right))
            {
                element.style.paddingRight = padding;
            }

            if (edge.HasFlag(Edge.Bottom))
            {
                element.style.paddingBottom = padding;
            }

            if (edge.HasFlag(Edge.Left))
            {
                element.style.paddingLeft = padding;
            }
        }

        public static void SetPaddingVertical(this VisualElement element, float padding)
        {
            SetPadding(element, Edge.Top | Edge.Bottom, padding);
        }

        public static void SetPaddingHorizontal(this VisualElement element, float padding)
        {
            SetPadding(element, Edge.Left | Edge.Right, padding);
        }
        #endregion

        #region Position
        public static void SetPosition(this VisualElement element, Position position)
        {
            element.style.position = position;
        }

        public static void SetPosition(this VisualElement element, Position position, float top, float right, float bottom, float left)
        {
            SetPosition(element, position);
            element.style.top = top;
            element.style.right = right;
            element.style.bottom = bottom;
            element.style.left = left;
        }

        public static void SetPosition(this VisualElement element, Edge edge, float value)
        {
            if (edge.HasFlag(Edge.Top))
            {
                element.style.top = value;
            }

            if (edge.HasFlag(Edge.Right))
            {
                element.style.right = value;
            }

            if (edge.HasFlag(Edge.Bottom))
            {
                element.style.bottom = value;
            }

            if (edge.HasFlag(Edge.Left))
            {
                element.style.left = value;
            }
        }

        public static void SetPositionTop(this VisualElement element, float top)
        {
            element.style.top = top;
        }

        public static void SetPositionRight(this VisualElement element, float right)
        {
            element.style.right = right;
        }

        public static void SetPositionBottom(this VisualElement element, float bottom)
        {
            element.style.bottom = bottom;
        }

        public static void SetPositionLeft(this VisualElement element, float left)
        {
            element.style.left = left;
        }

        public static void SetPositionHorizontal(this VisualElement element, float left, float right)
        {
            element.style.left = left;
            element.style.right = right;
        }

        public static void SetPositionVertical(this VisualElement element, float top, float bottom)
        {
            element.style.top = top;
            element.style.bottom = bottom;
        }
        #endregion

        #region Border
        public static void SetBorder(this VisualElement element, float width, Color color)
        {
            SetBorderTop(element, width, color);
            SetBorderRight(element, width, color);
            SetBorderBottom(element, width, color);
            SetBorderLeft(element, width, color);
        }

        public static void SetBorder(this VisualElement element, Edge edge, float width)
        {
            if (edge.HasFlag(Edge.Top))
            {
                element.style.borderTopWidth = width;
            }

            if (edge.HasFlag(Edge.Right))
            {
                element.style.borderRightWidth = width;
            }

            if (edge.HasFlag(Edge.Bottom))
            {
                element.style.borderBottomWidth = width;
            }

            if (edge.HasFlag(Edge.Left))
            {
                element.style.borderLeftWidth = width;
            }
        }

        public static void SetBorderTop(this VisualElement element, float width, Color color)
        {
            element.style.borderTopWidth = width;
            element.style.borderTopColor = color;
        }

        public static void SetBorderRight(this VisualElement element, float width, Color color)
        {
            element.style.borderRightWidth = width;
            element.style.borderRightColor = color;
        }

        public static void SetBorderBottom(this VisualElement element, float width, Color color)
        {
            element.style.borderBottomWidth = width;
            element.style.borderBottomColor = color;
        }

        public static void SetBorderLeft(this VisualElement element, float width, Color color)
        {
            element.style.borderLeftWidth = width;
            element.style.borderLeftColor = color;
        }

        public static void SetBorderHorizontal(this VisualElement element, float width, Color color)
        {
            SetBorderLeft(element, width, color);
            SetBorderRight(element, width, color);
        }

        public static void SetBorderVertical(this VisualElement element, float width, Color color)
        {
            SetBorderTop(element, width, color);
            SetBorderBottom(element, width, color);
        }

        public static void SetBorderRadius(this VisualElement element, float radius)
        {
            element.style.borderTopLeftRadius = radius;
            element.style.borderTopRightRadius = radius;
            element.style.borderBottomLeftRadius = radius;
            element.style.borderBottomRightRadius = radius;
        }

        public static void SetBorderRadius(this VisualElement element, float topLeft, float topRight, float bottomRight, float bottomLeft)
        {
            element.style.borderTopLeftRadius = topLeft;
            element.style.borderTopRightRadius = topRight;
            element.style.borderBottomRightRadius = bottomRight;
            element.style.borderBottomLeftRadius = bottomLeft;
        }

        public static void SetBorderRadius(this VisualElement element, Corner corner, float radius)
        {
            if (corner.HasFlag(Corner.TopLeft))
            {
                element.style.borderTopLeftRadius = radius;
            }

            if (corner.HasFlag(Corner.TopRight))
            {
                element.style.borderTopRightRadius = radius;
            }

            if (corner.HasFlag(Corner.BottomRight))
            {
                element.style.borderBottomRightRadius = radius;
            }

            if (corner.HasFlag(Corner.BottomLeft))
            {
                element.style.borderBottomLeftRadius = radius;
            }
        }
        #endregion
    }
}