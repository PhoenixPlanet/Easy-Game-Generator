using EGG.Attributes;
using EGG.EasyInspector;
using EGG.EditorStyle;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class TestClass
{
	[Label("테스트 (Nested int)")] public int a;
}

[System.Serializable]
public struct TestStruct
{
	[Label(TextStyle.StrikeThrough | TextStyle.Bold)] public int nestedInt;
}

public class Test : MonoBehaviour
{
	[InlineSO(false), Label("테스트 (ScriptableObject)")] public TestSO testSO;
	[InlineGO, Label("테스트 (GameObject)")] public GameObject testGO;
	[Label("테스트 (int)")] public int testInt;

	[Label("테스트 (class)")] public TestClass testClass = new();
	[Label("테스트 (struct)")] public TestStruct testStruct;

	[Preview(40), Label("테스트 (Sprite)")] public Sprite testSprite;
	[Preview, Label("테스트 (Texture)")] public Texture testTexture;
	[Preview, Label("테스트 (Material)")] public Material testMaterial;
	[Preview, Label("테스트 (Mesh)")] public Mesh testMesh;
	[Preview, Label("테스트 (GameObject)")] public GameObject testGameObject;

	[EditorButton]
	private void TestMethodd(int a)
	{
		Debug.Log("Test");
		EditorGUIUtility.ShowObjectPicker<Object>(null, true, "", 0);
	}

	[EditorButton]
	private void TestMethod()
	{
		Debug.Log("Test1");
	}

	[EditorButton(true, "Test2")]
	private void TestMethod2()
	{
		Debug.Log("Test2");
	}

	[EditorButton("Test3")]
	private void TestMethod3()
	{
		Debug.Log("Test3");
	}

	[EditorButton(true)]
	private void TestMethod4(int a, string b)
	{
		Debug.Log($"Test4 {a} {b}");
	}
}
