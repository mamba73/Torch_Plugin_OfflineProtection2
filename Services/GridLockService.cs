using System;
using System.Collections.Generic;
using Sandbox.Game.Entities;

namespace OfflineStaticProtection.Services
{
    /// <summary>
    /// Service for locking/unlocking player grids.
    /// Currently stubbed for clean build.
    /// </summary>
    public static class GridLockService
    {
        /// <summary>
        /// Locks all grids for a player (stub).
        /// </summary>
        /// <param name="steamId">Player Steam ID</param>
        public static void LockPlayerGrids(long steamId)
        {
            // Stub: do nothing, just log
            Console.WriteLine("[GridLockService] LockPlayerGrids called for SteamID: " + steamId);
        }

        /// <summary>
        /// Unlocks grids with delay (stub).
        /// </summary>
        /// <param name="steamId">Player Steam ID</param>
        public static void UnlockPlayerGridsWithDelay(long steamId)
        {
            // Stub: do nothing, just log
            Console.WriteLine("[GridLockService] UnlockPlayerGridsWithDelay called for SteamID: " + steamId);
        }

        /// <summary>
        /// Returns a list of grids for a player (stub).
        /// </summary>
        /// <param name="identityId">Player identity ID</param>
        /// <returns>Empty list</returns>
        private static IEnumerable<MyCubeGrid> GetPlayerGrids(long identityId)
        {
            // Stub: return empty list
            return new List<MyCubeGrid>();
        }
    }
}
