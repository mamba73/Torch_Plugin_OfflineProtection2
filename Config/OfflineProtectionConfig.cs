using Torch;

namespace OfflineStaticProtection.Config
{
    public class OfflineProtectionConfig : ViewModel
    {
        public bool Enabled { get; set; } = true;
        public bool DisableProduction { get; set; } = true;
        public int UnlockDelaySeconds { get; set; } = 30;
        public bool StaticGridsOnly { get; set; } = true;
    }
}
