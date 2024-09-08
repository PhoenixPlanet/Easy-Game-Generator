using EGG.Utils;
using UnityEngine;
using UnityEngine.UIElements;

namespace EGG.EditorStyle
{
    public class BoxHeader : VisualElement
    {
        public BoxHeader(EditorLabel label, Texture icon = null)
        {
            style.flexDirection = FlexDirection.Row;
            style.alignItems = Align.Center;
            this.SetPadding(5, 5, 5, 5);

            if (icon != null)
            {
                var imageSection = new Image();
                imageSection.style.width = 16;
                imageSection.style.height = 16;
                imageSection.image = icon;
                imageSection.SetMarginRight(5);

                Add(imageSection);
            }

            var labelSection = new Label(label.StyledText);
            labelSection.style.height = 16;
            Add(labelSection);
        }

        public BoxHeader(string label, Texture icon = null) : this(new EditorLabel(label, TextStyle.Bold, FontSize.Medium), icon)
        {

        }
    }
}