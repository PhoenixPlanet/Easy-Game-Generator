using EGG.EditorStyle;
using EGG.Inspector;
using UnityEditor;
using UnityEditor.UIElements;

namespace EGG.Attributes
{
    public class ShowIfAttribute : ModifierAttribute
    {
        public string variableName;

        public override ModifierType ModifierType => ModifierType.ShowIf;

        public ShowIfAttribute(string variableName)
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