using UnityEngine;

namespace EGG.Attributes
{
    public class DirectorySelectAttribute : EGGPropertyAttribute
    {
        public readonly string defaultPath = "";
        public readonly string buttonLabel = "Select Directory";

        public DirectorySelectAttribute(string buttonLabel = "Select Directory", string defaultPath = "")
        {
            this.defaultPath = defaultPath;
            this.buttonLabel = buttonLabel;
        }
    }
}