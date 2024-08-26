using Microsoft.AspNetCore.SignalR;

namespace Clients.DAL.Hub
{
    public class MessageHub : DynamicHub
    {
        private static int messageCount = 0;

        public async Task SendMessage(string message)
        {
            messageCount++;
            var timestamp = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss");

            await Clients.All.SendAsync("ReceiveMessage", messageCount, message, timestamp);
        }

    }

}
