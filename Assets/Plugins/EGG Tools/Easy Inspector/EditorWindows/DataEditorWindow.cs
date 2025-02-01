#if UNITY_EDITOR

using EGG.Attributes;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace EGG.Inspector
{
    public abstract class DataEditorWindow<T> : EditorWindow where T : Object
    {
        protected readonly string[] BASE_PATH_ARRAY = new string[] { "Assets/" };

        protected abstract string WindowTitle { get; }
        protected virtual string[] DataPath => BASE_PATH_ARRAY;
        protected virtual float ListItemHeight => 40f;

        private VisualElement contentPanel;
        [SerializeReference] private CreateNew createNew;

        private bool hasCreateNew;

        public void CreateGUI()
        {
            var allObjGuids = AssetDatabase.FindAssets($"t:{typeof(T).Name}", DataPath);
            var allObjs = new List<T>();
            foreach (var guid in allObjGuids)
            {
                allObjs.Add(AssetDatabase.LoadAssetAtPath<T>(AssetDatabase.GUIDToAssetPath(guid)));
            }

            var splitView = new TwoPaneSplitView(0, 250, TwoPaneSplitViewOrientation.Horizontal);
            rootVisualElement.Add(splitView);

            var listView = new ListView();
            contentPanel = new ScrollView(ScrollViewMode.Vertical);
            splitView.Add(listView);
            splitView.Add(contentPanel);

            createNew = CreateNewInstance();
            hasCreateNew = createNew != null;

            List<object> allItems = hasCreateNew ? new() { createNew } : new();
            allItems.AddRange(allObjs);

            listView.makeItem = () => ListItem();
            listView.bindItem = (item, idx) =>
            {
                if (allItems[idx] is T data)
                {
                    (item as Label).text = data.name;
                }
                else if (allItems[idx] is CreateNew createNew)
                {
                    (item as Label).text = createNew.Name;
                }
            };
            listView.itemsSource = allItems;
            listView.selectionType = SelectionType.Single;
            listView.selectionChanged += OnSelected;
            listView.fixedItemHeight = ListItemHeight;
        }

        protected virtual VisualElement ListItem()
        {
            var label = new Label();

            label.style.borderBottomColor = new StyleColor(new Color(153 / 255f, 153 / 255f, 153 / 255f, 0.4f));
            label.style.borderBottomWidth = 1;
            label.style.unityTextAlign = TextAnchor.MiddleLeft;
            label.style.paddingLeft = 20f;

            return label;
        }

        protected virtual void OnSelected(IEnumerable<object> selectedItems)
        {
            contentPanel.Clear();

            var targetObject = selectedItems.First();
            if (targetObject == null) return;

            if (targetObject is T data)
            {
                var inspector = new InspectorElement();
                inspector.Bind(BindingObject(data));
                contentPanel.Add(inspector);
            }
            else if (targetObject is CreateNew)
            {
                SerializedObject editorObj = new SerializedObject(this);
                SerializedProperty property = editorObj.FindProperty(nameof(createNew));
                var propertyField = new PropertyField();
                propertyField.BindProperty(property);
                contentPanel.Add(propertyField);
            }
        }

        protected virtual void OnDestroy()
        {
            if (createNew != null)
            {
                createNew.Destroy();
            }
        }

        protected abstract CreateNew CreateNewInstance();
        protected abstract SerializedObject BindingObject(T targetData);
    }

    [System.Serializable]
    public abstract class CreateNew
    {
        public virtual string Name => "Create New";

        public CreateNew(System.Type type)
        {
            Init(type);
        }

        public abstract void Init(System.Type type);
        public abstract void CreateDataFile(string fileName);
        public abstract void Destroy();
    }
}

#endif