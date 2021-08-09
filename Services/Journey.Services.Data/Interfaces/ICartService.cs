namespace Journey.Services.Data.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICartService
    {
        bool IsInCart(string userId, int gameId);

        IEnumerable<T> GetAllInCart<T>(string userId);

        public T Get<T>(string userId, int gameId);

        Task CreateAsync(string userId, int gameId);

        Task RemoveAsync(string userId, int gameId);

        Task ClearAllAsync(string userId);

        int GetCount(string userId);
    }
}
