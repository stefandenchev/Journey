namespace Journey.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Journey.Data.Common.Repositories;
    using Journey.Data.Models;
    using Journey.Services.Data.Interfaces;
    using Journey.Services.Mapping;
    using Journey.Web.ViewModels.Profile;

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

        public CreditCard GetById(int id)
        {
            var card = this.creditCardsRepository.AllAsNoTrackingWithDeleted()
             .Where(x => x.Id == id)
             .FirstOrDefault();

            return card;
        }

        public T GetByIdToModel<T>(int id)
        {
            var card = this.creditCardsRepository.AllAsNoTrackingWithDeleted()
                .Where(x => x.Id == id)
                .To<T>().FirstOrDefault();

            return card;
        }

        public async Task RemoveById(int id)
        {
            var card = this.creditCardsRepository
                .All()
                .FirstOrDefault(x => x.Id == id);

            this.creditCardsRepository.Delete(card);
            await this.creditCardsRepository.SaveChangesAsync();
        }

        public async Task CreateAsync(CreateCardInputModel input)
        {
            var card = new CreditCard
            {
                UserId = input.UserId,
                CardNumber = input.CardNumber,
                ExpirationDate = input.ExpirationDate,
            };

            await this.creditCardsRepository.AddAsync(card);
            await this.creditCardsRepository.SaveChangesAsync();
        }

        public string GetLatestCardNumber(int id)
        {
            string cardNumber = this.creditCardsRepository
                .AllAsNoTracking()
                .Where(cc => cc.Id == id).FirstOrDefault().CardNumber;

            return cardNumber;
        }
    }
}
