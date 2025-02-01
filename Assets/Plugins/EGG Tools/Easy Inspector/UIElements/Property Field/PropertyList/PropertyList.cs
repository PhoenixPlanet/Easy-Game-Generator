using EGG.Attributes;
using EGG.Utils;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace EGG.Inspector
{
    public class PropertyList : VisualElement
    {
        private EditorUIElementsFactory _factory;

        private SerializedProperty _arrayProperty;
        private PropertyField _propertyField;
        private ScrollView _scrollView;

        private List<EGGPropertyAttribute> _attributes;
        private List<ModifierAttribute> _modifiers;

        public EditorUIElementsFactory Factory => _factory;

        public PropertyList(SerializedProperty arrayProperty, List<EGGPropertyAttribute> attributes, List<ModifierAttribute> modifiers)
        {
            this._factory = new EditorUIElementsFactory();

            this._arrayProperty = arrayProperty.Copy();
            this._attributes = attributes;
            this._modifiers = modifiers;
            this._propertyField = new PropertyField(arrayProperty);
            this._scrollView = new ScrollView(ScrollViewMode.Vertical);

            var contentBox = new ContentBox(arrayProperty.displayName);
            contentBox.SetMarginHorizontal(2);
            contentBox.SetMarginVertical(5);
            Add(contentBox);
            contentBox.Add(this._scrollView);

            contentBox.Header.AddHeaderButton("Add", () =>
            {
                _arrayProperty.arraySize++;
                _arrayProperty.serializedObject.ApplyModifiedProperties();
                Update();
            });

            Update();

            _propertyField.RegisterCallback<ChangeEvent<SerializedProperty>>((evt) =>
            {
                Update();
            });

            Undo.undoRedoPerformed -= Update;
            Undo.undoRedoPerformed += Update;
        }

        public void Update()
        {
            this._scrollView.Clear();
            _arrayProperty.serializedObject.Update();

            if (!this._arrayProperty.isArray)
            {
                EGGLog.LogError("Property is not an array or list.");
                return;
            }

            if (this._arrayProperty.arraySize == 0)
            {
                _scrollView.Add(new Label("No elements in array"));
            }

            for (int i = 0; i < this._arrayProperty.arraySize; i++)
            {
                var element = this._arrayProperty.GetArrayElementAtIndex(i);
                var propertyField = new CustomListElement(this, element.Copy(), _attributes, _modifiers,
                i == 0, i == this._arrayProperty.arraySize - 1, i,
                (idx) =>
                {
                    this._arrayProperty.DeleteArrayElementAtIndex(idx);
                    _arrayProperty.serializedObject.ApplyModifiedProperties();
                    Update();
                },
                (idx) =>
                {
                    if (idx == 0) return;
                    this._arrayProperty.MoveArrayElement(idx, idx - 1);
                    _arrayProperty.serializedObject.ApplyModifiedProperties();
                    Update();
                },
                (idx) =>
                {
                    if (idx == this._arrayProperty.arraySize - 1) return;
                    this._arrayProperty.MoveArrayElement(idx, idx + 1);
                    _arrayProperty.serializedObject.ApplyModifiedProperties();
                    Update();
                });
                this._scrollView.Add(propertyField);
            }
        }
    }
}