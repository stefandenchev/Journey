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
    using Journey.Web.ViewModels.Profile;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;

    [Authorize]
    public class CartController : BaseController
    {
        private readonly IGamesService gamesService;
        private readonly IOrdersService ordersService;
        private readonly ICreditCardsService creditCardsService;
        private readonly ICartService cartService;
        private readonly IWishlistService wishlistService;

        public CartController(
            IGamesService gamesService,
            IOrdersService ordersService,
            ICreditCardsService creditCardsService,
            ICartService cartService,
            IWishlistService wishlistService)
        {
            this.gamesService = gamesService;
            this.ordersService = ordersService;
            this.creditCardsService = creditCardsService;
            this.cartService = cartService;
            this.wishlistService = wishlistService;
        }

        public IActionResult Index()
        {
            var userId = this.User.GetId();

            var viewModel = new CartViewModel();
            viewModel.GamesInCart = this.cartService.GetAllInCart<GameInCartViewModel>(userId);

            viewModel.Total = viewModel.GamesInCart.Sum(g => g.CurrentPrice);

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

            var gameInCart = this.cartService.Get<CartItemViewModel>(userId, gameId);

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

        public async Task<ActionResult> ClearAll()
        {
            var userId = this.User.GetId();
            await this.cartService.ClearAllAsync(userId);
            return this.RedirectToAction("Index", "Cart");
        }

        [HttpGet]
        public IActionResult Checkout()
        {
            var userId = this.User.GetId();

            var viewModel = new CheckoutViewModel();

            viewModel.GamesInCart = this.cartService.GetAllInCart<GameInCartViewModel>(userId);
            viewModel.CreditCards = this.creditCardsService.GetAll<CreditCardViewModel>().Where(c => c.UserId == userId).ToList();
            viewModel.Total = viewModel.GamesInCart.Sum(g => g.CurrentPrice);

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Checkout(CheckoutViewModel model)
        {
            var userId = this.User.GetId();

            try
            {
                string orderId = Guid.NewGuid().ToString();
                model.GamesInCart = this.cartService.GetAllInCart<GameInCartViewModel>(userId);

                await this.ordersService.CreateAsync(orderId, userId, model.CreditCardId, model.GamesInCart.Sum(x => x.CurrentPrice));
                await this.ordersService.CreateOrderItems(model.GamesInCart, orderId);

                // remove all items from cart
                await this.cartService.ClearAllAsync(userId);

                List<int> gameIds = new();

                foreach (var g in model.GamesInCart)
                {
                    gameIds.Add(g.Id);
                }

                // remove the games from wishlist
                await this.wishlistService.ClearBoughtGamesFromWishlist(userId, gameIds);
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
            var latestOrder = this.ordersService.GetLatest<OrderCompleteViewModel>(userId);
            var latestCardNumer = this.creditCardsService.GetLatestCardNumber(latestOrder.CreditCardId);

            latestOrder.CreditCardLast4 = latestCardNumer.Substring(latestCardNumer.Length - 5);
            latestOrder.GamesInCart = this.GetGamesFromOrder(latestOrder);

            latestOrder.Total = latestOrder.GamesInCart.Sum(g => g.PriceOnPurchase);

            return this.View(latestOrder);
        }

        [HttpGet]
        public IActionResult ViewOrder(string id)
        {
            var userId = this.User.GetId();
            var order = this.ordersService.GetById<OrderCompleteViewModel>(id);
            if (order == null)
            {
                return this.RedirectToAction("Orders", "Home");
            }

            var card = this.creditCardsService.GetById(order.CreditCardId);
            order.CreditCardLast4 = card.CardNumber.Substring(card.CardNumber.Length - 5);
            order.GamesInCart = this.GetGamesFromOrder(order);
            order.Total = order.GamesInCart.Sum(g => g.PriceOnPurchase);

            return this.View("OrderComplete", order);
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

        private IEnumerable<GameInCartViewModel> GetGamesFromOrder(OrderCompleteViewModel order)
        {
            var orderItems = this.ordersService.GetOrderItems<OrderItemViewModel>(order.Id);
            var gameIds = this.ordersService.GetGameIdsFromOrder(order.Id);
            var gamesToReturn = this.gamesService.GetGamesFromOrder<GameInCartViewModel>(gameIds);

            foreach (var game in gamesToReturn)
            {
                game.GameKey = orderItems.FirstOrDefault(x => x.GameId == game.Id).GameKey;
                game.PriceOnPurchase = orderItems.FirstOrDefault(x => x.GameId == game.Id).PriceOnPurchase;
            }

            return gamesToReturn;
        }
    }
}
