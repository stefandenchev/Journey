namespace Journey.Tests.Routing
{
    using Journey.Web.Controllers;
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
    }
}
