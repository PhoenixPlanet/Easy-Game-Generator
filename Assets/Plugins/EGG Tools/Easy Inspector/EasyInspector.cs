#if UNITY_EDITOR
using System;
using System.Collections.Generic;

using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace EGG.EasyInspector
{
	[CustomEditor(typeof(object), true)]
	public partial class EasyInspector : Editor
	{
		public override VisualElement CreateInspectorGUI()
		{
			var container = new VisualElement();

			var serializedObject = new SerializedObject(target);
			var iterator = serializedObject.GetIterator();
			while (iterator.NextVisible(true))
			{
				if (iterator.depth > 0) continue;

				//var propertyField = new PropertyField(iterator);
				var propertyField = GeneratePropertyField(iterator);
				container.Add(propertyField);
			}

			var obj = target;
			Type type = obj.GetType();

			List<VisualElement> buttons = HandleButtonAttribute(type, obj); // Partial method from EasyInspector.Button.cs
			foreach (var button in buttons)
			{
				container.Add(button);
			}

			return container;
		}
	}
}
#endif