namespace Journey.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Journey.Data;
    using Journey.Services.Data.Interfaces;
    using Journey.Web.Hubs;
    using Journey.Web.Infrastructure;
    using Journey.Web.ViewModels.Chat;
    using Journey.Web.ViewModels.Profile;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.SignalR;

    using static Journey.Common.GlobalConstants;

    [Authorize]
    public class ChatController : Controller
    {
        private readonly IChatService chatService;
        private readonly IUsersService usersService;

        public ChatController(
            IChatService chatService,
            IUsersService usersService)
        {
            this.chatService = chatService;
            this.usersService = usersService;
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
            string chatId = Guid.NewGuid().ToString();

            await this.chatService.CreateRoom(name, chatId, userId);
            return this.RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> JoinRoom(string id)
        {
            var roomPrivate = this.chatService.CheckRoomPrivacy(id);
            if (roomPrivate)
            {
                return this.BadRequest();
            }

            var userId = this.User.GetId();

            await this.chatService.JoinRoom(id, userId);

            return this.RedirectToAction("Chat", new { id = id });
        }

        public async Task<IActionResult> SendMessage(
            string roomId,
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

        public IActionResult Find()
        {
            var userId = this.User.GetId();

            var users = this.chatService.GetOtherUsers<UserViewModel>(userId);
            foreach (var user in users)
            {
                user.Profile = new BaseProfileViewModel
                {
                    ImageUrl = this.usersService.GetProfilePicturePath(user.Id),
                };
            }

            return this.View(users);
        }

        public IActionResult Private()
        {
            var userId = this.User.GetId();

            var chats = this.chatService.GetPrivateChats(userId);

            return this.View(chats);
        }

        public async Task<IActionResult> CreatePrivateRoom(string userId)
        {
            var currentUserId = this.User.GetId();
            string chatId = Guid.NewGuid().ToString();

            var id = await this.chatService.CreatePrivateRoom(currentUserId, chatId, userId);

            return this.RedirectToAction("Chat", new { id });
        }

        [HttpGet("/Chat/Room/{id}")]
        public IActionResult Chat(string id)
        {
            return this.View(this.chatService.GetChat<ChatViewModel>(id));
        }
    }
}
