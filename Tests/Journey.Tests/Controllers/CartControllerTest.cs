namespace Journey.Tests.Controllers
{
    using Journey.Web.Controllers;
    using Journey.Web.ViewModels.Cart;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    public class CartControllerTest
    {
        [Fact]
        public void AddToCartShouldWorkCorrectly()
        => MyController<CartController>
           .Instance()
           .WithUser()
           .Calling(c => c.Add(1))
           .ShouldReturn()
           .RedirectToAction("Index");

        [Fact]
        public void RemoveFromCartShouldWorkCorrectly()
        => MyController<CartController>
           .Instance()
           .WithUser()
           .Calling(c => c.Remove(1))
           .ShouldReturn()
           .Json();

        [Fact]
        public void ClearAllShouldWorkCorrectly()
        => MyController<CartController>
           .Instance()
           .WithUser()
           .Calling(c => c.ClearAll())
           .ShouldReturn()
           .RedirectToAction("Index");

        [Fact]
        public void CheckoutShouldReturnView()
        => MyController<CartController>
           .Instance()
           .WithUser()
           .Calling(c => c.Checkout())
           .ShouldReturn()
           .View(x => x.WithModelOfType<CheckoutViewModel>());
    }
}
