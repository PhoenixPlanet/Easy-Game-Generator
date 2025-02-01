using EGG.Attributes;
using EGG.EditorStyle;
using EGG.Inspector;
using EGG.Utils;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace EGG.Inspector
{
    public class CustomListElement : VisualElement
    {
        private const int BUTTON_AREA_WIDTH = 30;

        private PropertyList _propertyList;
        private VisualElement _buttonArea;

        public CustomListElement
        (
            PropertyList propertyList, SerializedProperty property,
            List<EGGPropertyAttribute> attributes, List<ModifierAttribute> modifiers,
            bool isFirst, bool isLast, int idx,
            Action<int> onDelete, Action<int> swapUp, Action<int> swapDown
        )
        {
            _propertyList = propertyList;
            Color backgroundColor = EasyColorUtils.GetColor(EasyColor.EditorThemeColor) * (idx % 2 == 0 ? 0.75f : 0.85f);

            var contentBox = new ContentBox(property.displayName, backgroundColor: backgroundColor);
            contentBox.HideHeader();
            contentBox.SetBorderSize(0);
            contentBox.SetPaddingVertical(5);
            Add(contentBox);

            contentBox.style.flexDirection = FlexDirection.Row;

            _buttonArea = new VisualElement();
            _buttonArea.style.height = Length.Percent(100);
            _buttonArea.style.width = BUTTON_AREA_WIDTH;
            contentBox.Add(_buttonArea);
            _buttonArea.style.justifyContent = Justify.Center;

            VisualElement field = _propertyList.Factory.GenerateAppropriateField(property, attributes, modifiers, property.GetEGGMethods());
            contentBox.Add(field);
            field.SetMarginLeft(BUTTON_AREA_WIDTH + StyleUtils.INDENT_SIZE);
            field.style.width = Length.Percent(90);

            var deleteButton = new Button(() =>
            {
                onDelete?.Invoke(idx);
            })
            {
                text = "X",
            };
            contentBox.Add(deleteButton);

            _buttonArea.style.flexDirection = FlexDirection.Column;
            _buttonArea.Add(new Button(() =>
            {
                swapUp?.Invoke(idx);
                //EGGLog.LogEGGDebug($"Swapping up {property.displayName}");
            })
            {
                enabledSelf = !isFirst,
                text = "▲"
            });
            _buttonArea.Add(new Button(() =>
            {
                swapDown?.Invoke(idx);
                //EGGLog.LogEGGDebug($"Swapping down {property.displayName}");
            })
            {
                enabledSelf = !isLast,
                text = "▼"
            });
        }
    }
}