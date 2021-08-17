namespace Journey.Web.ViewComponents
{
    using System.Linq;
    using System.Security.Claims;

    using Journey.Data;
    using Journey.Data.Models.Chat;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    public class RoomViewComponent : ViewComponent
    {
        private ApplicationDbContext db;

        public RoomViewComponent(ApplicationDbContext db)
        {
            this.db = db;
        }

        public IViewComponentResult Invoke()
        {
            var userId = this.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var chats = this.db.ChatUsers
                .Include(x => x.Chat)
                .Where(x => x.UserId == userId
                    && x.Chat.Type == ChatType.Room)
                .Select(x => x.Chat)
                .ToList();

            return this.View(chats);
        }
    }
}
