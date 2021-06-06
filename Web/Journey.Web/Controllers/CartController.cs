namespace Journey.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;

    using Journey.Data;
    using Journey.Data.Models;
    using Journey.Services.Data.Interfaces;
    using Journey.Web.ViewModels.Cart;
    using Journey.Web.ViewModels.Export;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;

    [Authorize]
    public class CartController : BaseController
    {
        private readonly ApplicationDbContext db;
        private readonly IGamesService gamesService;
        private readonly IOrdersService ordersService;

        public CartController(
            ApplicationDbContext db,
            IGamesService gamesService,
            IOrdersService ordersService)
        {
            this.db = db;
            this.gamesService = gamesService;
            this.ordersService = ordersService;
        }

        public ActionResult Index()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

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
                return this.RedirectToAction("Index", "Cart");
            }

            var newCartItem = new UserCartItem() { UserId = userId, GameId = id };
            this.db.UserCartItems.Add(newCartItem);
            await this.db.SaveChangesAsync();

            var a = this.HttpContext.Session.GetString("cart");
            this.HttpContext.Session.SetString("cart", this.db.UserCartItems.Where(c => c.UserId == userId).Count().ToString());

            return this.RedirectToAction("Index");
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

        [HttpGet]
        public ActionResult Checkout()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var viewModel = new CheckoutViewModel();

            // get games
            List<GameInCartViewModel> games = this.GetGamesFromCart(userId);
            viewModel.GamesInCart = games;

            // get credit cards
            // viewModel.CreditCards = this.creditCardsService.GetAll<CreditCardViewModel>().Where(c => c.UserId == userId).ToList();
            viewModel.CreditCards = this.db.CreditCards.Where(c => c.UserId == userId).ToList();

            // calculate total
            viewModel.Total = games.Sum(g => g.Price);

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Checkout(CheckoutViewModel model)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            try
            {
                string orderId = Guid.NewGuid().ToString();
                model.GamesInCart = this.GetGamesFromCart(userId);

                var newOrder = new Order
                {
                    Id = orderId,
                    UserId = userId,
                    PurchaseDate = DateTime.Now,
                    CreditCardId = model.PaymentMethodId,
                    Total = model.GamesInCart.Sum(x => x.Price),
                };

                this.db.Orders.Add(newOrder);

                // add new orderItem records
                foreach (var game in model.GamesInCart)
                {
                    this.db.OrderItems.Add(new OrderItem { OrderId = orderId, GameId = game.Id, GameKey = this.RandomKeyGen() });
                }

                // if any of the games bought were in the user's wish list, remove them from there
                var userWishList = this.db.Wishlists.Where(i => i.UserId == userId);
                foreach (var game in model.GamesInCart)
                {
                    var gameInWishList = userWishList.FirstOrDefault(i => i.GameId == game.Id);
                    if (gameInWishList != null)
                    {
                        this.db.Wishlists.Remove(gameInWishList);
                    }
                }

                // remove all items from user's cart
                var cartItem = this.db.UserCartItems.Where(c => c.UserId == userId).ToList();
                this.db.UserCartItems.RemoveRange(cartItem);

                List<int> gameIds = new();

                foreach (var g in model.GamesInCart)
                {
                    gameIds.Add(g.Id);
                }

                // remove the games from wishlist
                var wishes = this.db.Wishlists.Where(c => c.UserId == userId && gameIds.Contains(c.GameId));
                this.db.Wishlists.RemoveRange(wishes);

                await this.db.SaveChangesAsync();
            }
            catch (Exception)
            {
                this.ModelState.AddModelError("", "An unexpected error occured while attempting to place your order.");
                return this.View(model);
            }

            return this.RedirectToAction("OrderComplete", "Cart");
        }

        [HttpGet]
        public ActionResult OrderComplete()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var model = new CheckoutViewModel();

            Order latestOrder = this.db.Orders.Where(o => o.UserId == userId).OrderByDescending(o => o.PurchaseDate).FirstOrDefault();
            model.Id = latestOrder.Id;

            string cardNumber = this.db.CreditCards.Where(cc => cc.Id == latestOrder.CreditCardId).FirstOrDefault().CardNumber;

            if (cardNumber != null)
            {
                model.CreditCardLast4 = cardNumber.Substring(cardNumber.Length - 5);
            }

            model.GamesInCart = this.GetGamesFromLastOrder(userId, latestOrder);

            model.Total = model.GamesInCart.Sum(g => g.Price);

            return this.View(model);
        }

        [HttpGet]
        public ActionResult ViewOrder(string id)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var model = new CheckoutViewModel();

            // get lastest order
            var order = this.db.Orders.FirstOrDefault(o => o.Id == id);
            if (order == null)
            {
                return this.RedirectToAction("Orders", "Home");
            }

            model.Id = order.Id;

            string cardNumber = this.db.CreditCards.Where(cc => cc.Id == order.CreditCardId).FirstOrDefault().CardNumber;

            if (cardNumber != null)
            {
                model.CreditCardLast4 = cardNumber.Substring(cardNumber.Length - 5);
            }

            model.GamesInCart = this.GetGamesFromLastOrder(userId, order);

            model.Total = model.GamesInCart.Sum(g => g.Price);

            return this.View("OrderComplete", model);
        }

        public IActionResult ExportToJson(string id)
        {
            var order = this.ordersService.GetById<OrderJsonExportModel>(id);
            order.OrderItems = this.ordersService.GetAllOrderItems<OrderItemJsonExportModel>()
                .Where(x => x.OrderId == order.Id);

            string jsonResult = JsonConvert.SerializeObject(order, Formatting.Indented);

            StringBuilder sb = new();
            sb.AppendLine($"Receipt for Order: {order.Id}").AppendLine(jsonResult);

            var fileName = $"Receipt-{order.Id}.txt";
            var type = "text/plain";
            var fileBytes = Encoding.ASCII.GetBytes(sb.ToString());
            return new FileContentResult(fileBytes, type)
            {
                FileDownloadName = fileName,
            };
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

        private List<GameInCartViewModel> GetGamesFromLastOrder(string userId, Order lastestOrder)
        {
            List<OrderItem> orderItems = this.db.OrderItems.Where(oi => oi.OrderId == lastestOrder.Id).ToList();
            List<int> gameIds = new List<int>();

            foreach (var oi in orderItems)
            {
                gameIds.Add(oi.GameId);
            }

            var gamesToReturn = this.gamesService.GetAll<GameInCartViewModel>().Where(g => gameIds.Contains(g.Id)).ToList();
            foreach (var game in gamesToReturn)
            {
                game.GameKey = orderItems.FirstOrDefault(x => x.GameId == game.Id).GameKey;
            }

            return gamesToReturn;
        }

        private string RandomKeyGen()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var stringChars = new List<char>();
            var random = new Random();

            for (int i = 0; i < 16; i++)
            {
                if (i % 4 == 0 && i != 0)
                {
                    stringChars.Add('-');
                }

                stringChars.Add(chars[random.Next(chars.Length)]);
            }

            var finalString = new string(stringChars.ToArray());
            return finalString;
        }
    }
}
