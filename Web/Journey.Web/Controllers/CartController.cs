namespace Journey.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Journey.Data;
    using Journey.Data.Models;
    using Journey.Services.Data.Interfaces;
    using Journey.Web.ViewModels.Cart;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class CartController : BaseController
    {
        private readonly ApplicationDbContext db;
        private readonly IGamesService gamesService;

        public CartController(ApplicationDbContext db, IGamesService gamesService)
        {
            this.db = db;
            this.gamesService = gamesService;
        }

        public ActionResult Index()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (userId == null)
            {
                return this.RedirectToAction("Index", "Home");
            }

            var viewModel = new CartViewModel();

            List<GameInCartViewModel> games = this.GetGamesFromCart(userId);
            viewModel.GamesInCart = games;

            viewModel.Total = games.Sum(g => g.Price);

            return this.View(viewModel);
        }

        public async Task<ActionResult> Add(int id)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (this.db.UserCartItems.Any(c => c.UserId == userId && c.GameId == id))
            {
                // add displaying errors to the home page
                // return RedirectToAction("ViewGame", "Home", new { area = "" });
                this.TempData["message"] = "This game is already in your cart.";

                // return this.RedirectToAction("ById", new RouteValueDictionary(new { controller = "Games", action = "ById", Id = id }));
                return this.RedirectToAction("Index", "Cart");
            }

            var newCartItem = new UserCartItem() { UserId = userId, GameId = id };
            this.db.UserCartItems.Add(newCartItem);
            await this.db.SaveChangesAsync();

            var a = this.HttpContext.Session.GetString("cart");
            this.HttpContext.Session.SetString("cart", this.db.UserCartItems.Where(c => c.UserId == userId).Count().ToString());

            return this.RedirectToAction("Index");

            // Session["cart"] = this.db.UserCartItems.Where(c => c.UserId == userId).Count().ToString();
            // return this.RedirectToAction("ViewGame", new RouteValueDictionary(new { controller = "Home", action = "ViewGame", Id = id }));
        }

        [HttpGet]
        public JsonResult RemoveItem(int gameId)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var gameInCart = this.db.UserCartItems.FirstOrDefault(c => c.UserId == userId && c.GameId == gameId);
            if (gameInCart != null)
            {
                this.db.UserCartItems.Remove(gameInCart);
                this.db.SaveChanges();

                var a = this.HttpContext.Session.GetString("cart");
                this.HttpContext.Session.SetString("cart", this.db.UserCartItems.Where(c => c.UserId == userId).Count().ToString());

                // Session["cart"] = this.db.UserCartItems.Where(c => c.UserId == userId).Count().ToString();
                return this.Json(new { Success = true });
            }
            else
            {
                return this.Json(new { Success = false, Error = "Error occurred while removing game from your cart" });
            }
        }

        public ActionResult ClearAll()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var gamesInCart = this.db.UserCartItems.Where(c => c.UserId == userId);
            foreach (var game in gamesInCart)
            {
                this.db.UserCartItems.Remove(game);
            }

            this.db.SaveChanges();
            var a = this.HttpContext.Session.GetString("cart");
            this.HttpContext.Session.SetString("cart", this.db.UserCartItems.Where(c => c.UserId == userId).Count().ToString());
            return this.RedirectToAction("Index", "Cart");
        }

        private List<GameInCartViewModel> GetGamesFromCart(string userId)
        {
            var gamesInCartDB = this.db.UserCartItems.Where(c => c.UserId == userId).ToList();
            var gamesInDB = this.db.Games;

            var games = new List<GameInCartViewModel>();

            foreach (var game in gamesInCartDB)
            {
                var dbGame = gamesInDB.FirstOrDefault(g => g.Id == game.GameId);
                if (dbGame != null)
                {
                    var gameToAdd = this.gamesService.GetById<GameInCartViewModel>(dbGame.Id);

                    games.Add(gameToAdd);
                }
            }

            return games;
        }

        /*[HttpPost]
        public JsonResult MoveToWishList(string gameId)
        {
            var userId = User.Identity.GetUserId();

            var gameInCart = db.UserCartItems.FirstOrDefault(g => g.UserId == userId && g.GameId == gameId);
            if (gameInCart != null)
            {
                var gameInWishList = db.WishLists.FirstOrDefault(g => g.userId == userId && g.gameId == gameId);
                if (gameInWishList != null)
                {
                    return Json(new { Success = false, Error = "This game is already in your wish list" }, JsonRequestBehavior.AllowGet);
                }

                db.WishLists.Add(new WishList { userId = userId, gameId = gameInCart.GameId });
                db.UserCartItems.Remove(gameInCart);
                db.SaveChanges();

                Session["cart"] = db.UserCartItems.Where(c => c.UserId == userId).Count().ToString();
                return Json(new { Success = true }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Success = false, Error = "Error occurred while moving game to your wish list" }, JsonRequestBehavior.AllowGet);
            }
        }*/
    }
}
