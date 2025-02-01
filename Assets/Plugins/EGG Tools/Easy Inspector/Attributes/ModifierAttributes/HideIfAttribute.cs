using EGG.Inspector;
using UnityEditor;
using UnityEditor.UIElements;

namespace EGG.Attributes
{
    public class HideIfAttribute : ModifierAttribute
    {
        public string variableName;

        public override ModifierType ModifierType => ModifierType.HideIf;

        public HideIfAttribute(string variableName)
        {
            this.variableName = variableName;
        }

        public override void ApplyModifier(SerializedProperty property, AbstractPropertyField propertyField)
        {

        }

        public override void ApplyModifier(SerializedProperty property, PropertyField objectField)
        {

        }
    }
}