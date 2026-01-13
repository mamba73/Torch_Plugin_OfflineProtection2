using System.IO;
using NLog;
using Torch;
using Torch.API.Plugins;
using Torch.API.Session;
using OfflineStaticProtection.Config;
using OfflineStaticProtection.Services;

namespace OfflineStaticProtection.Plugin
{
    public class OfflineStaticProtectionPlugin : TorchPluginBase
    {
        public static OfflineProtectionConfig Config;
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        public override void Init(ITorchBase torch)
        {
            base.Init(torch);

            var persistent =
                Persistent<OfflineProtectionConfig>.Load(
                    Path.Combine(StoragePath, "OfflineProtection.cfg"));

            Config = persistent.Data;
            persistent.Save();

            var sessionManager =
                torch.Managers.GetManager(typeof(ITorchSessionManager)) as ITorchSessionManager;

            sessionManager.SessionStateChanged += OnSessionStateChanged;

            Log.Info("OfflineStaticProtection v2 loaded");
        }

        private void OnSessionStateChanged(ITorchSession session, TorchSessionState state)
        {
            if (state == TorchSessionState.Loaded)
            {
                new OfflinePlayerTracker().Attach(session);
            }
        }
    }
}
