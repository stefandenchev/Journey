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
        public void DetailsShouldReturnViewWithCorrectModelWhenPublicArticleAndAnonymousUser()
            => MyController<GamesController>
        .Instance(instance => instance
            .WithData(Games.GetGames(1))
            .WithUser())
        .Calling(c => c.ById(1))
        .ShouldReturn()
        .View(view => view
            .WithModelOfType<SingleGameViewModel>()
            .Passing(game => game.Id == 1));

        [Fact]
        public void CreatePostShouldReturnViewWithSameModelWhenInvalidModelState()
            => MyController<GamesController>
        .Calling(c => c.Create(With.Default<CreateGameInputModel>()))
        .ShouldHave()
        .InvalidModelState()
        .AndAlso()
        .ShouldReturn()
        .View(With.Default<CreateGameInputModel>());
    }
}
