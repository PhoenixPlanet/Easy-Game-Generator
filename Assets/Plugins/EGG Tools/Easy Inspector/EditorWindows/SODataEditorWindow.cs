#if UNITY_EDITOR

using EGG.Attributes;
using UnityEditor;
using UnityEngine;

namespace EGG.Inspector
{
    public abstract class SODataEditorWindow<T> : DataEditorWindow<T> where T : ScriptableObject
    {
        protected override CreateNew CreateNewInstance()
        {
            return new CreateNewSOData(typeof(T));
        }

        protected override SerializedObject BindingObject(T targetData)
        {
            return new SerializedObject(targetData);
        }
    }

    [System.Serializable]
    public class CreateNewSOData : CreateNew
    {
        [SerializeReference, InlineSO(false, EditorStyle.EasyColor.SecondaryDark), ReadOnly(true)] private ScriptableObject data;
        [SerializeField, DirectorySelect] private string saveDirectory;
        private System.Type type;

        public CreateNewSOData(System.Type type) : base(type) { }

        public override void Init(System.Type type)
        {
            this.type = type;
            data = ScriptableObject.CreateInstance(type);
        }

        public override void Destroy()
        {
            if (data != null)
            {
                Object.DestroyImmediate(data);
            }
        }

        [EditorButton(buttonName = "Create")]
        public override void CreateDataFile(string fileName)
        {
            AssetDatabase.CreateAsset(data, $"{saveDirectory}/{fileName}.asset");
            data = ScriptableObject.CreateInstance(type);
        }
    }
}

#endif