using EGG.Attributes;
using EGG.EditorStyle;
using EGG.Utils;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace EGG.Inspector
{
    [BindEGGPropertyAttribute(typeof(DirectorySelectAttribute))]
    public class DirectorySelectDrawer : EGGPropertyDrawer
    {
        const string RESOURCE_PATH = "Resource/";
        readonly string ENTIRE_RESOURCE_PATH = Application.dataPath + "/" + RESOURCE_PATH;

        private DirectorySelectAttribute _directorySelectAttribute;

        private VisualElement _wrapper;
        private PropertyField _propertyField;
        private Button _button;

        protected override VisualElement EGGPropertyGUI()
        {
            // Get the attribute
            _directorySelectAttribute = _attribute as DirectorySelectAttribute;

            InitElements();

            _wrapper.Add(_propertyField);
            _wrapper.Add(_button);

            return _wrapper;
        }

        private void InitElements()
        {
            if (_wrapper == null)
            {
                _wrapper = new VisualElement();
                _wrapper.style.flexDirection = FlexDirection.Column;
            }

            if (_propertyField == null)
            {
                _propertyField = GetPropertyField();
            }

            if (_button == null)
            {
                if (_property.propertyType == SerializedPropertyType.String)
                {
                    _button = new Button(() =>
                    {
                        string path = EditorUtility.OpenFolderPanel("Select Directory", ENTIRE_RESOURCE_PATH + _directorySelectAttribute.defaultPath, "");
                        if (!string.IsNullOrEmpty(path))
                        {
                            if (path.Contains(ENTIRE_RESOURCE_PATH))
                            {
                                var temp = path.Substring(ENTIRE_RESOURCE_PATH.Length);
                                _property.stringValue = "Assets/" + RESOURCE_PATH + temp;
                                _property.serializedObject.ApplyModifiedProperties();
                            }
                            else
                            {
                                EGGLog.LogError("Selected directory is not in Resources folder.");
                            }
                        }
                    });
                    _button.text = _directorySelectAttribute.buttonLabel;
                }
                else
                {
                    EGGLog.LogError("DirectorySelectAttribute can only be used on string fields.");
                }
            }
        }
    }
}