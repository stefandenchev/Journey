namespace Journey.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

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
        private readonly IGamesService gamesService;
        private readonly IOrdersService ordersService;
        private readonly IOrderItemsService orderItemsService;
        private readonly ICreditCardsService creditCardsService;

        public ProfileController(
            IGamesService gamesService,
            IOrdersService ordersService,
            IOrderItemsService orderItemsService,
            ICreditCardsService creditCardsService)
        {
            this.gamesService = gamesService;
            this.ordersService = ordersService;
            this.orderItemsService = orderItemsService;
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
               // return this.RedirectToPage("/NotFound", new { Area = "Home", Controller = "Home" });
            }

            return this.RedirectToAction("Payment", "Profile");
        }

        public IActionResult Orders(string sortOrder)
        {
            this.ViewBag.DateSortParam = string.IsNullOrEmpty(sortOrder) ? "date_asc" : string.Empty;
            this.ViewBag.PriceSortParam = sortOrder == "price_asc" ? "price_desc" : "price_asc";

            var userId = this.User.GetId();

            OrdersListViewModel viewModel = new()
            {
                Orders = this.ordersService.GetOrders<ProfileOrderViewModel>(userId),
            };

            foreach (var order in viewModel.Orders)
            {
                var gameIds = this.orderItemsService.GetGameIdsFromOrder(order.Id);
                var orderGames = this.gamesService.GetGamesFromOrder<GameThumbViewModel>(gameIds);

                var total = orderGames.Sum(g => g.PriceOnPurchase);
                order.Games = orderGames;

                viewModel.Total = viewModel.Orders.Sum(x => x.Total);
            }

            if (sortOrder == "date_asc")
            {
                viewModel.Orders = viewModel.Orders
                .OrderBy(x => x.PurchaseDate).ToList();
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

            var orderIds = this.ordersService.GetOrderIds(userId);
            List<int> gameIds = new();

            foreach (var orderId in orderIds)
            {
                gameIds.AddRange(this.orderItemsService.GetGameIdsFromOrder(orderId));
            }

            var games = this.gamesService.GetGamesFromOrder<GameInLibraryViewModel>(gameIds).OrderBy(x => x.Title);
            var viewModel = new GameLibraryViewModel { Collection = games, };

            if (sortOrder == "title_desc")
            {
                viewModel.Collection = viewModel.Collection
                .OrderByDescending(x => x.Title).ToList();
            }

            return this.View(viewModel);
        }
    }
}
