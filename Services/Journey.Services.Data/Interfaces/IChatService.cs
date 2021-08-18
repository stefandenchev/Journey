namespace Journey.Services.Data.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Journey.Data.Models.Chat;
    using Journey.Web.ViewModels.Chat;

    public interface IChatService
    {
        IEnumerable<T> GetChats<T>(string userId);

        Task CreateChat(string name, string chatId, string userId);

        T GetChat<T>(string id);

        Task JoinChat(string chatId, string userId);

        Task<string> CreatePrivateChat(string rootId, string chatId, string targetId);

        IEnumerable<T> GetPrivateChats<T>(string userId);

        Task<Message> CreateMessage(string chatId, string message, string userId);

        IEnumerable<T> GetUserChats<T>(string userId);

        bool CheckChatPrivacy(string chatId);

        IEnumerable<T> GetOtherUsers<T>(string userId);
    }
}
