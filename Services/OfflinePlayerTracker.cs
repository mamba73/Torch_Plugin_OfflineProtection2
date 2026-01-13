using System;

namespace OfflineStaticProtection.Services
{
    /// <summary>
    /// Tracks players offline for grid locking.
    /// Currently stubbed for clean build.
    /// </summary>
    public static class OfflinePlayerTracker
    {
        /// <summary>
        /// Called when a player disconnects.
        /// </summary>
        /// <param name="steamId">Player Steam ID</param>
        public static void OnPlayerDisconnected(long steamId)
        {
            // Stub: do nothing, just log
            Console.WriteLine("[OfflinePlayerTracker] Player disconnected: " + steamId);
        }

        /// <summary>
        /// Called when a player reconnects.
        /// </summary>
        /// <param name="steamId">Player Steam ID</param>
        public static void OnPlayerConnected(long steamId)
        {
            // Stub: do nothing, just log
            Console.WriteLine("[OfflinePlayerTracker] Player connected: " + steamId);
        }
    }
}
