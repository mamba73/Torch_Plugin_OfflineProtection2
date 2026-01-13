namespace OfflineStaticProtection.Services
{
    /// <summary>
    /// Service responsible for locking and unlocking player grids.
    /// Stub functions: no actual locking implemented yet.
    /// </summary>
    public static class GridLockService
    {
        /// <summary>
        /// Locks all grids of a player identified by SteamId.
        /// </summary>
        /// <param name="steamId">Steam ID of the player</param>
        public static void LockPlayerGrids(ulong steamId)
        {
            // TODO: Implement grid lock logic
        }

        /// <summary>
        /// Unlocks all grids of a player with delay.
        /// </summary>
        /// <param name="steamId">Steam ID of the player</param>
        public static void UnlockPlayerGridsWithDelay(ulong steamId)
        {
            // TODO: Implement delayed unlock
        }
    }
}
