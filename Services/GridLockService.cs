using Sandbox.Game.Entities;
using Sandbox.Game.World;
using Sandbox.ModAPI;
using System.Collections.Generic;
using System.Threading;
using OfflineStaticProtection.Plugin;

namespace OfflineStaticProtection.Services
{
    public static class GridLockService
    {
        public static void LockPlayerGrids(IPlayer player)
        {
            long identityId = GetIdentityId(player);
            foreach (var grid in GetPlayerGrids(identityId))
            {
                grid.DestructibleBlocks = false;

                if (OfflineStaticProtectionPlugin.Config.DisableProduction)
                {
                    foreach (var block in grid.GetFatBlocks())
                    {
                        if (block is IMyProductionBlock prod)
                            prod.Enabled = false;
                    }
                }
            }
        }

        public static void UnlockPlayerGridsWithDelay(IPlayer player)
        {
            long identityId = GetIdentityId(player);
            int delay = OfflineStaticProtectionPlugin.Config.UnlockDelaySeconds;

            new Thread(() =>
            {
                Thread.Sleep(delay * 1000);

                foreach (var grid in GetPlayerGrids(identityId))
                {
                    grid.DestructibleBlocks = true;

                    foreach (var block in grid.GetFatBlocks())
                    {
                        if (block is IMyProductionBlock prod)
                            prod.Enabled = true;
                    }
                }

                Utils.ChatUtils.Send(player.SteamId, "Your grids are now unlocked.");

            }).Start();

            Utils.ChatUtils.Send(
                player.SteamId,
                $"Your grids will unlock in {delay} seconds.");
        }

        private static long GetIdentityId(IPlayer player)
        {
            return MySession.Static.Players.TryGetIdentity(player.SteamId)?.IdentityId ?? 0;
        }

        private static IEnumerable<MyCubeGrid> GetPlayerGrids(long identityId)
        {
            var list = new List<MyCubeGrid>();

            foreach (var e in MyEntities.GetEntities())
            {
                if (e is MyCubeGrid grid && grid.BigOwners.Contains(identityId))
                    list.Add(grid);
            }

            return list;
        }
    }
}
