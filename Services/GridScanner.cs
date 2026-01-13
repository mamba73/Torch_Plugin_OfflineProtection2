using Sandbox.Game.Entities;
using Torch.API;
using System.Collections.Generic;
using OfflineStaticProtection.Plugin;

namespace OfflineStaticProtection.Services
{
    public static class GridScanner
    {
        public static void LockPlayerGrids(IPlayer player)
        {
            foreach (var grid in GetPlayerGrids(player))
                GridLockService.LockGrid(grid);
        }

        public static void ScheduleUnlock(IPlayer player)
        {
            int delay = OfflineStaticProtectionPlugin.Config.UnlockDelaySeconds;

            for (int i = delay; i > 0; i--)
            {
                int remaining = i;
                Torch.CurrentSession?.Torch.Invoke(() =>
                {
                    ChatUtils.Send(player.SteamId,
                        $"Your grids will unlock in {remaining} seconds.");
                });

                System.Threading.Thread.Sleep(1000);
            }

            foreach (var grid in GetPlayerGrids(player))
                GridLockService.UnlockGrid(grid);

            ChatUtils.Send(player.SteamId, "Your grids are now unlocked.");
        }

        private static IEnumerable<MyCubeGrid> GetPlayerGrids(IPlayer player)
        {
            var result = new List<MyCubeGrid>();
            var identityId = player.IdentityId;

            foreach (var entity in MyEntities.GetEntities())
            {
                if (entity is MyCubeGrid grid)
                {
                    if (OfflineStaticProtectionPlugin.Config.StaticGridsOnly && !grid.IsStatic)
                        continue;

                    if (grid.BigOwners.Contains(identityId))
                        result.Add(grid);
                }
            }

            return result;
        }
    }
}
