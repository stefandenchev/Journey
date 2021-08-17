namespace Journey.Web.ViewModels.Chat
{
    using System.Collections.Generic;

    using Journey.Data.Models.Chat;
    using Journey.Services.Mapping;

    public class ChatViewModel : IMapFrom<Chat>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ChatType Type { get; set; }

        public ICollection<MessageViewModel> Messages { get; set; }

        public ICollection<ChatUserViewModel> Users { get; set; }
    }
}
