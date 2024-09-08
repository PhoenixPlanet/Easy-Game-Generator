using System.Linq;
using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

using EGG.Attributes;
using EGG.Utils;
using EGG.EditorStyle;

namespace EGG.EasyInspector
{
    [CustomPropertyDrawer(typeof(InlineGOAttribute))]
    public class InlineGOPropertyDrawer : NestingPropertyDrawer
    {
        protected override bool IsTargetType(SerializedProperty property)
        {
            if (property.propertyType == SerializedPropertyType.ObjectReference)
            {
                return property.objectReferenceValue is GameObject;
            }

            return false;
        }

        protected override VisualElement MainContent(SerializedProperty property)
        {
            var targetObject = property.objectReferenceValue as GameObject;
            var inlineGOAttribute = attribute as InlineGOAttribute;

            // If no GameObject is selected
            if (targetObject == null)
            {
                return new Label("No GameObject selected");
            }

            Type[] targetTypes = inlineGOAttribute.components;
            List<Component> targetComponents = new();

            if (targetTypes == null || targetTypes.Length == 0)
            {
                // If no components are specified, get all components
                targetComponents.AddRange(targetObject.GetComponents<Component>());

                // Check if the target object is in the list (to prevent infinite loop)
                if (targetComponents.Any(component => component == property.serializedObject.targetObject))
                {
                    return new Label("<color=red>Cannot self-inline");
                }
            }
            else
            {
                // Get all components of the specified types
                foreach (var targetType in targetTypes)
                {
                    var component = targetObject.GetComponent(targetType);
                    if (component != null)
                    {
                        if (component == property.serializedObject.targetObject)
                        {
                            return new Label("Cannot inline the same GameObject");
                        }
                        targetComponents.Add(component);
                    }
                }
            }

            var wrapper = new VisualElement();

            foreach (var component in targetComponents)
            {
                if (component == null) continue;

                var icon = EditorGUIUtility.ObjectContent(component, component.GetType());

                var contentBox = new ContentBox(icon.text, icon.image);
                contentBox.SetMarginVertical(5);

                var inspectorElement = new InspectorElement(component);
                contentBox.Add(inspectorElement);

                wrapper.Add(contentBox);
            }

            return wrapper;
        }
    }
}
