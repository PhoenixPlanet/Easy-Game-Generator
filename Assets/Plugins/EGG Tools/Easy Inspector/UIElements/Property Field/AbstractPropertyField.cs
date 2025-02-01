using EGG.EditorStyle;
using EGG.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Unity.Properties;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace EGG.Inspector
{
    public class AbstractPropertyField : VisualElement
    {
        private DropdownField _propertySelection;
        private IMGUIContainer _propertyField;

        private string _fieldLabel;

        private SerializedProperty _property;
        private List<Type> _derivedTypes;
        private List<string> _managedTypeNames;

        private Type _selectedType;

        public AbstractPropertyField(SerializedProperty property = null)
        {
            if (property != null)
            {
                this.Bind(property.serializedObject);
                BindAbstractProperty(property);
                _fieldLabel = property.displayName;
            }

            RegisterEvents();
        }

        public void BindAbstractProperty(SerializedProperty property)
        {
            this.Clear();
            _property = property.Copy();

            if (_property.propertyType != SerializedPropertyType.ManagedReference)
            {
                return;
            }

            FieldInfo fieldInfo = EditorUtils.GetFieldInfo(_property.Copy());
            if (fieldInfo == null)
            {
                EGGLog.LogError($"Field {_property.name} not found in {_property.serializedObject.targetObject.GetType()}");
                return;
            }

            if (fieldInfo.FieldType.IsGenericType && fieldInfo.FieldType.GetGenericTypeDefinition() == typeof(List<>))
            {
                _selectedType = _property.boxedValue?.GetType();
                _derivedTypes = fieldInfo.FieldType.GetGenericArguments()[0].FindDerivedTypesInAllAssemblies();
                _derivedTypes = _derivedTypes.FindAll(type => !typeof(UnityEngine.Object).IsAssignableFrom(type));
            }
            else
            {
                _selectedType = _property.boxedValue?.GetType();
                _derivedTypes = fieldInfo.FieldType.FindDerivedTypesInAllAssemblies();
                _derivedTypes = _derivedTypes.FindAll(type => !typeof(UnityEngine.Object).IsAssignableFrom(type));
            }

            if (_derivedTypes.Count == 0)
            {
                return;
            }
            _derivedTypes.Insert(0, null);

            _managedTypeNames = _derivedTypes.ConvertAll(type => type == null ? "-" : type.Name);
            var defaultIndex = _managedTypeNames.Contains(_selectedType?.Name) ? _managedTypeNames.IndexOf(_selectedType.Name) : 0;

            _propertySelection = new DropdownField("Type", _managedTypeNames, defaultIndex);
            _propertySelection.style.position = Position.Absolute;
            _propertySelection.labelElement.style.display = DisplayStyle.None;

            _propertyField = new IMGUIContainer(() =>
            {
                EditorGUI.BeginChangeCheck();
                EditorGUILayout.PropertyField(_property, new GUIContent(_fieldLabel), true);
                if (EditorGUI.EndChangeCheck())
                {
                    _property.serializedObject.ApplyModifiedProperties();
                }
            });

            Add(_propertySelection);
            Add(_propertyField);

            style.justifyContent = Justify.SpaceBetween;
            style.width = Length.Percent(100);

            _propertyField.PlaceBehind(_propertySelection);
        }

        public void SetLabel(string label)
        {
            _fieldLabel = label;
        }

        public void Enabled(bool enabled)
        {
            _propertyField.SetEnabled(enabled);
        }

        private void RegisterEvents()
        {
            this.RegisterCallback<GeometryChangedEvent>(evt =>
            {
                _propertySelection.SetPositionLeft(this.GetLabelContextWidth());
            });

            _propertySelection.RegisterValueChangedCallback(OnSelectionChanged);

            //_fakeField.RegisterCallback<SerializedPropertyChangeEvent>(OnValueChanged);
        }

        private void OnValueChanged(SerializedPropertyChangeEvent evt)
        {
            _propertySelection.UnregisterValueChangedCallback(OnSelectionChanged);

            _property = evt.changedProperty.Copy();
            _selectedType = _property.boxedValue?.GetType();
            int index = _managedTypeNames.IndexOf(_selectedType == null ? "-" : _selectedType.Name);
            _propertySelection.index = index;

            //_fakeField.BindProperty(_property);

            _propertySelection.RegisterValueChangedCallback(OnSelectionChanged);
        }

        private void OnSelectionChanged(ChangeEvent<string> evt)
        {
            if (evt.newValue == "-")
            {
                _selectedType = null;
                _property.boxedValue = null;
                _property.serializedObject.ApplyModifiedProperties();
                return;
            }

            _selectedType = _derivedTypes[_managedTypeNames.IndexOf(evt.newValue)];
            var obj = Activator.CreateInstance(_selectedType);
            _property.boxedValue = obj;
            _property.serializedObject.ApplyModifiedProperties();
        }
    }
}