using EGG.Inspector;
using UnityEditor;
using UnityEditor.UIElements;

namespace EGG.Attributes
{
    public enum ModifierType
    {
        None,
        Label,
        LabelStyle,
        ShowIf,
        HideIf,
        ReadOnly,
    }

    public abstract class ModifierAttribute : EGGAttribute
    {
        public ModifierAttribute()
        {

        }

        public abstract ModifierType ModifierType { get; }

        public abstract void ApplyModifier(SerializedProperty property, AbstractPropertyField propertyField);
        public abstract void ApplyModifier(SerializedProperty property, PropertyField objectField);
    }
}