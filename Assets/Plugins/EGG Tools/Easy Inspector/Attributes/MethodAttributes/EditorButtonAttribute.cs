using System;

namespace EGG.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class EditorButtonAttribute : EGGAttribute
    {
        public bool forPlayMode { get; set; }
        public string buttonName { get; set; }

        public EditorButtonAttribute(bool forPlayMode = false, string buttonName = "")
        {
            this.forPlayMode = forPlayMode;
            this.buttonName = buttonName;
        }

        public EditorButtonAttribute(string buttonName) : this(false, buttonName) { }

        public EditorButtonAttribute(bool forPlayMode) : this(forPlayMode, "") { }
    }
}