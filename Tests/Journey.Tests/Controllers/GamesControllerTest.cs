namespace Journey.Tests.Controllers
{
    using Journey.Tests.Data;
    using Journey.Web.Controllers;
    using Journey.Web.ViewModels.Games;
    using Journey.Web.ViewModels.Games.Create;
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
    }
}
