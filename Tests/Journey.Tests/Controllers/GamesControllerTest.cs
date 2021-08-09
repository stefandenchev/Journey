namespace Journey.Tests.Controllers
{
    using System.Linq;

    using FakeItEasy;
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
    }
}
