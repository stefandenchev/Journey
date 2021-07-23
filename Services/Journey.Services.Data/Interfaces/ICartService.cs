namespace Journey.Services.Data.Interfaces
{
    using System.Threading.Tasks;

    public interface ICartService
    {
        bool IsInCart(string userId, int gameId);

        public T Get<T>(string userId, int gameId);

        Task CreateAsync(string userId, int gameId);

        Task RemoveAsync(string userId, int gameId);

        Task ClearAllAsync(string userId);

        int GetCount(string userId);

    }
}
