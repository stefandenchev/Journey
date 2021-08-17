namespace Journey.Data.Models.Chat
{
    using Journey.Data.Common.Models;

    public class ChatUser : BaseModel<int>
    {
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public int ChatId { get; set; }

        public Chat Chat { get; set; }
    }
}