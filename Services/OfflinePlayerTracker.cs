using Torch.API;
using Torch.API.Managers;
using Torch.API.Session;
using Torch.Commands;
using OfflineStaticProtection.Utils;
using OfflineStaticProtection.Services;

namespace OfflineStaticProtection.Services
{
    public class OfflinePlayerTracker
    {
        private IMultiplayerManagerServer _mp;

        public void Attach(ITorchSession session)
        {
            _mp = session.Managers.GetManager<IMultiplayerManagerServer>();
            _mp.PlayerLeft += OnPlayerLeft;
            _mp.PlayerJoined += OnPlayerJoined;
        }

        private void OnPlayerLeft(IPlayer player)
        {
            GridScanner.LockPlayerGrids(player);
        }

        private void OnPlayerJoined(IPlayer player)
        {
            GridScanner.ScheduleUnlock(player);
        }
    }
}
