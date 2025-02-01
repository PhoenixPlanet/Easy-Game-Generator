#if UNITY_EDITOR
using EGG.Attributes;
using EGG.Utils;
using System;
using System.Collections.Generic;

using UnityEditor;
using UnityEngine.UIElements;

namespace EGG.Inspector
{
	[CustomEditor(typeof(object), true)]
	public class EasyInspector : Editor
	{
		private EditorUIElementsFactory _factory;

		public override VisualElement CreateInspectorGUI()
		{
			_factory = new EditorUIElementsFactory();

			var container = new VisualElement();

			var serializedObject = new SerializedObject(target);
			var iterator = serializedObject.GetIterator();
			while (iterator.NextVisible(true))
			{
				if (iterator.depth > 0) continue;

				var modifiers = iterator.GetAttributes<ModifierAttribute>();
				var attributes = iterator.GetAttributes<EGGPropertyAttribute>();
				var methods = iterator.GetEGGMethods();

				if (modifiers != null)
				{
					var showIf = modifiers.Find(modifier => modifier.ModifierType == ModifierType.ShowIf);
					bool show = true;
					if (showIf != null)
					{
						var showIfAttribute = showIf as ShowIfAttribute;
						var (field, parent) = iterator.GetFieldFromSameParent(showIfAttribute.variableName);
						if (field != null && parent != null && field.FieldType == typeof(bool))
						{
							var value = field.GetValue(parent);
							if (value is bool boolValue)
							{
								show = boolValue;
							}
						}
					}

					var hideIf = modifiers.Find(modifier => modifier.ModifierType == ModifierType.HideIf);
					if (hideIf != null)
					{
						var hideIfAttribute = hideIf as HideIfAttribute;
						var (field, parent) = iterator.GetFieldFromSameParent(hideIfAttribute.variableName);
						if (field != null && parent != null && field.FieldType == typeof(bool))
						{
							var value = field.GetValue(parent);
							if (value is bool boolValue)
							{
								show = !boolValue;
							}
						}
					}

					if (!show)
					{
						continue;
					}
				}

				container.Add(_factory.GenerateAppropriateField(iterator, attributes, modifiers, methods));
			}

			var obj = target;
			Type type = obj.GetType();

			List<VisualElement> buttons = _factory.HandleButtonAttribute(type, obj); // Partial method from EasyInspector.Button.cs
			foreach (var button in buttons)
			{
				container.Add(button);
			}

			return container;
		}
	}
}
#endif