namespace Journey.Tests.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    using Journey.Data.Common.Repositories;
    using Journey.Data.Models;
    using Journey.Services.Data;
    using Journey.Services.Mapping;
    using Journey.Web.ViewModels;
    using Moq;
    using Xunit;

    public class GenresServiceTest
    {
        private readonly Mock<IDeletableEntityRepository<Genre>> genresRepo;
        private readonly List<Genre> genresList;
        private readonly GenresService service;

        public GenresServiceTest()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            this.genresRepo = new Mock<IDeletableEntityRepository<Genre>>();

            this.genresList = new List<Genre>();
            this.service = new GenresService(this.genresRepo.Object);

            this.genresRepo.Setup(x => x.All()).Returns(this.genresList.AsQueryable());
            this.genresRepo.Setup(x => x.AllAsNoTracking()).Returns(this.genresList.AsQueryable());
            this.genresRepo.Setup(x => x.AddAsync(It.IsAny<Genre>())).Callback(
                (Genre item) => this.genresList.Add(item));
        }

        [Fact]
        public async Task GetAllGenresAsKeyValuePairsWorksCorrectly()
        {
            await this.genresRepo.Object.AddAsync(new()
            {
                Id = 1,
                Name = "RPG",
            });

            await this.genresRepo.Object.AddAsync(new()
            {
                Id = 2,
                Name = "Strategy",
            });

            var result = this.service.GetAllAsKeyValuePairs();

            Assert.Equal(2, result.Count());
        }
    }
}
