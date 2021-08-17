namespace Journey.Web.ViewModels.Chat
{
    using System;
    using System.Collections.Generic;

    using Journey.Data.Models.Chat;
    using Journey.Services.Mapping;

    public class MessageViewModel : IMapFrom<Message>
    {
       public string Name { get; set; }

       public string Text { get; set; }

       public DateTime Timestamp { get; set; }
    }
}
