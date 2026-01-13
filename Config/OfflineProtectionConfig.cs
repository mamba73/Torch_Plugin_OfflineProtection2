using System;
using System.IO;
using System.Xml.Serialization;
using Torch;
using Torch.API;
using NLog;

namespace OfflineStaticProtection.Config
{
    /// <summary>
    /// Plugin configuration - loaded from XML file
    /// </summary>
    [XmlRoot("OfflineProtectionConfig")]
    public class OfflineProtectionConfig
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Master switch - set to false to disable entire plugin
        /// </summary>
        public bool PluginEnabled { get; set; } = true;

        /// <summary>
        /// Enable verbose debug logging (shows every grid lock/unlock operation)
        /// </summary>
        public bool Debug { get; set; } = false;

        /// <summary>
        /// If true, disable assemblers/refineries/etc. while player is offline
        /// </summary>
        public bool DisableProduction { get; set; } = true;

        /// <summary>
        /// Delay in seconds before unlocking grids after player reconnects
        /// Set to 0 for instant unlock
        /// </summary>
        public int UnlockDelaySeconds { get; set; } = 30;

        /// <summary>
        /// Load configuration from XML file, or create default if not found
        /// </summary>
        public static OfflineProtectionConfig Load(ITorchBase torch)
        {
            var path = Path.Combine(torch.Config.InstancePath, "OfflineStaticProtection.cfg");

            try
            {
                // Create default config if file doesn't exist
                if (!File.Exists(path))
                {
                    var cfg = new OfflineProtectionConfig();
                    Save(path, cfg);
                    Log.Warn($"Config file created with default values at: {path}");
                    return cfg;
                }

                // Deserialize existing config from XML
                using (var stream = File.OpenRead(path))
                {
                    var serializer = new XmlSerializer(typeof(OfflineProtectionConfig));
                    var config = (OfflineProtectionConfig)serializer.Deserialize(stream);
                    Log.Info("Config loaded successfully.");
                    return config;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Failed to load config. Using defaults.");
                return new OfflineProtectionConfig();
            }
        }

        /// <summary>
        /// Save configuration to XML file
        /// </summary>
        private static void Save(string path, OfflineProtectionConfig cfg)
        {
            try
            {
                using (var stream = File.Create(path))
                {
                    var serializer = new XmlSerializer(typeof(OfflineProtectionConfig));
                    serializer.Serialize(stream, cfg);
                }
                Log.Info("Config saved successfully.");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Failed to save config.");
            }
        }
    }
}