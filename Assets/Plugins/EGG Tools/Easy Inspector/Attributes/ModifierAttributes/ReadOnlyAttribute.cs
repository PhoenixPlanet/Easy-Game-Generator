using EGG.Inspector;
using UnityEditor;
using UnityEditor.UIElements;

namespace EGG.Attributes
{
    public class ReadOnlyAttribute : ModifierAttribute
    {
        public override ModifierType ModifierType => ModifierType.ReadOnly;

        public readonly bool readOnly;

        public ReadOnlyAttribute(bool readOnly = true)
        {
            this.readOnly = readOnly;
        }

        public override void ApplyModifier(SerializedProperty property, PropertyField propertyField)
        {
            propertyField.SetEnabled(!readOnly);
        }

        public override void ApplyModifier(SerializedProperty property, AbstractPropertyField objectField)
        {
            objectField.Enabled(!readOnly);
        }
    }
}