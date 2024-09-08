#if UNITY_EDITOR

using System;
using System.Collections;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace EGG.Utils
{
	public static class TypeUtils
	{
		public static bool Is(this Type type, Type basetype)
		{
			return type.IsSubclassOf(basetype) || type == basetype;
		}

		public static object FieldDefaultValue(Type T)
		{
			if (T == typeof(string))
			{
				return "";
			}
			else if (T == typeof(bool))
			{
				return false;
			}
			else if (T == typeof(float))
			{
				return 0f;
			}
			else if (T == typeof(int))
			{
				return 0;
			}
			else if (T.Is(typeof(Enum)))
			{
				return Enum.ToObject(T, 0);
			}
			else if (T.Is(typeof(UnityEngine.Object)))
			{
				return null;
			}
			else if (T == typeof(Color))
			{
				return Color.white;
			}
			else if (T == typeof(Vector2))
			{
				return Vector2.zero;
			}
			else if (T == typeof(Vector2Int))
			{
				return Vector2Int.zero;
			}
			else if (T == typeof(Vector3))
			{
				return Vector3.zero;
			}
			else if (T == typeof(Vector3Int))
			{
				return Vector3Int.zero;
			}
			else if (T == typeof(IList))
			{
				return null;
			}
			else
			{
				return null;
			}
		}

		public static object IMGUIField(string label, Type T, object defaultValue = default)
		{
			object fieldValue;

			if (T == typeof(string))
			{
				fieldValue = EditorGUILayout.TextField(label, (string)defaultValue);
			}
			else if (T == typeof(bool))
			{
				fieldValue = EditorGUILayout.Toggle(label, (bool)defaultValue);
			}
			else if (T == typeof(float))
			{
				fieldValue = EditorGUILayout.FloatField(label, (float)defaultValue);
			}
			else if (T == typeof(int))
			{
				fieldValue = EditorGUILayout.IntField(label, (int)defaultValue);
			}
			else if (T.Is(typeof(Enum)))
			{
				fieldValue = EditorGUILayout.EnumPopup(label, (Enum)defaultValue);
			}
			else if (T.Is(typeof(UnityEngine.Object)))
			{
				fieldValue = EditorGUILayout.ObjectField(label, (UnityEngine.Object)defaultValue, T, true);
			}
			else if (T == typeof(Color))
			{
				fieldValue = EditorGUILayout.ColorField(label, (Color)defaultValue);
			}
			else if (T == typeof(Vector2))
			{
				fieldValue = EditorGUILayout.Vector2Field(label, (Vector2)defaultValue);
			}
			else if (T == typeof(Vector2Int))
			{
				fieldValue = EditorGUILayout.Vector2IntField(label, (Vector2Int)defaultValue);
			}
			else if (T == typeof(Vector3))
			{
				fieldValue = EditorGUILayout.Vector3Field(label, (Vector3)defaultValue);
			}
			else if (T == typeof(Vector3Int))
			{
				fieldValue = EditorGUILayout.Vector3IntField(label, (Vector3Int)defaultValue);
			}
			else if (T == typeof(IList))
			{
				EditorGUILayout.LabelField(label);
				fieldValue = EditorGUILayout.PropertyField((SerializedProperty)defaultValue);
			}
			else
			{
				fieldValue = null;
			}

			return fieldValue;
		}

		public static BindableElement PropertyField(string label, Type T, object defaultValue = default)
		{
			BindableElement fieldValue;

			if (T == typeof(string))
			{
				fieldValue = new TextField();
				(fieldValue as TextField).label = label;
				(fieldValue as TextField).value = (string)defaultValue;
			}
			else if (T == typeof(bool))
			{
				fieldValue = new Toggle();
				(fieldValue as Toggle).label = label;
				(fieldValue as Toggle).value = (bool)defaultValue;
			}
			else if (T == typeof(float))
			{
				fieldValue = new FloatField();
				(fieldValue as FloatField).label = label;
				(fieldValue as FloatField).value = (float)defaultValue;
			}
			else if (T == typeof(int))
			{
				fieldValue = new IntegerField();
				(fieldValue as IntegerField).label = label;
				(fieldValue as IntegerField).value = (int)defaultValue;
			}
			else if (T.Is(typeof(Enum)))
			{
				fieldValue = new EnumField();
				(fieldValue as EnumField).label = label;
				(fieldValue as EnumField).Init((Enum)defaultValue);
			}
			else if (T.Is(typeof(UnityEngine.Object)))
			{
				fieldValue = new ObjectField();
				(fieldValue as ObjectField).label = label;
				(fieldValue as ObjectField).objectType = T;
				(fieldValue as ObjectField).value = (UnityEngine.Object)defaultValue;
			}
			else if (T == typeof(Color))
			{
				fieldValue = new ColorField();
				(fieldValue as ColorField).label = label;
				(fieldValue as ColorField).value = (Color)defaultValue;
			}
			else if (T == typeof(Vector2))
			{
				fieldValue = new Vector2Field();
				(fieldValue as Vector2Field).label = label;
				(fieldValue as Vector2Field).value = (Vector2)defaultValue;
			}
			else if (T == typeof(Vector2Int))
			{
				fieldValue = new Vector2IntField();
				(fieldValue as Vector2IntField).label = label;
				(fieldValue as Vector2IntField).value = (Vector2Int)defaultValue;
			}
			else if (T == typeof(Vector3))
			{
				fieldValue = new Vector3Field();
				(fieldValue as Vector3Field).label = label;
				(fieldValue as Vector3Field).value = (Vector3)defaultValue;
			}
			else if (T == typeof(Vector3Int))
			{
				fieldValue = new Vector3IntField();
				(fieldValue as Vector3IntField).label = label;
				(fieldValue as Vector3IntField).value = (Vector3Int)defaultValue;
			}
			else
			{
				fieldValue = null;
			}

			return fieldValue;
		}
	}
}

#endif