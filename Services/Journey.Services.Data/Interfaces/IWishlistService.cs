namespace Journey.Services.Data.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IWishlistService
    {
        T GetById<T>(string userId, int gameId);

        Task AddToWishlist(string userId, int gameId);

        Task RemoveFromWishlist(string userId, int gameId);

        Task ClearBoughtGamesFromWishlist(string userId, List<int> gameIds);

        IEnumerable<T> GetAllForUser<T>(string userId);

        bool IsInWish(string userId, int gameId);
    }
}
