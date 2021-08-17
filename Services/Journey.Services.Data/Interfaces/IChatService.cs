namespace Journey.Services.Data.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Journey.Data.Models.Chat;
    using Journey.Web.ViewModels.Chat;

    public interface IChatService
    {
        IEnumerable<Chat> GetChats(string userId);

        Task CreateRoom(string name, string userId);

        T GetChat<T>(int id);

        Task JoinRoom(int chatId, string userId);

        Task<int> CreatePrivateRoom(string rootId, string targetId);

        IEnumerable<Chat> GetPrivateChats(string userId);

        Task<Message> CreateMessage(int chatId, string message, string userId);

        IEnumerable<Chat> GetUserChats(string userId);

        bool CheckRoomPrivacy(int chatId);
    }
}
