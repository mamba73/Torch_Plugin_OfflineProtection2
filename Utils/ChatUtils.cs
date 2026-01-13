using Torch;
using Torch.API.Managers;

namespace OfflineStaticProtection.Utils
{
    public static class ChatUtils
    {
        public static void Send(ulong steamId, string message)
        {
            var chat =
                TorchBase.Instance.Managers.GetManager(typeof(IChatManager)) as IChatManager;

            chat?.SendMessageAsOther(
                "OfflineProtection",
                message,
                steamId);
        }
    }
}
