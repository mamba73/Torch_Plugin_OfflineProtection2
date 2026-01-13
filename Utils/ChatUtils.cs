using Sandbox.Game.Multiplayer;

namespace OfflineStaticProtection.Utils
{
    public static class ChatUtils
    {
        public static void Send(ulong steamId, string message)
        {
            MyMultiplayer.Static?.SendMessageToClient(
                0,
                message,
                steamId);
        }
    }
}
