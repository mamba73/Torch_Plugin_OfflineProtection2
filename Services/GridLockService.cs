using System;
using System.Linq;
using System.Threading.Tasks;
using NLog;
using Sandbox.Game.World;
using Sandbox.Game.Entities;
using VRage.Game;
using Torch.API.Managers;

namespace OfflineStaticProtection.Services
{
    /// <summary>
    /// Handles grid locking, unlocking, and production block management
    /// </summary>
    public class GridLockService
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Lock all grids owned by the specified player (when they go offline)
        /// </summary>
        public void LockPlayerGrids(ulong steamId)
        {
            try
            {
                // Resolve SteamID to in-game IdentityID
                long identityId = MySession.Static.Players.TryGetIdentityId(steamId);
                if (identityId == 0)
                {
                    Log.Warn($"No identity found for SteamID {steamId}");
                    return;
                }

                int lockedCount = 0;
                // Iterate through all grids in the world
                foreach (var grid in MyEntities.GetEntities().OfType<MyCubeGrid>())
                {
                    // Check if this grid is owned by the player
                    if (grid.BigOwners.Contains(identityId))
                    {
                        // Make grid non-editable
                        grid.Editable = false;

                        // Optionally disable production blocks
                        if (OfflineStaticProtection.Plugin.OfflineStaticProtectionPlugin.Config.DisableProduction)
                        {
                            DisableProduction(grid);
                        }

                        lockedCount++;
                        if (OfflineStaticProtection.Plugin.OfflineStaticProtectionPlugin.Config.Debug)
                        {
                            Log.Info($"Grid LOCKED: {grid.DisplayName} (EntityID: {grid.EntityId})");
                        }
                    }
                }

                Log.Info($"Locked {lockedCount} grids for SteamID {steamId} (IdentityID: {identityId})");
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Error locking grids for SteamID {steamId}");
            }
        }

        /// <summary>
        /// Unlock player grids after a configured delay (when they come online)
        /// </summary>
        public async Task UnlockPlayerGridsDelayed(ulong steamId)
        {
            try
            {
                var delay = OfflineStaticProtection.Plugin.OfflineStaticProtectionPlugin.Config.UnlockDelaySeconds;
                if (delay > 0)
                {
                    Log.Info($"Delaying unlock for SteamID {steamId} by {delay} seconds.");
                    await Task.Delay(delay * 1000);
                }

                UnlockPlayerGrids(steamId);
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Error during delayed unlock for SteamID {steamId}");
            }
        }

        /// <summary>
        /// Unlock all grids owned by the specified player
        /// </summary>
        private void UnlockPlayerGrids(ulong steamId)
        {
            try
            {
                // Resolve SteamID to in-game IdentityID
                long identityId = MySession.Static.Players.TryGetIdentityId(steamId);
                if (identityId == 0)
                {
                    Log.Warn($"No identity found for SteamID {steamId} during unlock.");
                    return;
                }

                int unlockedCount = 0;
                // Iterate through all grids in the world
                foreach (var grid in MyEntities.GetEntities().OfType<MyCubeGrid>())
                {
                    // Check if this grid is owned by the player
                    if (grid.BigOwners.Contains(identityId))
                    {
                        // Make grid editable again
                        grid.Editable = true;

                        // Re-enable production blocks
                        EnableProduction(grid);

                        unlockedCount++;
                        if (OfflineStaticProtection.Plugin.OfflineStaticProtectionPlugin.Config.Debug)
                        {
                            Log.Info($"Grid UNLOCKED: {grid.DisplayName} (EntityID: {grid.EntityId})");
                        }
                    }
                }

                Log.Info($"Unlocked {unlockedCount} grids for SteamID {steamId} (IdentityID: {identityId})");
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Error unlocking grids for SteamID {steamId}");
            }
        }

        /// <summary>
        /// Disable all production blocks (assemblers, refineries, etc.) on a grid
        /// </summary>
        private void DisableProduction(MyCubeGrid grid)
        {
            try
            {
                foreach (var block in grid.GetFatBlocks())
                {
                    // Check if block is a production block (assembler, refinery, gas generator, etc.)
                    var prodBlock = block as Sandbox.Game.Entities.Cube.MyProductionBlock;
                    if (prodBlock != null && prodBlock.Enabled)
                    {
                        prodBlock.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Error disabling production on grid {grid.DisplayName}");
            }
        }

        /// <summary>
        /// Re-enable all production blocks on a grid
        /// </summary>
        private void EnableProduction(MyCubeGrid grid)
        {
            try
            {
                foreach (var block in grid.GetFatBlocks())
                {
                    // Check if block is a production block
                    var prodBlock = block as Sandbox.Game.Entities.Cube.MyProductionBlock;
                    if (prodBlock != null && !prodBlock.Enabled)
                    {
                        prodBlock.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Error enabling production on grid {grid.DisplayName}");
            }
        }
    }
}