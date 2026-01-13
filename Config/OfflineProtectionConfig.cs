namespace OfflineStaticProtection.Config
{
    public class OfflineProtectionConfig
    {
        public bool Enabled { get; set; } = true;
        public bool DisableProduction { get; set; } = true;
        public int UnlockDelaySeconds { get; set; } = 30;
    }
}
