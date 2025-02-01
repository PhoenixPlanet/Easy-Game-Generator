using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

using EGG.Attributes;
using EGG.EditorStyle;
using EGG.Utils;

namespace EGG.Inspector
{
    [BindEGGPropertyAttribute(typeof(InlineSOAttribute))]
    public class InlineSOPropertyDrawer : NestingPropertyDrawer
    {
        protected override bool IsTargetType(SerializedProperty property)
        {
            if (property.propertyType == SerializedPropertyType.ObjectReference)
            {
                return property.objectReferenceValue is ScriptableObject;
            }

            return false;
        }

        protected override VisualElement MainContent(SerializedProperty property)
        {
            if (property.serializedObject.targetObject.GetType().Is(property.objectReferenceValue.GetType()))
            {
                return new Label("<color=red>Cannot inline same type of ScriptableObject");
            }

            var inlineAttr = _attribute as InlineSOAttribute;
            var contentBox = new ContentBox(GetLabelString(), backgroundColor: inlineAttr.color);
            contentBox.SetMargin(5);
            contentBox.HideHeader();

            var inspectorElement = new InspectorElement(property.objectReferenceValue as ScriptableObject);
            contentBox.Add(inspectorElement);
            return contentBox;
        }
    }
}
