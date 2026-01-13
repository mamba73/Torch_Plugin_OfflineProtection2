using Torch.API;
using Torch.API.Managers;
using Torch.API.Session;

namespace OfflineStaticProtection.Services
{
    public class OfflinePlayerTracker
    {
        public void Attach(ITorchSession session)
        {
            var mp = session.Managers.GetManager<IMultiplayerManagerServer>();
            mp.PlayerLeft += OnPlayerLeft;
            mp.PlayerJoined += OnPlayerJoined;
        }

        private void OnPlayerLeft(IPlayer player)
        {
            GridLockService.LockPlayerGrids(player);
        }

        private void OnPlayerJoined(IPlayer player)
        {
            GridLockService.UnlockPlayerGridsWithDelay(player);
        }
    }
}
