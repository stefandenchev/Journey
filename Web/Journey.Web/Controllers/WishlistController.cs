namespace Journey.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Journey.Data;
    using Journey.Data.Models;
    using Journey.Services.Data.Interfaces;
    using Journey.Web.ViewModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Routing;

    [Authorize]
    public class WishlistController : BaseController
    {
        private readonly ApplicationDbContext db;
        private readonly IGamesService gamesService;

        public WishlistController(ApplicationDbContext db, IGamesService gamesService)
        {
            this.db = db;
            this.gamesService = gamesService;
        }

        public IActionResult Index()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var viewModel = new GamesListViewModel();

            List<GameInListViewModel> games = this.GetGamesFromWishlist(userId);
            viewModel.Games = games;

            return this.View(viewModel);
        }

        public async Task<ActionResult> Add(int id)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (this.db.Wishlists.Any(c => c.UserId == userId && c.GameId == id))
            {
                return this.RedirectToAction("ById", new RouteValueDictionary(new { controller = "Games", action = "ById", Id = id }));
            }

            var newWish = new Wishlist() { UserId = userId, GameId = id };
            this.db.Wishlists.Add(newWish);
            await this.db.SaveChangesAsync();
            return this.RedirectToAction("ById", new RouteValueDictionary(new { controller = "Games", action = "ById", Id = id }));
        }

        public async Task<ActionResult> Remove(int id)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (!this.db.Wishlists.Any(c => c.UserId == userId && c.GameId == id))
            {
                this.TempData["message"] = "This game is not in your wish list.";
                return this.RedirectToAction("ById", new RouteValueDictionary(new { controller = "Games", action = "ById", Id = id }));
            }

            Wishlist wish = this.db.Wishlists.FirstOrDefault(c => c.UserId == userId && c.GameId == id);
            this.db.Wishlists.Remove(wish);
            await this.db.SaveChangesAsync();
            return this.RedirectToAction("ById", new RouteValueDictionary(new { controller = "Games", action = "ById", Id = id }));
        }

        public async Task<ActionResult> RemoveFromList(int id)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (!this.db.Wishlists.Any(c => c.UserId == userId && c.GameId == id))
            {
                this.TempData["message"] = "This game is not in your wish list.";
                return this.RedirectToAction("Index");
            }

            Wishlist wish = this.db.Wishlists.FirstOrDefault(c => c.UserId == userId && c.GameId == id);
            this.db.Wishlists.Remove(wish);
            await this.db.SaveChangesAsync();
            return this.RedirectToAction("Index");
        }

        private List<GameInListViewModel> GetGamesFromWishlist(string userId)
        {
            var gamesInWishlist = this.db.Wishlists.Where(c => c.UserId == userId).ToList();
            var gamesInDB = this.db.Games;

            var games = new List<GameInListViewModel>();

            foreach (var game in gamesInWishlist)
            {
                var dbGame = gamesInDB.FirstOrDefault(g => g.Id == game.GameId);
                if (dbGame != null)
                {
                    var gameToAdd = this.gamesService.GetById<GameInListViewModel>(dbGame.Id);

                    games.Add(gameToAdd);
                }
            }

            return games;
        }
    }
}
