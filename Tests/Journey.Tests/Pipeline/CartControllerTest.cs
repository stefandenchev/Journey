namespace Journey.Tests.Pipeline
{
    using Journey.Web.Controllers;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    public class CartControllerTest
    {
        [Fact]
        public void GetCartShouldBeForAuthorizedUsersAndReturnView()
               => MyPipeline
              .Configuration()
              .ShouldMap(request => request
                  .WithPath("/Cart")
                  .WithUser())
              .To<CartController>(c => c.Index())
              .Which()
              .ShouldHave()
              .ActionAttributes(attributes => attributes
                  .RestrictingForAuthorizedRequests())
              .AndAlso()
              .ShouldReturn()
              .View();
    }
}
