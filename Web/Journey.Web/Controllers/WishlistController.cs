namespace Journey.Web.Controllers
{
    using System.Threading.Tasks;

    using Journey.Services.Data.Interfaces;
    using Journey.Web.Infrastructure;
    using Journey.Web.ViewModels.Wishlist;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class WishlistController : BaseController
    {
        private readonly IWishlistService wishlistService;

        public WishlistController(IWishlistService wishlistService)
        {
            this.wishlistService = wishlistService;
        }

        [Authorize]

        public IActionResult Index()
        {
            var userId = this.User.GetId();
            var viewModel = new WishlistIndexViewModel() { Games = this.wishlistService.GetAllForUser<GameInWishlistServiceModel>(userId) };

            return this.View(viewModel);
        }

        public async Task<ActionResult> Add(int id)
        {
            var userId = this.User.GetId();
            var wishListItem = this.wishlistService.GetById<WishlistGameFormModel>(userId, id);

            if (wishListItem != null)
            {
                return this.RedirectToAction("ById", "Games", new { Id = id });
            }

            await this.wishlistService.AddToWishlist(userId, id);
            return this.RedirectToAction("ById", "Games", new { Id = id });
        }

        public async Task<ActionResult> Remove(int id)
        {
            var userId = this.User.GetId();
            var wishListItem = this.wishlistService.GetById<WishlistGameFormModel>(userId, id);

            if (wishListItem == null)
            {
                return this.RedirectToAction("ById", "Games", new { Id = id });
            }

            await this.wishlistService.RemoveFromWishlist(userId, id);
            return this.RedirectToAction("ById", "Games", new { Id = id });
        }

        public async Task<ActionResult> RemoveFromList(int id)
        {
            var userId = this.User.GetId();
            var wishListItem = this.wishlistService.GetById<WishlistGameFormModel>(userId, id);

            if (wishListItem == null)
            {
                return this.RedirectToAction("Index");
            }

            await this.wishlistService.RemoveFromWishlist(userId, id);
            return this.RedirectToAction("Index");
        }
    }
}
