using NLog;

namespace OfflineStaticProtection.Plugin
{
    /// <summary>
    /// Main plugin class for Torch v1.3.1.
    /// Minimal implementation for clean C# 4.6 build.
    /// </summary>
    public class OfflineStaticProtectionPlugin
    {
        // Logger for debug output
        public static Logger Log;

        // Plugin configuration placeholder
        public static OfflineProtectionConfig Config;

        /// <summary>
        /// Called manually from Torch on plugin load.
        /// Sets up logger and config.
        /// </summary>
        public void Init()
        {
            Log = LogManager.GetLogger("OfflineStaticProtection");
            Config = new OfflineProtectionConfig();
            Log.Info("OfflineStaticProtection initialized (clean build).");
        }
    }

    /// <summary>
    /// Placeholder configuration class.
    /// </summary>
    public class OfflineProtectionConfig
    {
        public bool Debug = true;             // Enable debug logs
        public bool DisableProduction = true; // Disable production blocks while locked
        public int UnlockDelaySeconds = 5;    // Delay before unlocking grids
    }
}
