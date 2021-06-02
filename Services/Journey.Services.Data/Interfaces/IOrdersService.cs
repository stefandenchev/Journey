namespace Journey.Services.Data.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Journey.Web.ViewModels.Cart;

    public interface IOrdersService
    {
        Task CreateAsync(OrderViewModel input);

        public IEnumerable<T> GetAll<T>();
    }
}
