namespace Journey.Services.Data.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Journey.Data.Models.Chat;

    public interface IChatService
    {
        IEnumerable<Chat> GetChats(string userId);

        Task CreateRoom(string name, string userId);
    }
}
