namespace Journey.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;

    using Journey.Data;
    using Journey.Data.Models;
    using Journey.Services.Data.Interfaces;
    using Journey.Web.ViewModels;
    using Journey.Web.ViewModels.Profile;
    using Microsoft.AspNetCore.Mvc;

    public class ProfileController : BaseController
    {
        private readonly ApplicationDbContext db;
        private readonly IGamesService gamesService;
        private readonly IOrdersService ordersService;

        public ProfileController(
            ApplicationDbContext db,
            IGamesService gamesService,
            IOrdersService ordersService)
        {
            this.db = db;
            this.gamesService = gamesService;
            this.ordersService = ordersService;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        public IActionResult Payment()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var creditCards = new List<CreditCard>();

            creditCards = this.db.CreditCards.Where(c => c.UserId == userId).ToList();

            var model = new PaymentViewModel
            {
                CreditCards = creditCards ?? new List<CreditCard>(),
            };

            return this.View(model);
        }

        [HttpGet]
        public JsonResult GetCreditCards()
        {
            string currentUserId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            List<CreditCard> creditCards = new List<CreditCard>();

            creditCards = this.db.CreditCards.Where(c => c.UserId == currentUserId).ToList();

            return this.Json(creditCards);
        }

        [HttpPost]
        public ActionResult AddCreditCard([FromBody] CreditCard creditCard)
        {
            creditCard.UserId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            try
            {
                string cardNumberFormatted = creditCard.CardNumber.Replace(" ", string.Empty);
                cardNumberFormatted = cardNumberFormatted.Length == 13 ? string.Format("{0:0000 0000 0000 0}", long.Parse(cardNumberFormatted)) :
                    cardNumberFormatted.Length == 15 ? string.Format("{0:0000 0000 0000 000}", long.Parse(cardNumberFormatted)) :
                        string.Format("{0:0000 0000 0000 0000}", long.Parse(cardNumberFormatted));
                creditCard.CardNumber = cardNumberFormatted;

                this.db.CreditCards.Add(creditCard);
                this.db.SaveChanges();
                return this.Json(new { Success = true });
            }
            catch
            {
                return this.Json(new { Success = false, Error = "Error occurred while saving card information" });
            }
        }

        public ActionResult DeleteCreditCard(int? id)
        {
            if (id == null)
            {
                return this.Json(new { Success = false, Error = "Information not received" });
            }

            try
            {
                var creditCard = this.db.CreditCards.FirstOrDefault(c => c.Id == id);

                if (creditCard == null)
                {
                    return this.Json(new { Success = false, Error = "Credit Card not found" });
                }

                this.db.CreditCards.Remove(creditCard);
                this.db.SaveChanges();

                return this.RedirectToAction("Payment", "Profile");
            }
            catch
            {
                return this.Json(new { Success = false, Error = "Error occurred while deleting credit card record" });
            }
        }

        public IActionResult Orders(string sortOrder)
        {
            this.ViewBag.DateSortParam = string.IsNullOrEmpty(sortOrder) ? "date_asc" : string.Empty;
            this.ViewBag.PriceSortParam = sortOrder == "price_asc" ? "price_desc" : "price_asc";

            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var orders = this.db.Orders.Where(o => o.UserId == userId).OrderByDescending(x => x.PurchaseDate);
            var orderItems = this.db.OrderItems;
            var games = this.db.Games;

            OrdersListViewModel viewModel = new OrdersListViewModel();
            viewModel.Orders = new List<OrdersViewModel>();

            foreach (var order in orders)
            {
                var gameIds = this.db.OrderItems.Where(x => x.OrderId == order.Id).Select(x => x.GameId).ToList();

                var orderGames = this.gamesService.GetAll<GameInListViewModel>().Where(g => gameIds.Contains(g.Id));
                var total = orderGames.Sum(g => g.Price);
                List<GameInListViewModel> gameThumbs = orderGames.Select(g => new GameInListViewModel { Id = g.Id, ImageUrl = g.ImageUrl }).ToList();

                viewModel.Orders.Add(new OrdersViewModel
                {
                    Id = order.Id,
                    OrderPlaced = order.PurchaseDate,
                    ItemNumber = orderItems.Where(oi => oi.OrderId == order.Id).Count(),
                    Total = total,
                    Games = gameThumbs,
                });
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

        public ActionResult Library(string sortOrder)
        {
            this.ViewBag.TitleSortParam = string.IsNullOrEmpty(sortOrder) ? "title_desc" : string.Empty;

            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

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

            var games = new GameLibraryViewModel();
            games.Collection = new List<GameInLibraryViewModel>();

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
