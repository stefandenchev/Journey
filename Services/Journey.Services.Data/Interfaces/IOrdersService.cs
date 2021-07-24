namespace Journey.Services.Data.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Journey.Web.ViewModels.Cart;

    public interface IOrdersService
    {
        Task CreateAsync(string orderId, string userId, int creditCardId, decimal total);

        T GetById<T>(string id);

        T GetLatest<T>(string id);

        public IEnumerable<T> GetAll<T>();

        public IEnumerable<T> GetAllOrderItems<T>();

        bool IsInLibrary(string userId, int gameId);

        IEnumerable<int> GetGameIdsFromOrder(string orderId);

        IEnumerable<T> GetOrderItems<T>(string orderId);

        Task CreateOrderItems(IEnumerable<GameInCartViewModel> games, string orderId);
    }
}
