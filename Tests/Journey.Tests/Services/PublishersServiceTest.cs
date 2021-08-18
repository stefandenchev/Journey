namespace Journey.Tests.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using Journey.Data.Common.Repositories;
    using Journey.Data.Models;
    using Journey.Services.Data;
    using Journey.Services.Mapping;
    using Journey.Web.ViewModels;
    using Journey.Web.ViewModels.Administration.Publishers;
    using Moq;
    using Xunit;

    public class PublishersServiceTest
    {
        private readonly Mock<IDeletableEntityRepository<Publisher>> publishersRepo;
        private readonly List<Publisher> publishersList;
        private readonly PublishersService service;

        public PublishersServiceTest()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            this.publishersRepo = new Mock<IDeletableEntityRepository<Publisher>>();

            this.publishersList = new List<Publisher>();
            this.service = new PublishersService(this.publishersRepo.Object);

            this.publishersRepo.Setup(x => x.All()).Returns(this.publishersList.AsQueryable());
            this.publishersRepo.Setup(x => x.AddAsync(It.IsAny<Publisher>())).Callback(
                (Publisher item) => this.publishersList.Add(item));
        }

        [Fact]
        public void GetAllGenresAsKeyValuePairsShouldWorkCorrectly()
        {
            this.publishersRepo.Object.AddAsync(new()
            {
                Id = 1,
                Name = "2K",
            });

            this.publishersRepo.Object.AddAsync(new()
            {
                Id = 2,
                Name = "CDPR",
            });

            this.publishersRepo.Object.AddAsync(new()
            {
                Id = 3,
                Name = "Giant Games",
            });

            var result = this.service.GetAllAsKeyValuePairs();

            Assert.Equal(3, result.Count());
        }

        [Fact]
        public void GetAllShouldWorkCorrectly()
        {
            this.publishersRepo.Object.AddAsync(new()
            {
                Id = 1,
                Name = "2K",
            });

            this.publishersRepo.Object.AddAsync(new()
            {
                Id = 2,
                Name = "CDPR",
            });

            this.publishersRepo.Object.AddAsync(new()
            {
                Id = 3,
                Name = "Giant Games",
            });

            var result = this.service.GetAll<PublishersInListViewModel>();

            Assert.Equal(3, result.Count());
        }

    }
}
