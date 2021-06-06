namespace Journey.Services.Data.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Journey.Web.ViewModels.Cart;

    public interface IOrdersService
    {
        Task CreateAsync(OrderViewModel input);

        T GetById<T>(string id);

        public IEnumerable<T> GetAll<T>();

        public IEnumerable<T> GetAllOrderItems<T>();
    }
}
