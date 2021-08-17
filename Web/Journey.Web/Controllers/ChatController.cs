namespace Journey.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Journey.Data;
    using Journey.Services.Data.Interfaces;
    using Journey.Web.Hubs;
    using Journey.Web.Infrastructure;
    using Journey.Web.ViewModels.Chat;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.SignalR;

    using static Journey.Common.GlobalConstants;

    [Authorize]
    public class ChatController : Controller
    {
        private readonly IChatService chatService;

        public ChatController(IChatService chatService)
        {
            this.chatService = chatService;
        }

        public IActionResult Index()
        {
            var userId = this.User.GetId();

            var chats = this.chatService.GetChats(userId);

            return this.View(chats);
        }

        [Authorize(Roles = AdministratorRoleName)]

        public async Task<IActionResult> CreateRoom(string name)
        {
            var userId = this.User.GetId();

            await this.chatService.CreateRoom(name, userId);
            return this.RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> JoinRoom(int id)
        {
            var roomPrivate = this.chatService.CheckRoomPrivacy(id);
            if (roomPrivate)
            {
                return this.BadRequest();
            }

            var userId = this.User.GetId();

            await this.chatService.JoinRoom(id, userId);

            return this.RedirectToAction("Chat", "Chat", new { id = id });
        }

        public async Task<IActionResult> SendMessage(
            int roomId,
            string message,
            [FromServices] IHubContext<ChatHub> chat)
        {
            var msg = await this.chatService.CreateMessage(roomId, message, this.User.Identity.Name);

            await chat.Clients.Group(roomId.ToString())
                .SendAsync("RecieveMessage", new
                {
                    Text = msg.Text,
                    Name = msg.Name,
                    Timestamp = msg.Timestamp.ToString("M/dd/yyyy h:mm:ss tt"),
                });

            return this.Ok();
        }

        [HttpGet("/Chat/Find")]
        public IActionResult Find([FromServices] ApplicationDbContext ctx)
        {
            var users = ctx.Users
                .Where(x => x.Id != this.User.GetId())
                .ToList();

            return this.View(users);
        }

        [HttpGet("/Chat/Private")]
        public IActionResult Private()
        {
            var userId = this.User.GetId();

            var chats = this.chatService.GetPrivateChats(userId);

            return this.View(chats);
        }

        [HttpGet("/Chat/CreatePrivateRoom")]
        public async Task<IActionResult> CreatePrivateRoom(string userId)
        {
            var currentUserId = this.User.GetId();

            var id = await this.chatService.CreatePrivateRoom(currentUserId, userId);

            return this.RedirectToAction("Chat", new { id });
        }

        [HttpGet("/Chat/{id}")]
        public IActionResult Chat(int id)
        {
            return this.View(this.chatService.GetChat<ChatViewModel>(id));
        }
    }
}
