using UnityEngine;

public interface ILogger
{
    void LogInfo(string message);
    void LogWarning(string message);
    void LogError(string message);
}

public class DebugLogger : ILogger {
    public void LogInfo(string message)
    {
        Debug.Log($"[INFO] {message}");
    }

    public void LogWarning(string message)
    {
        Debug.LogWarning($"[WARNING] {message}");
    }

    public void LogError(string message)
    {
        Debug.LogError($"[ERROR] {message}");
    }
}
