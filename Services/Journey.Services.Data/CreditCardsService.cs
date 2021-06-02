namespace Journey.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using Journey.Data.Common.Repositories;
    using Journey.Data.Models;
    using Journey.Services.Data.Interfaces;
    using Journey.Services.Mapping;

    public class CreditCardsService : ICreditCardsService
    {
        private readonly IDeletableEntityRepository<CreditCard> creditCardsRepository;

        public CreditCardsService(IDeletableEntityRepository<CreditCard> creditCardsRepository)
        {
            this.creditCardsRepository = creditCardsRepository;
        }

        public IEnumerable<T> GetAll<T>()
        {
            return this.creditCardsRepository.All().To<T>().ToList();
        }
    }
}
