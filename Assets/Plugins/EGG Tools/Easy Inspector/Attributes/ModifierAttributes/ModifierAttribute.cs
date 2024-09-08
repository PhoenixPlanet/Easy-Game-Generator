using System;
using UnityEngine;

namespace EGG.Attributes
{
    public enum ModifierType
    {
        None,
        Label,
        LabelStyle
    }

    public abstract class ModifierAttribute : EGGAttribute
    {
        public ModifierAttribute()
        {

        }

        public abstract ModifierType ModifierType { get; }
    }
}