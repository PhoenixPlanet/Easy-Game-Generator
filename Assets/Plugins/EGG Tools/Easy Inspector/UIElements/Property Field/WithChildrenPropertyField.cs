using EGG.DebugTools;
using EGG.Utils;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace EGG.Inspector
{
    public class WithChildrenPropertyField : VisualElement
    {
        private SerializedProperty _property;
        private EditorUIElementsFactory _factory;

        public WithChildrenPropertyField(SerializedProperty property, List<MethodAttributePair> methods, bool isFoldout = true)
        {
            _property = property.Copy();

            if (!_property.hasVisibleChildren)
            {
                EGGLog.LogWarning($"Property '{_property.name}' does not have any visible children.");
                Add(new PropertyField(_property));
                return;
            }

            _factory = new EditorUIElementsFactory();

            VisualElement wrapper;

            if (isFoldout)
            {
                Foldout foldout = new Foldout
                {
                    text = _property.displayName,
                    value = _property.isExpanded
                };
                foldout.BindProperty(_property);

                wrapper = foldout;
            }
            else
            {
                wrapper = new VisualElement();
                wrapper.Bind(_property.serializedObject);
            }

            foreach (SerializedProperty child in _property.GetChildren())
            {
                wrapper.Add(_factory.GenerateAppropriateField(child));
            }

            var methodButtons = _factory.HandleButtonAttribute(_property, methods);
            foreach (var button in methodButtons)
            {
                wrapper.Add(button);
            }

            Add(wrapper);
        }
    }
}