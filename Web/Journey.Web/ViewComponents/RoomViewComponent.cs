namespace Journey.Web.ViewComponents
{
    using System.Security.Claims;

    using Journey.Services.Data.Interfaces;
    using Journey.Web.ViewModels.Chat;
    using Microsoft.AspNetCore.Mvc;

    public class RoomViewComponent : ViewComponent
    {
        private readonly IChatService chatService;

        public RoomViewComponent(IChatService chatService)
        {
            this.chatService = chatService;
        }

        public IViewComponentResult Invoke()
        {
            var userId = this.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var chats = this.chatService.GetUserChats<ChatViewModel>(userId);

            return this.View(chats);
        }
    }
}
