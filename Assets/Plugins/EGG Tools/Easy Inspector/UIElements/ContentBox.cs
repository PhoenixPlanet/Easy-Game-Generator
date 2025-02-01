using EGG.EditorStyle;
using EGG.Utils;
using UnityEngine;
using UnityEngine.UIElements;

namespace EGG.Inspector
{
    public class ContentBox : VisualElement
    {
        public BoxHeader Header => _header;

        private BoxHeader _header;

        public ContentBox(string title, Texture icon = null, Color? backgroundColor = null)
        {
            style.flexDirection = FlexDirection.Column;
            this.SetPadding(UIToolkitUtils.Edge.Bottom, 5);
            this.SetBorder(1, Color.gray);
            this.SetBorderRadius(5);

            if (backgroundColor.HasValue)
            {
                style.backgroundColor = backgroundColor.Value;
            }
            else
            {
                style.backgroundColor = StyleUtils.PrimaryDarkColor;
            }

            _header = new BoxHeader(title, icon);
            _header.SetBorderBottom(1, Color.gray);
            Add(_header);
        }

        public void SetBorderSize(int size)
        {
            this.SetBorder(size, Color.gray);
        }

        public void HideHeader()
        {
            _header.style.display = DisplayStyle.None;
        }

        public void ShowHeader()
        {
            _header.style.display = DisplayStyle.Flex;
        }
    }
}