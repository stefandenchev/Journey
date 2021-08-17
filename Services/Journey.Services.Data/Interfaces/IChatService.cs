namespace Journey.Services.Data.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Journey.Data.Models.Chat;
    using Journey.Web.ViewModels.Chat;

    public interface IChatService
    {
        IEnumerable<Chat> GetChats(string userId);

        Task CreateRoom(string name, string chatId, string userId);

        T GetChat<T>(string id);

        Task JoinRoom(string chatId, string userId);

        Task<string> CreatePrivateRoom(string rootId, string chatId, string targetId);

        IEnumerable<Chat> GetPrivateChats(string userId);

        Task<Message> CreateMessage(string chatId, string message, string userId);

        IEnumerable<Chat> GetUserChats(string userId);

        bool CheckRoomPrivacy(string chatId);

        IEnumerable<T> GetOtherUsers<T>(string userId);
    }
}
