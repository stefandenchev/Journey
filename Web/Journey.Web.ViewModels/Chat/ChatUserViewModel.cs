namespace Journey.Web.ViewModels.Chat
{
    using Journey.Data.Models.Chat;
    using Journey.Services.Mapping;

    public class ChatUserViewModel : IMapFrom<ChatUser>
    {
        public string UserId { get; set; }

        public int ChatId { get; set; }
    }
}
