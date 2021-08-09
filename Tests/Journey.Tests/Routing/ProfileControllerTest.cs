namespace Journey.Tests.Routing
{
    using Journey.Web.Controllers;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    public class ProfileControllerTest
    {
        [Fact]
        public void PaymentRouteShouldBeMapped()
             => MyRouting
                 .Configuration()
                 .ShouldMap("/Profile/Payment")
                 .To<ProfileController>(c => c.Payment());

        [Fact]
        public void OrdersRouteShouldBeMapped()
             => MyRouting
                 .Configuration()
                 .ShouldMap("/Profile/Orders")
                 .To<ProfileController>(c => c.Orders(null));

        [Fact]
        public void LibraryRouteShouldBeMapped()
             => MyRouting
                 .Configuration()
                 .ShouldMap("/Profile/Library")
                 .To<ProfileController>(c => c.Library(null));
    }
}
