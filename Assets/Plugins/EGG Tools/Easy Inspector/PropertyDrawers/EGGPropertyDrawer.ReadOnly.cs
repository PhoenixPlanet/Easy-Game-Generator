using UnityEditor;
using UnityEngine;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

using EGG.Attributes;
using EGG.Utils;

namespace EGG.Inspector
{
    public abstract partial class EGGPropertyDrawer
    {
        protected bool IsReadOnly()
        {
            var readOnlyAttr = GetModifier(ModifierType.ReadOnly);
            return readOnlyAttr != null && (readOnlyAttr as ReadOnlyAttribute).readOnly;
        }
    }
}