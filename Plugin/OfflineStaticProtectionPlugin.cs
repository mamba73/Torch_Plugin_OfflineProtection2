using NLog;
using Torch;
using Torch.API;
using Torch.API.Plugins;
using Torch.API.Session;
using Torch.Session;
using OfflineStaticProtection.Config;
using OfflineStaticProtection.Services;

namespace OfflineStaticProtection.Plugin
{
    public class OfflineStaticProtectionPlugin : TorchPluginBase
    {
        public static OfflineProtectionConfig Config;
        public static OfflineStaticProtectionPlugin Instance;

        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        private OfflinePlayerTracker _playerTracker;

        public override void Init(ITorchBase torch)
        {
            base.Init(torch);
            Instance = this;

            Config = Persistent<OfflineProtectionConfig>.Load(Path.Combine(StoragePath, "OfflineProtection.cfg"));
            _playerTracker = new OfflinePlayerTracker();

            var sessionManager = torch.Managers.GetManager<ITorchSessionManager>();
            sessionManager.SessionStateChanged += OnSessionStateChanged;

            Log.Info("OfflineStaticProtection loaded.");
        }

        private void OnSessionStateChanged(ITorchSession session, TorchSessionState state)
        {
            if (state == TorchSessionState.Loaded)
                _playerTracker.Attach(session);
        }
    }
}
