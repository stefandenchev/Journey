namespace Journey.Data.Models.Chat
{
    using System.Collections.Generic;

    using Journey.Data.Common.Models;

    public class Chat : BaseDeletableModel<int>
    {
        public Chat()
        {
            this.Messages = new List<Message>();
            this.Users = new List<ApplicationUser>();
        }

        public string Name { get; set; }

        public ChatType Type { get; set; }

        public ICollection<Message> Messages { get; set; }

        public ICollection<ApplicationUser> Users { get; set; }
    }
}
