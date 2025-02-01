using UnityEditor;
using UnityEngine;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

using EGG.Attributes;
using EGG.Utils;
using System.Linq;
using System.Collections.Generic;
using System;
using System.Reflection;

namespace EGG.Inspector
{
    public abstract partial class EGGPropertyDrawer : IDisposable
    {
        protected SerializedProperty _property;
        protected EGGPropertyAttribute _attribute;
        protected List<ModifierAttribute> _modifierAttributes;

        private PropertyField _propertyField;

        private bool _disposed = false;

        protected virtual ModifierType[] InvalidModifiers => null;
        protected abstract VisualElement EGGPropertyGUI();

        #region Static Methods

        private static Dictionary<Type, Type> _drawerDictionary;

        public static EGGPropertyDrawer CreateDrawer(EGGPropertyAttribute attribute, List<ModifierAttribute> modifiers)
        {
            if (_drawerDictionary == null)
            {
                BuildDrawerDictionary();
            }

            if (!_drawerDictionary.ContainsKey(attribute.GetType()))
            {
                if (EGGLog.LogEnabled)
                {
                    EGGLog.LogWarning($"No drawer found for attribute {attribute.GetType().Name}");
                }
                var commonDrawer = new CommonPropertyDrawer();
                commonDrawer.SetAttribute(attribute, modifiers);
                return commonDrawer;
            }

            var drawerType = _drawerDictionary[attribute.GetType()];

            EGGPropertyDrawer drawer = Activator.CreateInstance(drawerType) as EGGPropertyDrawer;
            drawer.SetAttribute(attribute, modifiers);

            return drawer;
        }

        private static void BuildDrawerDictionary()
        {
            if (_drawerDictionary != null)
            {
                _drawerDictionary.Clear();
            }

            _drawerDictionary = new Dictionary<Type, Type>();

            var drawerTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => type.IsSubclassOf(typeof(EGGPropertyDrawer)) && !type.IsAbstract);

            foreach (var drawerType in drawerTypes)
            {
                var bindAttribute = drawerType.GetCustomAttribute<BindEGGPropertyAttributeAttribute>();
                if (bindAttribute != null)
                {
                    _drawerDictionary.Add(bindAttribute.attributeType, drawerType);
                }
            }
        }

        #endregion

        #region Dispose

        ~EGGPropertyDrawer()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool isDisposing)
        {
            if (_disposed) return;

            if (isDisposing)
            {
                // Dispose managed resources
                DisposeManagedResources();
            }

            _disposed = true;
        }

        protected virtual void DisposeManagedResources()
        {
            _propertyField?.Unbind();
            _propertyField = null;
        }

        #endregion

        public void SetAttribute(EGGPropertyAttribute attribute, List<ModifierAttribute> modifiers)
        {
            _attribute = attribute;
            _modifierAttributes = GetValidModifiers(modifiers);
        }

        public VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            this._property = property;

            return EGGPropertyGUI();
        }

        protected PropertyField GetPropertyField()
        {
            if (_propertyField == null)
            {
                _propertyField = new PropertyField(_property);
                _propertyField.BindProperty(_property);
                _propertyField.label = GetLabelString();
                _propertyField.SetEnabled(!IsReadOnly());
            }

            return _propertyField;
        }

        private List<ModifierAttribute> GetValidModifiers(List<ModifierAttribute> modifiers)
        {
            if (modifiers == null || modifiers.Count == 0) return null;

            List<ModifierAttribute> validModifiers = new();
            HashSet<ModifierType> validTypes = new();

            foreach (var modifier in modifiers)
            {
                if (InvalidModifiers != null && InvalidModifiers.Contains(modifier.ModifierType)) continue;

                if (validTypes.Contains(modifier.ModifierType))
                {
                    if (EGGLog.LogEnabled)
                    {
                        EGGLog.LogWarning($"Multiple modifiers of type {modifier.ModifierType} found on the same field. Only one is allowed.");
                    }
                    continue;
                }

                validModifiers.Add(modifier);
                validTypes.Add(modifier.ModifierType);
            }

            if (EGGLog.LogEnabled && validModifiers.Count != modifiers.Count)
            {
                EGGLog.LogWarning($"Some modifiers are not valid for this property drawer:");
                EGGLog.LogWarning($"{string.Join(", ", modifiers.Except(validModifiers).Select(modifier => modifier.GetType().Name))}");
            }

            return validModifiers;
        }

        private bool HasModifier()
        {
            return _modifierAttributes != null && _modifierAttributes.Count != 0;
        }

        private ModifierAttribute GetModifier(ModifierType modifierType)
        {
            if (!HasModifier()) return null;
            return _modifierAttributes.Find(modifier => modifier.ModifierType == modifierType);
        }

        private List<ModifierAttribute> GetModifiersByType(Type modifierType, bool inherit = true)
        {
            if (!HasModifier()) return null;

            if (inherit)
            {
                return _modifierAttributes.FindAll(modifier => modifier.GetType().Is(modifierType));
            }
            else
            {
                return _modifierAttributes.FindAll(modifier => modifier.GetType() == modifierType);
            }
        }
    }
}