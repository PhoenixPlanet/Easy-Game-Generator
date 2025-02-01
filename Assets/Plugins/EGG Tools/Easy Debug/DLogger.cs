using System;
using System.Diagnostics;

namespace EGG.DebugTools
{
    public class DLogger
    {
        public static bool LogEnabled { get; set; } = true;

        public static void Log(string message)
        {
            if (!LogEnabled) return;
            UnityEngine.Debug.Log(FormatMessage(message, "green"));
        }

        public static void LogWarning(string message)
        {
            if (!LogEnabled) return;
            UnityEngine.Debug.LogWarning(FormatMessage(message, "yellow"));
        }

        public static void LogError(string message)
        {
            if (!LogEnabled) return;
            UnityEngine.Debug.LogError(FormatMessage(message, "red"));
        }

        public static void Assert(bool condition, string message, Exception exception = null, bool throwInBuild = false)
        {
            if (!LogEnabled) return;
            if (condition) return;
            UnityEngine.Debug.LogError(FormatMessage(message, "red"));

            var ex = exception ?? new Exception(message);

            if (throwInBuild)
            {
                throw ex;
            }
            else
            {
#if UNITY_EDITOR
                throw ex;
#endif
            }
        }

        public static void Assert(string message, Exception exception = null, bool throwInBuild = false)
        {
            Assert(false, message, exception, throwInBuild);
        }

        private static string FormatMessage(string message, string color)
        {
            var stackTrace = new StackTrace(2, true);
            var frame = stackTrace.GetFrame(0);
            var fileName = frame.GetFileName();
            var lineNumber = frame.GetFileLineNumber();
            var method = frame.GetMethod();
            var className = method.DeclaringType?.Name;

            if (!string.IsNullOrEmpty(fileName))
            {
                var assetsIndex = fileName.IndexOf("Assets/");
                if (assetsIndex >= 0)
                {
                    fileName = fileName.Substring(assetsIndex);
                }
            }

            return $"<color={color}>{message}</color>\n<a href=\"{fileName}\" line=\"{lineNumber}\">({fileName}:{className}:{lineNumber})</a>";
        }
    }
}