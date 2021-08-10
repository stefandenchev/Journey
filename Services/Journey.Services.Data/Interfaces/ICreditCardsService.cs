namespace Journey.Services.Data.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Journey.Data.Models;
    using Journey.Web.ViewModels.Profile;

    public interface ICreditCardsService
    {
        public IEnumerable<T> GetAll<T>();

        CreditCard GetById(int id);

        string GetLatestCardNumber(int id);

        T GetByIdToModel<T>(int id);

        Task RemoveById(int id);

        Task CreateAsync(CreateCardInputModel input);

        bool CardExists(string cardNumber);
    }
}
