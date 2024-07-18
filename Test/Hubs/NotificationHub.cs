using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

public class NotificationHub : Hub
{
    // Gửi thông báo đến client với ConnectionId cụ thể
    public async Task SendNotificationToClient(string connectionId, string action, object product)
    {
        await Clients.Client(connectionId).SendAsync("ReceiveNotification", action, product);
    }
}

