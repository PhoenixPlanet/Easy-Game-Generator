using EGG.Attributes;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace EGG.Inspector
{
    [BindEGGPropertyAttribute(typeof(PreviewAttribute))]
    public class PreviewDrawer : EGGPropertyDrawer
    {
        PreviewAttribute _previewAttribute;
        private Object _targetObject;
        private Editor _objectPreviewEditor;

        private VisualElement _wrapper;
        private VisualElement _preview;
        private PropertyField _propertyField;

        protected override VisualElement EGGPropertyGUI()
        {
            // Get the attribute
            _previewAttribute = _attribute as PreviewAttribute;

            InitElements();

            _wrapper.Add(_propertyField);
            _wrapper.Add(_preview);

            var previewIMGUI = CreatePreview();
            if (previewIMGUI != null) _preview.Add(previewIMGUI);

            return _wrapper;
        }

        private void InitElements()
        {
            if (_wrapper == null)
            {
                _wrapper = new VisualElement();
                _wrapper.style.flexDirection = FlexDirection.Column;
            }

            if (_preview == null)
            {
                _preview = new VisualElement();
                _preview.style.flexDirection = FlexDirection.RowReverse;
                _preview.style.marginBottom = 5;
                _preview.style.paddingTop = 5;
            }
            else
            {
                _preview.Clear();
            }

            if (_propertyField == null)
            {
                _propertyField = GetPropertyField();
                _propertyField.RegisterCallback<SerializedPropertyChangeEvent>(OnPropertyFieldChanged);
            }

            _targetObject = _property.objectReferenceValue;
        }

        private IMGUIContainer CreatePreview()
        {
            if (_targetObject == null) return null;

            var previewIMGUI = new IMGUIContainer();

            previewIMGUI.RegisterCallback<DetachFromPanelEvent>(e => OnPreviewDestroy());

            float targetWidth, targetHeight;
            if (_previewAttribute.useSize)
            {
                Texture2D texture = null;
                if (_targetObject is Sprite sprite)
                {
                    texture = sprite.texture;
                }
                else if (_targetObject is Texture2D tex)
                {
                    texture = tex;
                }

                if (_previewAttribute.matchHeight)
                {
                    targetHeight = _previewAttribute.size;

                    if (texture != null)
                    {
                        targetWidth = _previewAttribute.size * texture.width / texture.height;
                    }
                    else
                    {
                        targetWidth = _previewAttribute.size;
                    }
                }
                else
                {
                    targetWidth = _previewAttribute.size;
                    if (texture != null)
                    {
                        targetHeight = _previewAttribute.size * texture.height / texture.width;
                    }
                    else
                    {
                        targetHeight = _previewAttribute.size;
                    }
                }
            }
            else
            {
                targetWidth = _previewAttribute.width;
                targetHeight = _previewAttribute.height;
            }

            previewIMGUI.style.width = targetWidth;
            previewIMGUI.style.height = targetHeight;

            previewIMGUI.onGUIHandler = () =>
            {
                if (_targetObject == null) return;

                if (_objectPreviewEditor == null)
                {
                    _objectPreviewEditor = Editor.CreateEditor(_targetObject);
                }

                _objectPreviewEditor.OnPreviewGUI(GUILayoutUtility.GetRect(targetWidth, targetHeight), EditorStyles.objectField);
            };

            return previewIMGUI;
        }

        private void OnPropertyFieldChanged(SerializedPropertyChangeEvent e)
        {
            var newTargetObject = e.changedProperty.objectReferenceValue;

            if (_targetObject == newTargetObject) return;

            _targetObject = newTargetObject;
            OnPreviewDestroy();

            InitElements();
            var previewIMGUI = CreatePreview();
            if (previewIMGUI != null) _preview.Add(previewIMGUI);
        }

        private void OnPreviewDestroy()
        {
            if (_objectPreviewEditor != null)
            {
                Object.DestroyImmediate(_objectPreviewEditor);
                _objectPreviewEditor = null;
            }
        }
    }
}