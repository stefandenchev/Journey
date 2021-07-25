namespace Journey.Services.Data.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Journey.Web.ViewModels.Cart;

    public interface IOrderItemsService
    {
        public IEnumerable<T> GetAllOrderItems<T>();

        IEnumerable<T> GetOrderItems<T>(string orderId);

        IEnumerable<int> GetGameIdsFromOrder(string orderId);

        Task CreateOrderItems(IEnumerable<GameInCartViewModel> games, string orderId);
    }
}
