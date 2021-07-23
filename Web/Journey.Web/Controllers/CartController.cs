namespace Journey.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Journey.Data;
    using Journey.Data.Models;
    using Journey.Services.Data.Interfaces;
    using Journey.Web.Infrastructure;
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
        private readonly ICreditCardsService creditCardsService;
        private readonly ICartService cartService;

        public CartController(
            ApplicationDbContext db,
            IGamesService gamesService,
            IOrdersService ordersService,
            ICreditCardsService creditCardsService,
            ICartService cartService)
        {
            this.db = db;
            this.gamesService = gamesService;
            this.ordersService = ordersService;
            this.creditCardsService = creditCardsService;
            this.cartService = cartService;
        }

        public IActionResult Index()
        {
            var userId = this.User.GetId();

            var viewModel = new CartViewModel();

            List<GameInCartViewModel> games = this.GetGamesFromCart(userId);
            viewModel.GamesInCart = games;

            viewModel.Total = games.Sum(g => g.CurrentPrice);

            return this.View(viewModel);
        }

        public async Task<ActionResult> Add(int id)
        {
            var userId = this.User.GetId();

            if (this.cartService.IsInCart(userId, id))
            {
                return this.RedirectToAction("Index", "Cart");
            }

            await this.cartService.CreateAsync(userId, id);
            return this.RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Remove(int gameId)
        {
            var userId = this.User.GetId();

            var gameInCart = this.cartService.Get<CartItemInputModel>(userId, gameId);

            if (gameInCart != null)
            {
                this.cartService.RemoveAsync(userId, gameId);

                return this.Json(new { Success = true });
            }
            else
            {
                return this.Json(new { Success = false, Error = "Error occurred while removing game from your cart" });
            }
        }

        public IActionResult ClearAll()
        {
            var userId = this.User.GetId();
            this.cartService.ClearAllAsync(userId);
            return this.RedirectToAction("Index", "Cart");
        }

        [HttpGet]
        public IActionResult Checkout()
        {
            var userId = this.User.GetId();

            var viewModel = new CheckoutViewModel();

            List<GameInCartViewModel> games = this.GetGamesFromCart(userId);
            viewModel.GamesInCart = games;

            viewModel.CreditCards = this.creditCardsService.GetAll<CreditCardViewModel>().Where(c => c.UserId == userId).ToList();

            viewModel.Total = games.Sum(g => g.CurrentPrice);

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Checkout(CheckoutViewModel model)
        {
            var userId = this.User.GetId();

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
                    Total = model.GamesInCart.Sum(x => x.CurrentPrice),
                };

                this.db.Orders.Add(newOrder);

                foreach (var game in model.GamesInCart)
                {
                    this.db.OrderItems.Add(new OrderItem { OrderId = orderId, GameId = game.Id, GameKey = RandomKeyGen(), PriceOnPurchase = game.CurrentPrice });
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

                // remove all items from cart
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
                this.ModelState.AddModelError(string.Empty, "An unexpected error occured while attempting to place your order.");
                return this.View(model);
            }

            return this.RedirectToAction("OrderComplete", "Cart");
        }

        [HttpGet]
        public IActionResult OrderComplete()
        {
            var userId = this.User.GetId();

            var model = new CheckoutViewModel();

            Order latestOrder = this.db.Orders.Where(o => o.UserId == userId).OrderByDescending(o => o.PurchaseDate).FirstOrDefault();
            model.Id = latestOrder.Id;

            string cardNumber = this.db.CreditCards.Where(cc => cc.Id == latestOrder.CreditCardId).FirstOrDefault().CardNumber;

            if (cardNumber != null)
            {
                model.CreditCardLast4 = cardNumber.Substring(cardNumber.Length - 5);
            }

            model.GamesInCart = this.GetGamesFromLastOrder(userId, latestOrder);

            model.Total = model.GamesInCart.Sum(g => g.PriceOnPurchase);

            return this.View(model);
        }

        [HttpGet]
        public IActionResult ViewOrder(string id)
        {
            var userId = this.User.GetId();
            var model = new CheckoutViewModel();

            var order = this.db.Orders.FirstOrDefault(o => o.Id == id);
            if (order == null)
            {
                return this.RedirectToAction("Orders", "Home");
            }

            model.Id = order.Id;

            var card = this.creditCardsService.GetById(order.CreditCardId);
            var cardNumber = card.CardNumber;

            if (cardNumber != null)
            {
                model.CreditCardLast4 = cardNumber.Substring(cardNumber.Length - 5);
            }

            model.GamesInCart = this.GetGamesFromLastOrder(userId, order);

            model.Total = model.GamesInCart.Sum(g => g.PriceOnPurchase);

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

        private static string RandomKeyGen()
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
            List<int> gameIds = new();

            foreach (var oi in orderItems)
            {
                gameIds.Add(oi.GameId);
            }

            var gamesToReturn = this.gamesService.GetAll<GameInCartViewModel>().Where(g => gameIds.Contains(g.Id)).ToList();
            foreach (var game in gamesToReturn)
            {
                game.GameKey = orderItems.FirstOrDefault(x => x.GameId == game.Id).GameKey;
                game.PriceOnPurchase = orderItems.FirstOrDefault(x => x.GameId == game.Id).PriceOnPurchase;
            }

            return gamesToReturn;
        }
    }
}
