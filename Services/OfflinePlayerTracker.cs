using System;
using NLog;
using Sandbox.Game.World;

namespace OfflineStaticProtection.Services
{
    /// <summary>
    /// Tracks player join/leave events and triggers grid lock/unlock operations
    /// </summary>
    public class OfflinePlayerTracker
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();
        private readonly GridLockService _gridLockService;

        public OfflinePlayerTracker(GridLockService gridLockService)
        {
            _gridLockService = gridLockService;
        }

        /// <summary>
        /// Register event handlers for player join/leave
        /// </summary>
        public void Register()
        {
            try
            {
                // Subscribe to player connection events
                MySession.Static.Players.PlayersChanged += OnPlayersChanged;

                Log.Info("Player join/leave tracking registered successfully.");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Failed to register player tracking events.");
            }
        }

        /// <summary>
        /// Unregister event handlers
        /// </summary>
        public void Unregister()
        {
            try
            {
                if (MySession.Static?.Players != null)
                {
                    MySession.Static.Players.PlayersChanged -= OnPlayersChanged;
                }

                Log.Info("Player join/leave tracking unregistered.");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Failed to unregister player tracking events.");
            }
        }

        /// <summary>
        /// Handles player connection state changes
        /// </summary>
        private void OnPlayersChanged(bool added, MyPlayer.PlayerId playerId)
        {
            try
            {
                var steamId = playerId.SteamId;

                if (added)
                {
                    // Player joined - unlock their grids after delay
                    if (OfflineStaticProtection.Plugin.OfflineStaticProtectionPlugin.Config.Debug)
                    {
                        Log.Info($"Player JOINED: SteamID={steamId}");
                    }

                    _ = _gridLockService.UnlockPlayerGridsDelayed(steamId);
                }
                else
                {
                    // Player left - lock their grids immediately
                    if (OfflineStaticProtection.Plugin.OfflineStaticProtectionPlugin.Config.Debug)
                    {
                        Log.Info($"Player LEFT: SteamID={steamId}");
                    }

                    _gridLockService.LockPlayerGrids(steamId);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Error handling player state change");
            }
        }
    }
}