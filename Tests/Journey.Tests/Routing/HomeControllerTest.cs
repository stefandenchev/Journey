namespace Journey.Tests.Routing
{
    using Journey.Web.Controllers;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    public class HomeControllerTest
    {
        [Fact]
        public void IndexRouteShouldBeMapped()
          => MyRouting
              .Configuration()
              .ShouldMap("/")
              .To<HomeController>(c => c.Index());

        [Fact]
        public void ErrorRouteShouldBeMapped()
          => MyRouting
              .Configuration()
              .ShouldMap("/Home/Error")
              .To<HomeController>(c => c.Error());

        [Fact]
        public void NotFoundRouteShouldBeMapped()
          => MyRouting
              .Configuration()
              .ShouldMap("/Home/NotFound")
              .To<HomeController>(c => c.NotFound());
    }
}
