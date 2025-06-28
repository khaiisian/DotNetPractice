using Microsoft.AspNetCore.SignalR;

namespace DotNetPractice.RealtimeChatApp.Hubs
{
    public class ChatHubs: Hub
    {
        public async Task ServerReceiveMessage(string user, string message)
        {
            await Clients.All.SendAsync("ClientReceiveMessage", user, message);
        }
    }
}
