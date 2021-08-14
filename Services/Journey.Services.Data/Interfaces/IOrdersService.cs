namespace Journey.Services.Data.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IOrdersService
    {
        Task CreateAsync(string orderId, string userId, int creditCardId, decimal total);

        T GetById<T>(string id);

        T GetLatest<T>(string id);

        public IEnumerable<T> GetAll<T>();

        bool IsInLibrary(string userId, int gameId);

        IEnumerable<T> GetOrders<T>(string userId);

        IEnumerable<string> GetOrderIds(string userId);
        int GetGamesBoughtCount(string userId);
    }
}
