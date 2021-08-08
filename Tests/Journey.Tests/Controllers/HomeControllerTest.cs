namespace Journey.Tests.Controllers
{
    using Journey.Web.Controllers;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    public class HomeControllerTest
    {
        [Fact]
        public void ErrorShouldReturnView()
            => MyController<HomeController>
                .Instance()
                .Calling(c => c.Error())
                .ShouldReturn()
                .View();

        [Fact]
        public void NotFoundShouldReturnView()
            => MyController<HomeController>
                .Instance()
                .Calling(c => c.NotFound())
                .ShouldReturn()
                .View();
    }
}
