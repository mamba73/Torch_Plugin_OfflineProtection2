using System;
using System.Collections.Generic;
using NLog;
using Torch;
using Torch.API;
using Torch.API.Managers;
using Torch.API.Plugins;
using Torch.API.Session;

using OfflineStaticProtection.Services;

namespace OfflineStaticProtection.Plugin
{
    /// <summary>
    /// Main plugin class that tracks players and locks/unlocks their grids.
    /// </summary>
    public class OfflineStaticProtectionPlugin : TorchPluginBase
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        // Tracks SteamIDs of players currently protected (offline)
        private HashSet<ulong> _protectedPlayers;

        public override void Init(ITorchBase torch)
        {
            base.Init(torch);
            Log.Info("OfflineStaticProtection plugin initialized.");

            _protectedPlayers = new HashSet<ulong>();

            // Subscribe to session state changed
            ITorchSessionManager sessionManager = torch.Managers.GetManager<ITorchSessionManager>();
            if (sessionManager != null)
            {
                sessionManager.SessionStateChanged += OnSessionStateChanged;
            }
        }

        private void OnSessionStateChanged(ITorchSession session, TorchSessionState state)
        {
            if (state == TorchSessionState.Loaded)
            {
                Log.Info("Session loaded, subscribing to multiplayer events.");

                IMultiplayerManagerServer multiplayer = session.Managers.GetManager<IMultiplayerManagerServer>();
                if (multiplayer != null)
                {
                    multiplayer.PlayerJoined += OnPlayerJoined;
                    multiplayer.PlayerLeft += OnPlayerLeft;
                }
            }
        }

        private void OnPlayerLeft(IPlayer player)
        {
            if (player == null) return;

            ulong steamId = player.SteamId;
            Log.Info("Player left: " + player.Name + " (" + steamId + ")");

            GridLockService.LockPlayerGrids(steamId);

            _protectedPlayers.Add(steamId);
            Log.Info("Player " + player.Name + " grids are now locked and tracked as protected.");
        }

        private void OnPlayerJoined(IPlayer player)
        {
            if (player == null) return;

            ulong steamId = player.SteamId;

            if (_protectedPlayers.Contains(steamId))
            {
                Log.Info("Player joined: " + player.Name + " (" + steamId + ") - unlocking grids in configured delay.");
                GridLockService.UnlockPlayerGridsWithDelay(steamId);
                _protectedPlayers.Remove(steamId);
            }
        }
    }
}
