namespace Journey.Web.ViewComponents
{
    using System.Linq;
    using System.Security.Claims;

    using Journey.Data;
    using Journey.Data.Models.Chat;
    using Journey.Services.Data.Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    public class RoomViewComponent : ViewComponent
    {
        private ApplicationDbContext db;
        private readonly IChatService chatService;

        public RoomViewComponent(ApplicationDbContext db, IChatService chatService)
        {
            this.db = db;
            this.chatService = chatService;
        }

        public IViewComponentResult Invoke()
        {
            var userId = this.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var chats = this.chatService.GetUserChats(userId);

            return this.View(chats);
        }
    }
}
