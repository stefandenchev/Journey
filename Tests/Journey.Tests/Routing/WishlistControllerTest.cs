namespace Journey.Tests.Routing
{
    using Journey.Web.Controllers;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    public class WishlistControllerTest
    {
        [Fact]
        public void WishlistRouteShouldBeMapped()
          => MyRouting
              .Configuration()
              .ShouldMap("/Wishlist")
              .To<WishlistController>(c => c.Index());
    }
}
