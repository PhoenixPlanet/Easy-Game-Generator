using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

using EGG.Attributes;
using EGG.EditorStyle;
using EGG.Utils;

namespace EGG.EasyInspector
{
    [CustomPropertyDrawer(typeof(InlineSOAttribute))]
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
            var contentBox = new ContentBox(GetLabelString());
            contentBox.SetMarginVertical(5);
            contentBox.HideHeader();

            var inspectorElement = new InspectorElement(property.objectReferenceValue as ScriptableObject);
            contentBox.Add(inspectorElement);
            return contentBox;
        }
    }
}
