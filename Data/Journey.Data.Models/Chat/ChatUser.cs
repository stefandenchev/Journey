namespace Journey.Data.Models.Chat
{
    public class ChatUser
    {
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public string ChatId { get; set; }

        public Chat Chat { get; set; }
    }
}
