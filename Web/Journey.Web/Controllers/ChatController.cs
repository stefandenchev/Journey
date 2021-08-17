namespace Journey.Web.Controllers
{
    using System.Threading.Tasks;

    using Journey.Services.Data.Interfaces;
    using Journey.Web.Infrastructure;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

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

        public async Task<IActionResult> CreateRoom(string name)
        {
            var userId = this.User.GetId();

            await this.chatService.CreateRoom(name, userId);
            return this.RedirectToAction("Index");
        }
    }
}
