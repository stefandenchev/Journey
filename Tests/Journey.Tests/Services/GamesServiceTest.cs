namespace Journey.Tests.Services
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;

    using Journey.Data.Common.Repositories;
    using Journey.Data.Models;
    using Journey.Services.Data;
    using Journey.Services.Mapping;
    using Journey.Web.ViewModels;
    using Journey.Web.ViewModels.Games;
    using Journey.Web.ViewModels.Games.Create;
    using Microsoft.AspNetCore.Http;
    using Moq;
    using Xunit;

    using static Journey.Tests.Data.Games;

    public class GamesServiceTest
    {
        private readonly Mock<IDeletableEntityRepository<Game>> gamesRepo;
        private readonly Mock<IDeletableEntityRepository<Language>> languagesRepo;
        private readonly Mock<IDeletableEntityRepository<Tag>> tagsRepo;
        private readonly Mock<IRepository<OrderItem>> orderItemsRepo;

        private readonly List<Game> gamesList;
        private readonly GamesService service;

        public GamesServiceTest()
        {
            this.gamesRepo = new Mock<IDeletableEntityRepository<Game>>();
            this.languagesRepo = new Mock<IDeletableEntityRepository<Language>>();
            this.tagsRepo = new Mock<IDeletableEntityRepository<Tag>>();
            this.orderItemsRepo = new Mock<IRepository<OrderItem>>();

            this.gamesList = new List<Game>();
            this.service = new GamesService(
                this.gamesRepo.Object,
                this.languagesRepo.Object,
                this.tagsRepo.Object,
                this.orderItemsRepo.Object);

            this.gamesRepo.Setup(x => x.All()).Returns(this.gamesList.AsQueryable());
            this.gamesRepo.Setup(x => x.AllAsNoTracking()).Returns(this.gamesList.AsQueryable());
            this.gamesRepo.Setup(x => x.AddAsync(It.IsAny<Game>())).Callback(
                (Game game) => this.gamesList.Add(game));
        }

        [Fact]
        public async Task GetAllAsKeyValuePairsWorksCorrectly()
        {
            CreateGameInputModel game1 = GetGameInModel();
            CreateGameInputModel game2 = GetGameInModel();

            await this.service.CreateAsync(game1, string.Empty);
            await this.service.CreateAsync(game2, string.Empty);

            var result = this.service.GetAllAsKeyValuePairs();

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GameCreateShouldWorkCorrectly()
        {
            CreateGameInputModel game = GetGameInModel();

            await this.service.CreateAsync(game, string.Empty);

            Assert.Single(this.gamesList);
        }

        [Fact]
        public void GameCreateShouldThrowExceptionWithIncompleteInput()
        {
            var game = new CreateGameInputModel
            {
                Title = $"Game Test",
            };

            Assert.ThrowsAsync<NullReferenceException>(async () => await this.service.CreateAsync(game, string.Empty));
        }

        [Fact]
        public void GameCreateShouldThrowExceptionForInvalidImageExtension()
        {
            CreateGameInputModel game = GetGameInModel();
            IFormFile file = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")), 0, 0, "Data", "dummy.txt");

            game.Images = new List<IFormFile>
                {
                    file,
                };

            var exception = Assert.ThrowsAsync<Exception>(async () => await this.service.CreateAsync(game, string.Empty));

            Assert.ThrowsAsync<Exception>(async () => await this.service.CreateAsync(game, string.Empty));
            Assert.Equal("Invalid image extension txt", exception.Result.Message);
        }

        [Fact]
        public async Task GetCountShouldReturnCorrectCount()
        {
            CreateGameInputModel game = GetGameInModel();
            CreateGameInputModel game2 = GetGameInModel();

            await this.service.CreateAsync(game, string.Empty);
            await this.service.CreateAsync(game2, string.Empty);

            int result = this.service.GetCount();

            Assert.Equal(2, result);
        }
    }
}
