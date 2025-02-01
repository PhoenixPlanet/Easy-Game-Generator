using UnityEngine.UIElements;

namespace EGG.Utils
{
    public static class EGGLog
    {
        public static bool LogEnabled { get; set; } = true;

        public static void Log(string message)
        {
            if (!LogEnabled) return;
            UnityEngine.Debug.Log($"<color=green>{message}</color>");
        }

        public static void LogWarning(string message)
        {
            if (!LogEnabled) return;
            UnityEngine.Debug.LogWarning($"<color=yellow>{message}</color>");
        }

        public static void LogError(string message)
        {
            if (!LogEnabled) return;
            UnityEngine.Debug.LogError($"<color=red>{message}</color>");
        }

        public static void LogEGGDebug(string message)
        {
            if (!LogEnabled) return;
            UnityEngine.Debug.Log($"<color=blue>{message}</color>");
        }

        public static void PrintHierarchy(VisualElement root, int depth = 0)
        {
            if (root == null) return;

            UnityEngine.Debug.Log($"{new string('-', depth * 2)} {root.name} ({root.GetType().Name})");

            foreach (var child in root.Children())
            {
                PrintHierarchy(child, depth + 1);
            }
        }
    }
}