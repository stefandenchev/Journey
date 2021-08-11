namespace Journey.Tests.Services
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;
    using AutoMapper;
    using Journey.Data;
    using Journey.Data.Common.Repositories;
    using Journey.Data.Models;
    using Journey.Services.Data;
    using Journey.Services.Mapping;
    using Journey.Web.ViewModels;
    using Journey.Web.ViewModels.Games;
    using Journey.Web.ViewModels.Games.Create;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using Xunit;

    using static Journey.Tests.Data.Games;

    public class GamesServiceTest
    {
        private readonly Mock<IDeletableEntityRepository<Game>> gamesRepo;
        private readonly Mock<IDeletableEntityRepository<Language>> languagesRepo;
        private readonly Mock<IDeletableEntityRepository<Tag>> tagsRepo;
        private readonly Mock<IDeletableEntityRepository<Publisher>> publishersRepo;
        private readonly Mock<IDeletableEntityRepository<Genre>> genresRepo;
        private readonly Mock<IRepository<GameTag>> gameTagsRepo;
        private readonly Mock<IRepository<GameLanguage>> gameLanguagesRepo;
        private readonly Mock<IRepository<ApplicationUser>> usersRepo;
        private readonly List<Game> gamesList;
        private readonly GamesService service;

        public GamesServiceTest()
        {
            this.gamesRepo = new Mock<IDeletableEntityRepository<Game>>();
            this.languagesRepo = new Mock<IDeletableEntityRepository<Language>>();
            this.tagsRepo = new Mock<IDeletableEntityRepository<Tag>>();
            this.publishersRepo = new Mock<IDeletableEntityRepository<Publisher>>();
            this.genresRepo = new Mock<IDeletableEntityRepository<Genre>>();
            this.gameTagsRepo = new Mock<IRepository<GameTag>>();
            this.gameLanguagesRepo = new Mock<IRepository<GameLanguage>>();
            this.usersRepo = new Mock<IRepository<ApplicationUser>>();

            this.gamesList = new List<Game>();
            this.service = new GamesService(this.gamesRepo.Object, this.languagesRepo.Object, this.tagsRepo.Object);

            this.gamesRepo.Setup(x => x.All()).Returns(this.gamesList.AsQueryable());
            this.gamesRepo.Setup(x => x.AllAsNoTracking()).Returns(this.gamesList.AsQueryable());
            this.gamesRepo.Setup(x => x.AddAsync(It.IsAny<Game>())).Callback(
                (Game game) => this.gamesList.Add(game));
        }

        [Fact]
        public async Task GameCreateWorksCorrectly()
        {
            CreateGameInputModel game = GetGameInModel();

            await this.service.CreateAsync(game, string.Empty);

            Assert.Single(this.gamesList);
        }

        [Fact]
        public async Task GameCreateThrowsExceptionWithIncompleteInput()
        {
            var game = new CreateGameInputModel
            {
                Title = $"Game Test",
            };

            await Assert.ThrowsAsync<NullReferenceException>(async () => await this.service.CreateAsync(game, string.Empty));
        }

        [Fact]
        public async Task GameCreateThrowsExceptionForInvalidImageExtension()
        {
            CreateGameInputModel game = GetGameInModel();
            IFormFile file = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")), 0, 0, "Data", "dummy.txt");

            game.Images = new List<IFormFile>
                {
                    file,
                };

            var exception = Assert.ThrowsAsync<Exception>(async () => await this.service.CreateAsync(game, string.Empty));

            await Assert.ThrowsAsync<Exception>(async () => await this.service.CreateAsync(game, string.Empty));
            Assert.Equal("Invalid image extension txt", exception.Result.Message);
        }

        [Fact]
        public async Task GetCountWorksCorrectly()
        {
            CreateGameInputModel game = GetGameInModel();
            CreateGameInputModel game2 = GetGameInModel();

            await this.service.CreateAsync(game, string.Empty);
            await this.service.CreateAsync(game2, string.Empty);

            int result = this.service.GetCount();

            Assert.Equal(2, result);
        }

        /*        [Fact]
                public void GetAllAsKeyValuePairsWorksCorrectly()
                {
                    CreateGameInputModel game1 = GetGameInModel();
                    CreateGameInputModel game2 = GetGameInModel();

                    this.service.CreateAsync(game1, string.Empty);
                    this.service.CreateAsync(game2, string.Empty);

                    var games = ThreeGames;
                    foreach (var game in games)
                    {
                        this.gamesRepo.Object.AddAsync(game);
                    }

                    var result = this.service.GetAllAsKeyValuePairs();

                    Assert.Equal(3, result.Count());
                }*/

        [Fact]
        public void ProductService_Given_Product_Id_Should_Get_Product_Name()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            this.gamesRepo.Object.AddAsync(new Game
            {
                Id = 1,
                Title = $"Game Test",
                Description = $"Game Description Test",
                PublisherId = 1,
                MininumRequirements = "min",
                RecommendedRequirements = "rec",
                Price = 9.99m,
                CurrentPrice = 9.99m,
                IsOnSale = false,
                OriginalUrl = "https://www.wingamestore.com/",
                GenreId = 1,
                Drm = "Steam",
                ReleaseDate = new DateTime(2020, 10, 10),
            });

            var result = this.service.GetAll<GameInListViewModel>();

            Assert.NotNull(result); // assert that a result was returned
        }

        /* [Fact]
         public void Test()
         {
             // Arrange
             this.FillDatabase();

             var data = new List<GameInListViewModel>
         {
             new GameInListViewModel{ Id = 1 },
             new GameInListViewModel{ Id = 2 },
             new GameInListViewModel{ Id = 3 }
         }.AsQueryable();

             this.gamesRepo.As<IQueryable<GameInListViewModel>>().Setup(m => m.Provider).Returns(data.Provider);

             // Act
             var result = this.service.GetAll<GameInListViewModel>();

             Assert.Equal(3, result.Count());
         }

         private void FillDatabase()
         {
             this.CleanDatabase();
             this.AddUser();
             this.AddLanguages();
             this.AddTags();
             this.AddPublishers();
             this.AddGenres();
             this.AddGameLanguages();
             this.AddGameTags();
             this.AddGames();
         }

         private void CleanDatabase()
         {
             foreach (var gameLanguage in this.gameLanguagesRepo.Object.All())
             {
                 this.gameLanguagesRepo.Object.Delete(gameLanguage);
             }

             foreach (var gameTag in this.gameTagsRepo.Object.All())
             {
                 this.gameTagsRepo.Object.Delete(gameTag);
             }

             foreach (var language in this.languagesRepo.Object.All())
             {
                 this.languagesRepo.Object.Delete(language);
             }

             foreach (var tag in this.tagsRepo.Object.All())
             {
                 this.tagsRepo.Object.Delete(tag);
             }

             foreach (var genre in this.genresRepo.Object.All())
             {
                 this.genresRepo.Object.Delete(genre);
             }

             foreach (var publisher in this.publishersRepo.Object.All())
             {
                 this.publishersRepo.Object.Delete(publisher);
             }

             foreach (var game in this.gamesRepo.Object.All())
             {
                 this.gamesRepo.Object.Delete(game);
             }

             foreach (var user in this.usersRepo.Object.All())
             {
                 this.usersRepo.Object.Delete(user);
             }

             this.gameLanguagesRepo.Object.SaveChangesAsync();
             this.gameTagsRepo.Object.SaveChangesAsync();
             this.languagesRepo.Object.SaveChangesAsync();
             this.tagsRepo.Object.SaveChangesAsync();
             this.genresRepo.Object.SaveChangesAsync();
             this.publishersRepo.Object.SaveChangesAsync();
             this.gamesRepo.Object.SaveChangesAsync();
             this.usersRepo.Object.SaveChangesAsync();
         }

         private IEnumerable<Game> AddGames()
         {
             *//*            var games = new List<Game>();
             *//*
             this.gamesList.Add(new Game
             {
                 Id = 1,
                 Title = "Test1",
                 Description = "Test1Desc",
                 PublisherId = 1,
                 Drm = "Steam",
                 MininumRequirements = "Test1Min",
                 RecommendedRequirements = "Test1Rec",
                 Price = 9.99m,
                 GenreId = 1,
             });

             this.gamesList.Add(new Game
             {
                 Id = 2,
                 Title = "Test2",
                 Description = "Test2Desc",
                 PublisherId = 2,
                 Drm = "Steam",
                 MininumRequirements = "Test2Min",
                 RecommendedRequirements = "Test2Rec",
                 Price = 19.99m,
                 GenreId = 2,
             });

             this.gamesList.Add(new Game
             {
                 Id = 3,
                 Title = "Test3",
                 Description = "Test3Desc",
                 PublisherId = 3,
                 Drm = "Steam",
                 MininumRequirements = "Test3Min",
                 RecommendedRequirements = "Test3Rec",
                 Price = 29.99m,
                 GenreId = 3,
             });

             foreach (var game in this.gamesList)
             {
                 this.gamesRepo.Object.AddAsync(game);
             }

             this.gamesRepo.Object.SaveChangesAsync();

             return this.gamesList;
         }

         private IEnumerable<Genre> AddGenres()
         {
             var genres = new List<Genre>();

             genres.Add(new Genre
             {
                 Id = 1,
                 Name = "RPG",
             });

             genres.Add(new Genre
             {
                 Id = 2,
                 Name = "Strategy",
             });

             genres.Add(new Genre
             {
                 Id = 3,
                 Name = "Adventure",
             });

             foreach (var genre in genres)
             {
                 this.genresRepo.Object.AddAsync(genre);
             }

             this.genresRepo.Object.SaveChangesAsync();

             return genres;
         }

         private IEnumerable<Language> AddLanguages()
         {
             var languages = new List<Language>();

             languages.Add(new Language
             {
                 Id = 1,
                 Name = "English",
             });

             languages.Add(new Language
             {
                 Id = 2,
                 Name = "Japaneese",
             });

             languages.Add(new Language
             {
                 Id = 3,
                 Name = "Bulgarian",
             });

             foreach (var language in languages)
             {
                 this.languagesRepo.Object.AddAsync(language);
             }

             this.languagesRepo.Object.SaveChangesAsync();

             return languages;
         }

         private IEnumerable<Tag> AddTags()
         {
             var tags = new List<Tag>();

             tags.Add(new Tag
             {
                 Id = 1,
                 Name = "Singleplayer",
             });

             tags.Add(new Tag
             {
                 Id = 2,
                 Name = "Multiplayer",
             });

             tags.Add(new Tag
             {
                 Id = 3,
                 Name = "VR Support",
             });

             foreach (var tag in tags)
             {
                 this.tagsRepo.Object.AddAsync(tag);
             }

             this.tagsRepo.Object.SaveChangesAsync();

             return tags;
         }

         private IEnumerable<Publisher> AddPublishers()
         {
             var publishers = new List<Publisher>();

             publishers.Add(new Publisher
             {
                 Id = 1,
                 Name = "CDPR",
             });

             publishers.Add(new Publisher
             {
                 Id = 2,
                 Name = "Giant Games",
             });

             publishers.Add(new Publisher
             {
                 Id = 3,
                 Name = "Sega",
             });

             foreach (var publisher in publishers)
             {
                 this.publishersRepo.Object.AddAsync(publisher);
             }

             this.publishersRepo.Object.SaveChangesAsync();

             return publishers;
         }

         private IEnumerable<GameLanguage> AddGameLanguages()
         {
             var gamesLanguages = new List<GameLanguage>();

             gamesLanguages.Add(new GameLanguage
             {
                 GameId = 1,
                 LanguageId = 1,
             });

             gamesLanguages.Add(new GameLanguage
             {
                 GameId = 2,
                 LanguageId = 2,
             });

             gamesLanguages.Add(new GameLanguage
             {
                 GameId = 3,
                 LanguageId = 3,
             });

             foreach (var language in gamesLanguages)
             {
                 this.gameLanguagesRepo.Object.AddAsync(language);
             }

             this.gameLanguagesRepo.Object.SaveChangesAsync();

             return gamesLanguages;
         }

         private IEnumerable<GameTag> AddGameTags()
         {
             var gamesTags = new List<GameTag>();

             gamesTags.Add(new GameTag
             {
                 GameId = 1,
                 TagId = 1,
             });

             gamesTags.Add(new GameTag
             {
                 GameId = 2,
                 TagId = 2,
             });

             gamesTags.Add(new GameTag
             {
                 GameId = 3,
                 TagId = 3,
             });

             foreach (var tag in gamesTags)
             {
                 this.gameTagsRepo.Object.AddAsync(tag);
             }

             this.gameTagsRepo.Object.SaveChangesAsync();

             return gamesTags;
         }

         private ApplicationUser AddUser()
         {
             var user = new ApplicationUser()
             {
                 Id = "userId",
                 Email = "test@gmail.com",
                 IsDeleted = false,
                 PasswordHash = "testPass",
                 UserName = "Kaladin",
                 CreatedOn = DateTime.UtcNow,
                 NormalizedUserName = "KALADIN",
                 NormalizedEmail = "TEST@GMAIL.COM",
                 EmailConfirmed = false,
                 SecurityStamp = "SecurityStamp",
                 ConcurrencyStamp = "ConcurrencyStamp",
                 PhoneNumberConfirmed = false,
                 TwoFactorEnabled = false,
                 LockoutEnabled = true,
                 AccessFailedCount = 0,
             };

             this.usersRepo.Object.AddAsync(user);
             this.usersRepo.Object.SaveChangesAsync();

             return user;
         }
     }*/
    }
}
