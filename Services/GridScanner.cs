using Sandbox.Game.Entities;
using Sandbox.Game.World;
using Torch.API;
using System.Collections.Generic;
using OfflineStaticProtection.Plugin;
using OfflineStaticProtection.Utils;

namespace OfflineStaticProtection.Services
{
    public static class GridScanner
    {
        public static void LockPlayerGrids(IPlayer player)
        {
            long identityId = GetIdentityId(player);
            foreach (var grid in GetPlayerGrids(identityId))
                GridLockService.LockGrid(grid);
        }

        public static void ScheduleUnlock(IPlayer player)
        {
            long identityId = GetIdentityId(player);

            ChatUtils.Send(player.SteamId,
                $"Your grids will unlock in {OfflineStaticProtectionPlugin.Config.UnlockDelaySeconds} seconds.");

            TorchBase.Instance.Invoke(() =>
            {
                foreach (var grid in GetPlayerGrids(identityId))
                    GridLockService.UnlockGrid(grid);

                ChatUtils.Send(player.SteamId, "Your grids are now unlocked.");
            }, OfflineStaticProtectionPlugin.Config.UnlockDelaySeconds * 1000);
        }

        private static long GetIdentityId(IPlayer player)
        {
            var identity = MySession.Static.Players.TryGetIdentity(player.SteamId);
            return identity?.IdentityId ?? 0;
        }

        private static IEnumerable<MyCubeGrid> GetPlayerGrids(long identityId)
        {
            var grids = new List<MyCubeGrid>();

            foreach (var entity in MyEntities.GetEntities())
            {
                if (entity is MyCubeGrid grid)
                {
                    if (OfflineStaticProtectionPlugin.Config.StaticGridsOnly && !grid.IsStatic)
                        continue;

                    if (grid.BigOwners.Contains(identityId))
                        grids.Add(grid);
                }
            }

            return grids;
        }
    }
}
