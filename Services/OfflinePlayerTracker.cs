namespace OfflineStaticProtection.Services
{
    /// <summary>
    /// Tracks players going offline or joining the game.
    /// Stub functions: no actual tracking yet.
    /// </summary>
    public static class OfflinePlayerTracker
    {
        /// <summary>
        /// Called when a player leaves the server.
        /// </summary>
        /// <param name="steamId">Steam ID of the player</param>
        public static void OnPlayerLeft(ulong steamId)
        {
            // TODO: Track offline player
        }

        /// <summary>
        /// Called when a player joins the server.
        /// </summary>
        /// <param name="steamId">Steam ID of the player</param>
        public static void OnPlayerJoined(ulong steamId)
        {
            // TODO: Track login
        }
    }
}
