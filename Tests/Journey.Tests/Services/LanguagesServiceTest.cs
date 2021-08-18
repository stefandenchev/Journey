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
    using Journey.Web.ViewModels.Administration.GameLanguage;
    using Moq;
    using Xunit;

    public class LanguagesServiceTest
    {
        private readonly Mock<IDeletableEntityRepository<Language>> languagesRepo;
        private readonly Mock<IRepository<GameLanguage>> gamesLanguagesRepo;

        private readonly List<Language> languagesList;
        private readonly List<GameLanguage> gameLanguagesList;
        private readonly LanguagesService service;

        public LanguagesServiceTest()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            this.languagesRepo = new Mock<IDeletableEntityRepository<Language>>();
            this.gamesLanguagesRepo = new Mock<IRepository<GameLanguage>>();

            this.languagesList = new List<Language>();
            this.gameLanguagesList = new List<GameLanguage>();

            this.service = new LanguagesService(this.languagesRepo.Object, this.gamesLanguagesRepo.Object);

            this.languagesRepo.Setup(x => x.All()).Returns(this.languagesList.AsQueryable());
            this.languagesRepo.Setup(x => x.AllAsNoTracking()).Returns(this.languagesList.AsQueryable());
            this.languagesRepo.Setup(x => x.AddAsync(It.IsAny<Language>())).Callback(
                (Language item) => this.languagesList.Add(item));

            this.gamesLanguagesRepo.Setup(x => x.All()).Returns(this.gameLanguagesList.AsQueryable());
            this.gamesLanguagesRepo.Setup(x => x.AllAsNoTracking()).Returns(this.gameLanguagesList.AsQueryable());
            this.gamesLanguagesRepo.Setup(x => x.AddAsync(It.IsAny<GameLanguage>())).Callback(
                (GameLanguage item) => this.gameLanguagesList.Add(item));
        }

        [Fact]
        public void GetAllGenresAsKeyValuePairsShoulsWorkCorrectly()
        {
            this.languagesRepo.Object.AddAsync(new()
            {
                Id = 1,
                Name = "English",
            });

            this.languagesRepo.Object.AddAsync(new()
            {
                Id = 2,
                Name = "Japanese",
            });

            var result = this.service.GetAllAsKeyValuePairs();

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public void GetByIdShouldReturnCorrectLanguage()
        {
            this.gamesLanguagesRepo.Object.AddAsync(new()
            {
                Id = 1,
                GameId = 21,
                LanguageId = 23,
            });

            this.gamesLanguagesRepo.Object.AddAsync(new()
            {
                Id = 1,
                GameId = 11,
                LanguageId = 13,
            });

            var result = this.service.GetById<GameLanguageAdminInputModel>(1);

            Assert.Equal(23, result.LanguageId);
        }

        [Fact]
        public void GetAllShouldReturnWorkCorrectly()
        {
            this.gamesLanguagesRepo.Object.AddAsync(new()
            {
                Id = 1,
                GameId = 21,
                LanguageId = 23,
            });

            this.gamesLanguagesRepo.Object.AddAsync(new()
            {
                Id = 1,
                GameId = 11,
                LanguageId = 13,
            });

            var result = this.service.GetAll<GameLanguageAdminInputModel>();

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task UpdateShouldReturnWorkCorrectly()
        {
            await this.gamesLanguagesRepo.Object.AddAsync(new()
            {
                Id = 1,
                GameId = 21,
                LanguageId = 23,
            });

            await this.gamesLanguagesRepo.Object.AddAsync(new()
            {
                Id = 2,
                GameId = 11,
                LanguageId = 13,
            });

            GameLanguageAdminInputModel input = new GameLanguageAdminInputModel
            {
                GameId = 21,
                LanguageId = 24,
            };

            await this.service.UpdateAsync(1, input);
            var result = this.service.GetById<GameLanguageAdminInputModel>(1);

            Assert.Equal(24, result.LanguageId);
        }
    }
}
