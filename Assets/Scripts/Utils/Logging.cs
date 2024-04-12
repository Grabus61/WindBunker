using UnityEngine;

namespace Grabus.Utils
{
    public static class Logging
    {
        public static void Log(string message)
        {
            Debug.Log(message);
        }

        public static void Warn(string message)
        {
            Debug.LogWarning(message);
        }

        public static void Err(string message)
        {
            Debug.LogError(message);
        }
    }
}

