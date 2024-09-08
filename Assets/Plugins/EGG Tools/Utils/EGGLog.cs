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
    }
}