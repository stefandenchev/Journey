namespace Journey.Services.Data.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Journey.Data.Models;

    public interface ICreditCardsService
    {
        public IEnumerable<T> GetAll<T>();

        CreditCard GetById(int id);

        T GetByIdToModel<T>(int id);

        Task RemoveById(int id);
    }
}
