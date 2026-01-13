using System;
using Torch;
using Torch.API;
using Torch.API.Managers;
using Torch.API.Session;
using NLog;
using OfflineStaticProtection.Config;

namespace OfflineStaticProtection.Plugin
{
    /// <summary>
    /// Main plugin class - handles Torch lifecycle and service initialization
    /// </summary>
    public class OfflineStaticProtectionPlugin : TorchPluginBase
    {
        internal static readonly Logger Log = LogManager.GetCurrentClassLogger();
        internal static OfflineProtectionConfig Config;

        private Services.OfflinePlayerTracker _playerTracker;
        private Services.GridLockService _gridLockService;

        /// <summary>
        /// Called when plugin is loaded by Torch
        /// </summary>
        public override void Init(ITorchBase torch)
        {
            base.Init(torch);

            try
            {
                Log.Info("OfflineStaticProtection: Initializing...");

                // Load configuration from XML file
                Config = OfflineProtectionConfig.Load(torch);

                // Check if plugin is disabled in config
                if (!Config.PluginEnabled)
                {
                    Log.Warn("Plugin is DISABLED via config. Set PluginEnabled=true to activate.");
                    return;
                }

                // Initialize core services
                _gridLockService = new Services.GridLockService();
                _playerTracker = new Services.OfflinePlayerTracker(_gridLockService);

                // Register session state listener to know when game is ready
                var sessionManager = torch.Managers.GetManager<ITorchSessionManager>();
                if (sessionManager != null)
                {
                    sessionManager.SessionStateChanged += OnSessionStateChanged;
                    Log.Info("Session state change listener registered.");
                }
                else
                {
                    Log.Error("Failed to get ITorchSessionManager. Plugin may not function correctly.");
                }

                Log.Info("OfflineStaticProtection: Initialization complete.");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Failed to initialize OfflineStaticProtection plugin.");
            }
        }

        /// <summary>
        /// Handles game session state changes (loading, loaded, unloading)
        /// </summary>
        private void OnSessionStateChanged(ITorchSession session, TorchSessionState state)
        {
            try
            {
                if (state == TorchSessionState.Loaded)
                {
                    // Game world is loaded - safe to register player events
                    Log.Info("Session loaded, registering player tracking.");
                    _playerTracker?.Register();
                }
                else if (state == TorchSessionState.Unloading)
                {
                    // Game world is unloading - cleanup event handlers
                    Log.Info("Session unloading, unregistering player tracking.");
                    _playerTracker?.Unregister();
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Error during session state change to {state}");
            }
        }

        /// <summary>
        /// Called when plugin is unloaded (server shutdown or plugin reload)
        /// </summary>
        public override void Dispose()
        {
            try
            {
                Log.Info("OfflineStaticProtection: Disposing...");
                _playerTracker?.Unregister();
                base.Dispose();
                Log.Info("OfflineStaticProtection: Disposed successfully.");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error during plugin disposal.");
            }
        }
    }
}