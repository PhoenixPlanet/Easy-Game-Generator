using System;
using EGG.Attributes;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace EGG.Inspector
{
    [CustomPropertyDrawer(typeof(EGGPropertyAttribute))]
    [CustomPropertyDrawer(typeof(EGGAttribute))]
    public class CommonPropertyDrawer : EGGPropertyDrawer
    {
        protected override VisualElement EGGPropertyGUI()
        {
            PropertyField propertyField = new PropertyField(_property);
            propertyField.label = GetLabelString();

            return propertyField;
        }
    }
}