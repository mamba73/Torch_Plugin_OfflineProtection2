using Sandbox.Game.Entities;
using Sandbox.ModAPI;
using OfflineStaticProtection.Plugin;

namespace OfflineStaticProtection.Services
{
    public static class GridLockService
    {
        public static void LockGrid(MyCubeGrid grid)
        {
            grid.DestructibleBlocks = false;

            foreach (var block in grid.GetFatBlocks())
            {
                if (OfflineStaticProtectionPlugin.Config.DisableProduction &&
                    block is IMyProductionBlock prod)
                {
                    prod.Enabled = false;
                }
            }
        }

        public static void UnlockGrid(MyCubeGrid grid)
        {
            grid.DestructibleBlocks = true;

            foreach (var block in grid.GetFatBlocks())
            {
                if (block is IMyProductionBlock prod)
                    prod.Enabled = true;
            }
        }
    }
}
