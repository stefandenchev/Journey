namespace Journey.Data.Models.Chat
{
    using System.Collections.Generic;

    using Journey.Data.Common.Models;

    public class Chat : BaseDeletableModel<string>
    {
        public Chat()
        {
            this.Messages = new List<Message>();
            this.Users = new List<ChatUser>();
        }

        public string Name { get; set; }

        public ChatType Type { get; set; }

        public ICollection<Message> Messages { get; set; }

        public ICollection<ChatUser> Users { get; set; }
    }
}
