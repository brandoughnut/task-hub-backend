using Microsoft.AspNetCore.SignalR;
using task_hub_backend.Models;

namespace task_hub_backend.Hubs;

    public class ChatHub : Hub
    {
        public async Task JoinSpecificChatRoom(UserConnection conn)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, conn.Room);
            await Clients.Group(conn.Room)
            .SendAsync("ReceiveMessage", "admin");
        }
    }
