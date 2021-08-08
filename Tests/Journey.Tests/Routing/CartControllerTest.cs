namespace Journey.Tests.Routing
{
    using Journey.Web.Controllers;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    public class CartControllerTest
    {
        [Fact]
        public void CartRouteShouldBeMapped()
          => MyRouting
              .Configuration()
              .ShouldMap("/Cart")
              .To<CartController>(c => c.Index());
    }
}
