namespace Journey.Tests.Pipeline
{
    using Journey.Web.Controllers;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    public class WishlistControllerTest
    {
        [Fact]
        public void GetWishlistShouldBeForAuthorizedUsersAndReturnView()
               => MyPipeline
              .Configuration()
              .ShouldMap(request => request
                  .WithPath("/Wishlist")
                  .WithUser())
              .To<WishlistController>(c => c.Index())
              .Which()
              .ShouldHave()
              .ActionAttributes(attributes => attributes
                  .RestrictingForAuthorizedRequests())
              .AndAlso()
              .ShouldReturn()
              .View();
    }
}
