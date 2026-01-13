namespace OfflineStaticProtection.Config
{
    /// <summary>
    /// Configuration settings for OfflineStaticProtection plugin.
    /// </summary>
    public class OfflineProtectionConfig
    {
        /// <summary>
        /// If true, plugin functionality is active.
        /// </summary>
        public bool Enabled { get; set; } = true;

        /// <summary>
        /// Number of seconds to wait after player logs in before unlocking grids.
        /// </summary>
        public int UnlockDelaySeconds { get; set; } = 30;

        /// <summary>
        /// If true, production blocks will be disabled on lock.
        /// </summary>
        public bool DisableProduction { get; set; } = true;
    }
}
