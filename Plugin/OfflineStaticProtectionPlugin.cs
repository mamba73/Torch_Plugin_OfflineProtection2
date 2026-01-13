using OfflineStaticProtection.Config;

namespace OfflineStaticProtection.Plugin
{
    /// <summary>
    /// Main plugin class for OfflineStaticProtection.
    /// Responsible for initializing plugin and loading configuration.
    /// </summary>
    public class OfflineStaticProtectionPlugin
    {
        /// <summary>
        /// Holds plugin configuration such as enabled, unlock delay, etc.
        /// </summary>
        public static OfflineProtectionConfig Config { get; set; } = new OfflineProtectionConfig();

        /// <summary>
        /// Initialize the plugin.
        /// Currently a stub: no managers or events are hooked.
        /// </summary>
        public void Init()
        {
            // TODO: Initialize Torch managers and subscribe to events
        }
    }
}
