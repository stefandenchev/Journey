namespace Journey.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Journey.Data;
    using Journey.Data.Models;
    using Journey.Services.Data.Helpers;
    using Journey.Services.Data.Interfaces;
    using Journey.Web.ViewModels.Cart;
    using Journey.Web.ViewModels.Games;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Routing;
    using Microsoft.AspNetCore.Session;

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

            // add games
            List<GameInCartViewModel> games = this.GetGamesFromCart(userId);
            viewModel.GamesInCart = games;

            // calculate total
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

        public ActionResult RemoveItem(int gameId)
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




        /* public IActionResult Index()
         {
             *//*           var cart = SessionHelper.GetObjectFromJson<List<Item>>(this.HttpContext.Session, "cart");
                        this.ViewBag.cart = cart;
                        this.ViewBag.total = cart.Sum(item => item.Game.Price);

                        return this.View();*//*

             var viewModel = new CartViewModel
             {
                 UserId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value,
                 Games = new List<GameInCartViewModel>(),
             };

             return this.View(viewModel);
         }

         private int Exists(int id)
         {
             List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(this.HttpContext.Session, "cart");
             for (int i = 0; i < cart.Count; i++)
             {
                 if (cart[i].Game.Id.Equals(id))
                 {
                     return i;
                 }
             }

             return -1;
         }

         public IActionResult Buy(int id)
         {
             *//*            AllGamesModel gamesListModel = new AllGamesModel
                         {
                             Games = this.gamesService.GetAll(),
                         };

                         if (SessionHelper.GetObjectFromJson<List<Item>>(this.HttpContext.Session, "cart") == null)
                         {
                             List<Item> cart = new List<Item>();
                             cart.Add(new Item { Game = gamesListModel.Games.FirstOrDefault(x => x.Id == id) });
                             SessionHelper.SetObjectAsJson(this.HttpContext.Session, "cart", cart);
                         }
                         else
                         {
                             List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(this.HttpContext.Session, "cart");
                             int index = this.Exists(id);
                             cart.Add(new Item { Game = gamesListModel.Games.FirstOrDefault(x => x.Id == id) });

                             SessionHelper.SetObjectAsJson(this.HttpContext.Session, "cart", cart);
                         }*//*

             AllGamesModel gamesListModel = new AllGamesModel
             {
                 Games = this.gamesService.GetAll(),
             };

             var viewModel = new CartViewModel
             {
                 UserId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value,
                 Games = this.gamesService.GetAllToModel<GameInCartViewModel>(),
             };

             Game game = gamesListModel.Games.FirstOrDefault(x => x.Id == id);
             GameInCartViewModel gameToAdd = new GameInCartViewModel
             {
                 Id = id,
                 Title = game.Title,
                 Price = game.Price,
             };

             viewModel.Games.Add(gameToAdd);

             return this.RedirectToAction("Index");
         }

         public IActionResult Remove(int id)
         {
             List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(this.HttpContext.Session, "cart");
             int index = this.Exists(id);
             cart.RemoveAt(index);

             SessionHelper.SetObjectAsJson(this.HttpContext.Session, "cart", cart);

             return this.RedirectToAction("Index");
         }
     }*/
    }
}