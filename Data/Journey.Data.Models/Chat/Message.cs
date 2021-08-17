namespace Journey.Data.Models.Chat
{
    using System;

    using Journey.Data.Common.Models;

    public class Message : BaseDeletableModel<int>
    {
        public string Name { get; set; }

        public string Text { get; set; }

        public DateTime Timestamp { get; set; }

        public int ChatId { get; set; }

        public Chat Chat { get; set; }
    }
}
