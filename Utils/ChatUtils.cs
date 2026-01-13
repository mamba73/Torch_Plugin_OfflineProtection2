using NLog;
using Torch.API.Managers;
using Torch;

namespace OfflineStaticProtection.Utils
{
    /// <summary>
    /// Helper utilities for sending chat messages to players
    /// </summary>
    public static class ChatUtils
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Send a chat message to a specific player by SteamID
        /// </summary>
        /// <param name="steamId">Target player's Steam ID</param>
        /// <param name="message">Message text to send</param>
        public static void SendMessage(ulong steamId, string message)
        {
            try
            {
                // Get Torch chat manager
                var chatManager = TorchBase.Instance.Managers?.GetManager<IChatManagerServer>();
                if (chatManager != null)
                {
                    // Send message as system/server to specific player
                    chatManager.SendMessageAsOther("OfflineProtection", message);
                }
                else
                {
                    Log.Warn($"ChatManager not available. Message to {steamId}: {message}");
                }
            }
            catch (System.Exception ex)
            {
                Log.Error(ex, $"Failed to send chat message to {steamId}");
            }
        }
    }
}