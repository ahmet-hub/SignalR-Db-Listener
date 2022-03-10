using Microsoft.AspNetCore.SignalR;

namespace SignalR_Db_Listener.Hubs
{
    public class HubExample : Hub
    {
        public async Task SendMessageAsync(string message)
        {
            await Clients.All.SendAsync("receiveMessage", message);
        }
    }
}
