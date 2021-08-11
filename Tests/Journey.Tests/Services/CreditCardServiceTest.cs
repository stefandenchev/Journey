namespace Journey.Tests.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Journey.Data.Common.Repositories;
    using Journey.Data.Models;
    using Journey.Services.Data;
    using Journey.Web.ViewModels.Profile;
    using Moq;
    using Xunit;

    using static Journey.Tests.Data.CreditCards;

    public class CreditCardServiceTest
    {
        private readonly Mock<IDeletableEntityRepository<CreditCard>> creditCardsRepository;
        private readonly CreditCardsService service;
        private readonly List<CreditCard> creditCards;

        public CreditCardServiceTest()
        {
            this.creditCardsRepository = new Mock<IDeletableEntityRepository<CreditCard>>();
            this.creditCards = new List<CreditCard>();
            this.service = new CreditCardsService(this.creditCardsRepository.Object);

            this.creditCardsRepository.Setup(x => x.All()).Returns(this.creditCards.AsQueryable());
            this.creditCardsRepository.Setup(x => x.AllWithDeleted()).Returns(this.creditCards.AsQueryable());
            this.creditCardsRepository.Setup(x => x.AllAsNoTracking()).Returns(this.creditCards.AsQueryable());
            this.creditCardsRepository.Setup(x => x.AllAsNoTrackingWithDeleted()).Returns(this.creditCards.AsQueryable());
            this.creditCardsRepository.Setup(x => x.AddAsync(It.IsAny<CreditCard>())).Callback(
                (CreditCard item) => this.creditCards.Add(item));
            this.creditCardsRepository.Setup(x => x.Delete(It.IsAny<CreditCard>())).Callback(
                (CreditCard item) => this.creditCards.Remove(item));
        }

        [Fact]
        public async Task AddingCreditCardWorksCorrectly()
        {
            var user = new ClaimsPrincipal(new ClaimsIdentity(
                new Claim[]
                {
                     new Claim(ClaimTypes.NameIdentifier, "TestValue"),
                     new Claim(ClaimTypes.Name, "kal@dunno.com"),
                }));

            CreateCardInputModel card = GetCardInModel(user.Identity.Name);

            await this.service.CreateAsync(card);

            Assert.Single(this.creditCards);
        }

        [Fact]
        public async Task CreditCardExistenceCheckWorksCorrectly()
        {
            var user = new ClaimsPrincipal(new ClaimsIdentity(
                new Claim[]
                {
                     new Claim(ClaimTypes.NameIdentifier, "TestValue"),
                     new Claim(ClaimTypes.Name, "kal@dunno.com"),
                }));

            CreateCardInputModel card = GetCardInModel(user.Identity.Name);

            await this.service.CreateAsync(card);

            Assert.True(this.service.CardExists("4567465745674561"));
        }

        [Fact]
        public async Task RemovingCreditCardWorksCorrectly()
        {
            var cards = ThreeCards;
            foreach (var card in cards)
            {
                this.creditCards.Add(card);
            }

            await this.service.RemoveById(1);

            Assert.Equal(2, this.creditCards.Count);
        }

        [Fact]
        public void GetLatestCardWorksCorrectly()
        {
            var cards = ThreeCards;
            foreach (var card in cards)
            {
                this.creditCards.Add(card);
            }

            var result = this.service.GetLatestCardNumber(2);

            Assert.Equal("4444555566667772", result);
        }

        [Fact]
        public void GetByIdWorksCorrectly()
        {
            var cards = ThreeCards;
            foreach (var card in cards)
            {
                this.creditCards.Add(card);
            }

            var result = this.service.GetById(3);

            Assert.Equal("4444555566667773", result.CardNumber);
        }
    }
}
