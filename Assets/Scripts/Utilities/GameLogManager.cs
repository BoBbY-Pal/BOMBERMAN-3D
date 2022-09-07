using UnityEngine;

namespace Utilities
{
    // This script will allow to disable logs in whole project just by changing show logs bool to false.
    public static class GameLogManager  
    {
        private static bool showLogs = false;

        public static void CustomLog(object msg)
        {
            if (!showLogs)
            {
                return;
            }
            
            Debug.Log(msg);
        }
    }
}

