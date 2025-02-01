#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

using EGG.Utils;
using EGG.Attributes;
using EGG.EditorStyle;

namespace EGG.Inspector
{
	public partial class EditorUIElementsFactory
	{
		// TODO: 후에 중복되는 코드 제거 필요.
		public List<VisualElement> HandleButtonAttribute(Type targetType, object targetObject)
		{
			var buttonMethods = targetType.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static)
										  .Where(m => m.GetCustomAttributes(typeof(EditorButtonAttribute), true).Length > 0)
										  .ToArray();

			List<VisualElement> buttons = new();

			foreach (var method in buttonMethods)
			{
				Button button = new Button();

				EditorButtonAttribute attr = method.GetCustomAttribute(typeof(EditorButtonAttribute), true) as EditorButtonAttribute;

				if (attr.forPlayMode &&
					!EditorApplication.isPlaying)
				{
					button.SetEnabled(false);
				}
				else
				{
					button.SetEnabled(true);
				}

				if (attr.buttonName == "")
				{
					button.text = method.Name;
				}
				else
				{
					button.text = attr.buttonName;
				}

				var parameters = method.GetParameters();
				if (parameters.Length == 0)
				{
					button.clicked += () => method.Invoke(targetObject, null);

					buttons.Add(button);
				}
				else
				{
					var buttonBox = new ContentBox(button.text, backgroundColor: StyleUtils.EditorThemeColor * StyleUtils.COLOR_DARKEN_FACTOR * StyleUtils.COLOR_DARKEN_FACTOR);
					buttonBox.SetMargin(5);

					List<VisualElement> args = new();
					foreach (var param in parameters)
					{
						var defaultValue = param.HasDefaultValue ? param.DefaultValue : TypeUtils.FieldDefaultValue(param.ParameterType);

						VisualElement val;
						/*if (param.ParameterType is IList)
                        {
                            val = new ArrayField
                        }
                        else*/
						{
							val = TypeUtils.PropertyField(param.Name, param.ParameterType, defaultValue);
						}
						val.SetMarginHorizontal(10);

						args.Add(val);
						buttonBox.Add(val);
					}

					button.clicked += () => method.Invoke(targetObject, args.Select(a =>
					{
						if (a is BindableElement bindableElement)
						{
							return bindableElement.GetType().GetProperty("value").GetValue(bindableElement);
						}
						else
						{
							return null;
						}
					}).ToArray());

					buttonBox.Add(button);
					buttons.Add(buttonBox);
				}
			}

			return buttons;
		}

		public List<VisualElement> HandleButtonAttribute(SerializedProperty property, List<MethodAttributePair> methods)
		{
			List<VisualElement> buttons = new();
			SerializedProperty targetProperty = property.Copy();

			foreach (var method in methods)
			{
				Button button = new Button();

				EditorButtonAttribute attr = method.attribute;

				if (attr.forPlayMode &&
					!EditorApplication.isPlaying)
				{
					button.SetEnabled(false);
				}
				else
				{
					button.SetEnabled(true);
				}

				if (attr.buttonName == "")
				{
					button.text = method.method.Name;
				}
				else
				{
					button.text = attr.buttonName;
				}

				var parameters = method.method.GetParameters();
				if (parameters.Length == 0)
				{
					targetProperty.serializedObject.Update();
					button.clicked += () => method.method.Invoke(targetProperty.boxedValue, null);

					buttons.Add(button);
				}
				else
				{
					var buttonBox = new ContentBox(button.text, backgroundColor: StyleUtils.EditorThemeColor * StyleUtils.COLOR_DARKEN_FACTOR * StyleUtils.COLOR_DARKEN_FACTOR);
					buttonBox.SetMargin(5);

					List<VisualElement> args = new();
					foreach (var param in parameters)
					{
						var defaultValue = param.HasDefaultValue ? param.DefaultValue : TypeUtils.FieldDefaultValue(param.ParameterType);

						VisualElement val;
						/*if (param.ParameterType is IList)
                        {
                            val = new ArrayField
                        }
                        else*/
						{
							val = TypeUtils.PropertyField(param.Name, param.ParameterType, defaultValue);
						}
						val.SetMarginHorizontal(10);

						args.Add(val);
						buttonBox.Add(val);
					}

					targetProperty.serializedObject.Update();
					button.clicked += () => method.method.Invoke(targetProperty.boxedValue, args.Select(a =>
					{
						if (a is BindableElement bindableElement)
						{
							return bindableElement.GetType().GetProperty("value").GetValue(bindableElement);
						}
						else
						{
							return null;
						}
					}).ToArray());

					buttonBox.Add(button);
					buttons.Add(buttonBox);
				}
			}

			return buttons;
		}
	}
}
#endif