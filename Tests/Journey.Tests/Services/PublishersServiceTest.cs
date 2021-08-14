namespace Journey.Tests.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Journey.Data.Common.Repositories;
    using Journey.Data.Models;
    using Journey.Services.Data;
    using Moq;
    using Xunit;

    public class PublishersServiceTest
    {
        private readonly Mock<IDeletableEntityRepository<Publisher>> publishersRepo;
        private readonly List<Publisher> publishersList;
        private readonly PublishersService service;

        public PublishersServiceTest()
        {
            this.publishersRepo = new Mock<IDeletableEntityRepository<Publisher>>();

            this.publishersList = new List<Publisher>();
            this.service = new PublishersService(this.publishersRepo.Object);

            this.publishersRepo.Setup(x => x.AllAsNoTracking()).Returns(this.publishersList.AsQueryable());
            this.publishersRepo.Setup(x => x.AddAsync(It.IsAny<Publisher>())).Callback(
                (Publisher item) => this.publishersList.Add(item));
        }

        [Fact]
        public async Task GetAllGenresAsKeyValuePairsWorksCorrectly()
        {
            await this.publishersRepo.Object.AddAsync(new()
            {
                Id = 1,
                Name = "2K",
            });

            await this.publishersRepo.Object.AddAsync(new()
            {
                Id = 2,
                Name = "CDPR",
            });

            await this.publishersRepo.Object.AddAsync(new()
            {
                Id = 3,
                Name = "Giant Games",
            });

            var result = this.service.GetAllAsKeyValuePairs();

            Assert.Equal(3, result.Count());
        }
    }
}
