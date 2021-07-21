namespace Journey.Web.Controllers
{
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Journey.Services.Data.Interfaces;
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

        public IActionResult Index()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var viewModel = new WishlistIndexViewModel() { Games = this.GetGamesFromWishlist(userId) };

            return this.View(viewModel);
        }

        public async Task<ActionResult> Add(int id)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
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
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
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
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var wishListItem = this.wishlistService.GetById<WishlistGameFormModel>(userId, id);

            if (wishListItem == null)
            {
                return this.RedirectToAction("Index");
            }

            await this.wishlistService.RemoveFromWishlist(userId, id);
            return this.RedirectToAction("Index");
        }

        private IEnumerable<GameInWishlistServiceModel> GetGamesFromWishlist(string userId)
        {
            var gamesInList = this.wishlistService.GetAllForUser<GameInWishlistServiceModel>(userId);

            return gamesInList;
        }
    }
}
