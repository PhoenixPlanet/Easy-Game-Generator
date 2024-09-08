using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

using EGG.Attributes;
using EGG.Utils;
using EGG.EditorStyle;

namespace EGG.EasyInspector
{
    public abstract class NestingPropertyDrawer : EGGPropertyDrawer
    {
        protected VisualElement _container;
        protected VisualElement _contentWrapper;
        protected PropertyField _propertyField;

        protected override VisualElement EGGPropertyGUI()
        {
            // Get the attribute
            InlineAttribute inlineAttr = attribute as InlineAttribute;

            // Create the container
            _container = new VisualElement();

            // Draw the property field
            _propertyField = new PropertyField(property);
            _propertyField.BindProperty(property);
            ApplyPropertyFieldStyle();

            // Draw the content
            _contentWrapper = new VisualElement();
            Draw(property, inlineAttr);
            _container.Add(_contentWrapper);

            // Add on value changed event
            _propertyField.RegisterValueChangeCallback(evt =>
            {
                Draw(property, inlineAttr);
            });

            return _container;
        }

        protected virtual void ApplyPropertyFieldStyle()
        {
            _propertyField.label = GetLabelString();
        }

        private void Draw(SerializedProperty property, InlineAttribute inlineAttr)
        {
            _contentWrapper.Clear();

            if (!IsTargetType(property))
            {
                _propertyField.style.position = Position.Relative;
                _contentWrapper.Add(_propertyField);
                return;
            }

            // Add the property field to the content wrapper
            _contentWrapper.Add(_propertyField);

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
                foldout.PlaceBehind(_propertyField);
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