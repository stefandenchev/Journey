namespace Journey.Tests.Routing
{
    using Journey.Web.Controllers;
    using Journey.Web.ViewModels.Games.Create;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    public class GamesControllerTest
    {
        [Fact]
        public void AllGamesRouteShouldBeMapped()
          => MyRouting
              .Configuration()
              .ShouldMap("/Games/All/1")
              .To<GamesController>(c => c.All(1));

        [Fact]
        public void SingleGameRouteShouldBeMapped()
          => MyRouting
              .Configuration()
              .ShouldMap("/Games/ById/1")
              .To<GamesController>(c => c.ById(1));

        [Fact]
        public void GetCreateShouldBeRoutedCorrectly()
            => MyRouting
              .Configuration()
              .ShouldMap("/Games/Create")
              .To<GamesController>(c => c.Create());

        [Fact]
        public void PostCreateShouldBeRoutedCorrectly()
          => MyRouting
              .Configuration()
              .ShouldMap(request => request
              .WithLocation("/Games/Create")
              .WithMethod(HttpMethod.Post)
              .WithUser())
              .To<GamesController>(c => c.Create(With.Any<CreateGameInputModel>()));

        [Fact]
        public void GetEditShouldBeRoutedCorrectly()
            => MyRouting
              .Configuration()
              .ShouldMap("/Games/Edit/1")
              .To<GamesController>(c => c.Edit(1));
    }
}
