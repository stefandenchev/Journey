namespace Journey.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Journey.Data;
    using Journey.Data.Models;
    using Journey.Services.Data.Interfaces;
    using Journey.Web.Infrastructure;
    using Journey.Web.ViewModels.Cart;
    using Journey.Web.ViewModels.Games;
    using Journey.Web.ViewModels.Profile;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class ProfileController : BaseController
    {
        private readonly ApplicationDbContext db;
        private readonly IGamesService gamesService;
        private readonly IOrdersService ordersService;
        private readonly ICreditCardsService creditCardsService;

        public ProfileController(
            ApplicationDbContext db,
            IGamesService gamesService,
            IOrdersService ordersService,
            ICreditCardsService creditCardsService)
        {
            this.db = db;
            this.gamesService = gamesService;
            this.ordersService = ordersService;
            this.creditCardsService = creditCardsService;
        }

        public IActionResult Payment()
        {
            var userId = this.User.GetId();

            var creditCards = this.creditCardsService.GetAll<CreditCardViewModel>().Where(c => c.UserId == userId);

            var model = new PaymentViewModel
            {
                CreditCards = creditCards ?? new List<CreditCardViewModel>(),
            };

            return this.View(model);
        }

        [HttpPost]
        public IActionResult AddCreditCard([FromBody] CreateCardInputModel creditCard)
        {
            creditCard.UserId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            try
            {
                string cardNumberFormatted = creditCard.CardNumber.Replace(" ", string.Empty);
                cardNumberFormatted = string.Format("{0:0000 0000 0000 0000}", long.Parse(cardNumberFormatted));

                if (this.creditCardsService.GetAll<CreditCardViewModel>().Any(x => x.CardNumber == cardNumberFormatted))
                {
                    return this.RedirectToAction("Payment");
                }

                creditCard.CardNumber = cardNumberFormatted;

                this.creditCardsService.CreateAsync(creditCard);

                return this.Json(new { Success = true });
            }
            catch
            {
                return this.Json(new { Success = false, Error = "Error occurred while saving card information" });
            }
        }

        public async Task<ActionResult> DeleteCreditCard(int id)
        {
            var card = this.creditCardsService.GetByIdToModel<CreditCardViewModel>(id);
            if (card != null)
            {
                await this.creditCardsService.RemoveById(id);
            }
            else
            {
                return this.RedirectToPage("/NotFound", new { Area = "Home", Controller = "Home" });
            }

            return this.RedirectToAction("Payment", "Profile");
        }

        public IActionResult Orders(string sortOrder)
        {
            this.ViewBag.DateSortParam = string.IsNullOrEmpty(sortOrder) ? "date_asc" : string.Empty;
            this.ViewBag.PriceSortParam = sortOrder == "price_asc" ? "price_desc" : "price_asc";

            var userId = this.User.GetId();

            var orders = this.db.Orders.Where(o => o.UserId == userId).OrderByDescending(x => x.PurchaseDate);
            var orderItems = this.db.OrderItems;
            var games = this.db.Games;

            OrdersListViewModel viewModel = new();
            viewModel.Orders = new List<ProfileOrderViewModel>();

            foreach (var order in orders)
            {
                var gameIds = this.db.OrderItems.Where(x => x.OrderId == order.Id).Select(x => x.GameId).ToList();

                var orderGames = this.gamesService.GetAll<GameInCartViewModel>().Where(g => gameIds.Contains(g.Id));
                var total = orderGames.Sum(g => g.PriceOnPurchase);

                var currentOrderItems = this.ordersService.GetAllOrderItems<OrderItemViewModel>().Where(g => gameIds.Contains(g.GameId));
                var currentTotal = currentOrderItems.Sum(g => g.PriceOnPurchase);

                List<GameThumbViewModel> gameThumbs = orderGames.Select(g => new GameThumbViewModel { Id = g.Id, ImageUrl = g.ImageUrl }).ToList();

                viewModel.Orders.Add(new ViewModels.Profile.ProfileOrderViewModel
                {
                    Id = order.Id,
                    OrderPlaced = order.PurchaseDate,
                    ItemNumber = orderItems.Where(oi => oi.OrderId == order.Id).Count(),
                    Total = currentTotal,
                    Games = gameThumbs,
                });

                viewModel.Total = viewModel.Orders.Sum(x => x.Total);
            }

            if (sortOrder == "date_asc")
            {
                viewModel.Orders = viewModel.Orders
                .OrderBy(x => x.OrderPlaced).ToList();
            }

            if (sortOrder == "price_desc")
            {
                viewModel.Orders = viewModel.Orders
                .OrderByDescending(x => x.Total).ToList();
            }

            if (sortOrder == "price_asc")
            {
                viewModel.Orders = viewModel.Orders
                .OrderBy(x => x.Total).ToList();
            }

            return this.View(viewModel);
        }

        public IActionResult Library(string sortOrder)
        {
            this.ViewBag.TitleSortParam = string.IsNullOrEmpty(sortOrder) ? "title_desc" : string.Empty;

            var userId = this.User.GetId();

            var viewModel = this.GetPurchasedGames(userId);

            viewModel.Collection = viewModel.Collection
                .OrderBy(x => x.Title).ToList();

            if (sortOrder == "title_desc")
            {
                viewModel.Collection = viewModel.Collection
                .OrderByDescending(x => x.Title).ToList();
            }

            return this.View(viewModel);
        }

        private GameLibraryViewModel GetPurchasedGames(string userId)
        {
            var orderIds = this.db.Orders.Where(o => o.UserId == userId).Select(o => o.Id).ToList();
            var gameIds = this.db.OrderItems.Where(oi => orderIds.Contains(oi.OrderId)).Select(oi => oi.GameId).ToList();

            var games = new GameLibraryViewModel
            {
                Collection = new List<GameInLibraryViewModel>(),
            };

            List<Game> purchasedGames = this.db.Games.Where(g => gameIds.Contains(g.Id)).ToList();
            foreach (var purchasedGame in purchasedGames)
            {
                var gameToAdd = this.gamesService.GetById<GameInLibraryViewModel>(purchasedGame.Id);

                games.Collection.Add(gameToAdd);
            }

            return games;
        }
    }
}
