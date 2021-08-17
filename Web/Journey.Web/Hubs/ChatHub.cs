namespace Journey.Web.Hubs
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.SignalR;

    public class ChatHub : Hub
    {
        public Task JoinRoom(string roomId)
        {
            return this.Groups.AddToGroupAsync(this.Context.ConnectionId, roomId);
        }

        public Task LeaveRoom(string roomId)
        {
            return this.Groups.RemoveFromGroupAsync(this.Context.ConnectionId, roomId);
        }
    }
}
