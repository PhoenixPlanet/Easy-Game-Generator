using System;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;

namespace EGG.Attributes
{
    public enum ModifierType
    {
        None,
        Label,
        LabelStyle
    }

    public abstract class ModifierAttribute : EGGAttribute
    {
        public ModifierAttribute()
        {

        }

        public abstract ModifierType ModifierType { get; }

        public abstract void ApplyModifier(SerializedProperty property, PropertyField propertyField);
    }
}