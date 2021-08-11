namespace Journey.Tests.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using FakeItEasy;
    using Journey.Data.Models;
    using Journey.Services.Data.Interfaces;
    using Journey.Web.Controllers;
    using Journey.Web.ViewModels;
    using Journey.Web.ViewModels.Games.Create;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Memory;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    public class GamesControllerTest
    {
        [Fact]
        public void ByIdShouldReturnNotFoundWhenInvalidGameId()
            => MyController<GamesController>
                .Calling(c => c.ById(int.MaxValue))
                .ShouldReturn()
                .Redirect("/Home/NotFound");

        [Fact]
        public void CreateGameShouldHaveRestrictionsForHttpPostOnlyAndAuthorizedUsers()
            => MyController<GamesController>
               .Calling(c => c.Create(With.Empty<CreateGameInputModel>()))
               .ShouldHave()
               .ActionAttributes(attrs => attrs
                   .RestrictingForHttpMethod(HttpMethod.Post)
                   .RestrictingForAuthorizedRequests());

        [Fact]
        public void GetAllShouldWorkCorrectly()
        {
            var fakeGamesService = A.Fake<IGamesService>();
            var fakeGenresService = A.Fake<IGenresService>();
            var fakeLanguagesService = A.Fake<ILanguagesService>();
            var fakePublishersService = A.Fake<IPublishersService>();
            var fakeTagsService = A.Fake<ITagsService>();
            var fakeEnvironment = A.Fake<IWebHostEnvironment>();
            var fakeWishlistService = A.Fake<IWishlistService>();
            var fakeOrdersService = A.Fake<IOrdersService>();
            var fakeCartService = A.Fake<ICartService>();
            var fakeCache = A.Fake<IMemoryCache>();

            A.CallTo(() => fakeGamesService.GetAllInList<GameInListViewModel>(1, 16))
                    .Returns(A.CollectionOfFake<GameInListViewModel>(20));

            var gamesController = new GamesController(
                fakeGamesService,
                fakeGenresService,
                fakeLanguagesService,
                fakeTagsService,
                fakePublishersService,
                fakeEnvironment,
                fakeWishlistService,
                fakeOrdersService,
                fakeCartService,
                fakeCache);

            var result = gamesController.All();

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<GamesListViewModel>(viewResult.Model);
            Assert.Equal(20, model.Games.Count());
        }

        [Fact]
        public void AllShouldReturnView()
             => MyController<GamesController>
                .Instance()
                .Calling(c => c.All(1))
                .ShouldReturn()
                .View(result => result
                    .WithModelOfType<GamesListViewModel>());

        /*        [Fact]
                public void ExportToJsonShouldWorkCorrectlyNew()
                {
                    var fakeGamesService = A.Fake<IGamesService>();
                    var fakeGenresService = A.Fake<IGenresService>();
                    var fakeLanguagesService = A.Fake<ILanguagesService>();
                    var fakePublishersService = A.Fake<IPublishersService>();
                    var fakeTagsService = A.Fake<ITagsService>();
                    var fakeEnvironment = A.Fake<IWebHostEnvironment>();
                    var fakeWishlistService = A.Fake<IWishlistService>();
                    var fakeOrdersService = A.Fake<IOrdersService>();
                    var fakeCartService = A.Fake<ICartService>();
                    var fakeCache = A.Fake<IMemoryCache>();

                    A.CallTo(() => fakeGamesService.GetById<GameInListViewModel>(1))
                            .Returns(A.Fake<GameInListViewModel>());

                    var gamesController = new GamesController(
                        fakeGamesService,
                        fakeGenresService,
                        fakeLanguagesService,
                        fakeTagsService,
                        fakePublishersService,
                        fakeEnvironment,
                        fakeWishlistService,
                        fakeOrdersService,
                        fakeCartService,
                        fakeCache);

                    var result = gamesController.ExportToJson(1);

                    var viewResult = Assert.IsType<ViewResult>(result);
                    Assert.Equal(20, model.Games.Count());
                }*/

        /*        [Fact]
                public void ExportToJsonShouldWorkCorrectly()
             => MyController<GamesController>
                        .Instance()
                        .WithData(data => data
                            .WithEntities(x => x.AddRange(
                                new Game { Id = 1, Title = "Test1"},
                                new Game { Id = 2, Title = "Test2"},
                            )))
                        .Calling(c => c.ExportToJson(1))
                        .ShouldReturn()
                        .File(x => x.WithName("Test1"));*/

        /*        private List<Game> Games()
                {
                    List<Game> games = new();

                    games.Add(new Game
                    {
                        Id = 1,
                        Title = "Eternum EX",
                        CurrentPrice = 9.99m,
                        Description = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa",
                        Drm = "Steam",
                        GenreId = 1,
                        MininumRequirements = "minminminminminminminminminminminminminminminminminminminminminminminminminminminminminminminminminminminmin",
                        RecommendedRequirements = "recrecrecrecrecrecrecrecrecrecrecrecrecrecrecrecrecrecrecrecrecrecrecrecrecrecrecrecrecrecrecrecrecrecrecrecrecrecrecrecrec",
                        Price = 9.99m,
                        ReleaseDate = new DateTime(1995, 1, 1),
                        PublisherId = 1,
                    });

                    return games;
                }*/
    }
}
