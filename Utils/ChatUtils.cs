using System;

namespace OfflineStaticProtection.Utils
{
    /// <summary>
    /// Utility class for sending chat messages.
    /// Currently stubbed for clean build.
    /// </summary>
    public static class ChatUtils
    {
        /// <summary>
        /// Send a chat message to a player by Steam ID (stub).
        /// </summary>
        /// <param name="steamId">Player Steam ID</param>
        /// <param name="message">Message text</param>
        public static void Send(long steamId, string message)
        {
            // Stub method: logs message to console for now
            Console.WriteLine("[ChatUtils] Message to {0}: {1}", steamId, message);
        }
    }
}
