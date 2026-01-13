using System.Collections.Generic;
using System.Threading;
using NLog;

namespace OfflineStaticProtection.Services
{
    /// <summary>
    /// Handles grid locking/unlocking.
    /// </summary>
    public static class GridLockService
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Lock all grids of a player by SteamID.
        /// </summary>
        public static void LockPlayerGrids(ulong steamId)
        {
            Log.Info("[DEBUG] LockPlayerGrids called for SteamID: " + steamId);

            // TODO: Implement actual grid lock using MySession / Sandbox references
            // Minimal version for initial build: does nothing but log
            Log.Info("Simulated: grids locked for SteamID " + steamId);
        }

        /// <summary>
        /// Unlock all grids of a player after a delay.
        /// </summary>
        public static void UnlockPlayerGridsWithDelay(ulong steamId)
        {
            int delay = 30; // placeholder, will move to config
            Log.Info("[DEBUG] UnlockPlayerGridsWithDelay called for SteamID: " + steamId);

            Log.Info("Simulated: grids will unlock in " + delay + " seconds.");

            Thread t = new Thread(delegate ()
            {
                Thread.Sleep(delay * 1000);
                Log.Info("Simulated: grids unlocked for SteamID " + steamId);
            });
            t.Start();
        }
    }
}
