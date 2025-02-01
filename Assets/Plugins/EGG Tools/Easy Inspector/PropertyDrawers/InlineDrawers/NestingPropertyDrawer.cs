using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

using EGG.Attributes;
using EGG.Utils;
using EGG.EditorStyle;

namespace EGG.Inspector
{
    public abstract class NestingPropertyDrawer : EGGPropertyDrawer
    {
        protected VisualElement _container;
        protected VisualElement _contentWrapper;
        protected PropertyField _propertyField;

        protected override VisualElement EGGPropertyGUI()
        {
            // Get the attribute
            InlineAttribute inlineAttr = _attribute as InlineAttribute;

            // Create the container
            _container = new VisualElement();

            // Draw the property field
            _propertyField = GetPropertyField();

            // Add the property field to the content wrapper
            _container.Add(_propertyField);

            // Draw the content
            _contentWrapper = new VisualElement();
            _container.Add(_contentWrapper);
            Draw(_property, inlineAttr);

            // Add on value changed event
            _propertyField.RegisterValueChangeCallback(evt =>
            {
                Draw(_property, inlineAttr);
            });

            return _container;
        }

        private void Draw(SerializedProperty property, InlineAttribute inlineAttr)
        {
            _contentWrapper.Clear();

            if (!IsTargetType(property))
            {
                _propertyField.style.position = Position.Relative;
                return;
            }

            if (inlineAttr.foldout)
            {
                // If the foldout is true, add the foldout to the content wrapper
                var foldout = new Foldout
                {
                    value = false
                };

                // Set Position of the property field to Absolute
                _propertyField.style.position = Position.Absolute;
                _propertyField.SetPositionHorizontal(0, 0);
                _propertyField.SetPositionTop(0);

                // Add the main content to the foldout, and add the foldout to the content wrapper
                foldout.Add(MainContent(property));
                foldout.SetMarginVertical(2);

                _contentWrapper.Add(foldout);
                _contentWrapper.PlaceBehind(_propertyField);
                //_propertyField.SetMarginLeft(StyleUtils.INDENT_SIZE);
                //_propertyField.PlaceBehind(_contentWrapper);
            }
            else
            {
                // Add the main content to the content wrapper
                var mainContent = MainContent(property);
                mainContent.SetMarginLeft(StyleUtils.INDENT_SIZE);
                _contentWrapper.Add(mainContent);
            }
        }

        protected abstract bool IsTargetType(SerializedProperty property);
        protected abstract VisualElement MainContent(SerializedProperty property);
    }
}