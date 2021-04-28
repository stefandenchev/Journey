namespace Journey.Web.Controllers
{
    using System.Linq;

    using Journey.Data;
    using Journey.Services.Data;
    using Journey.Web.ViewModels;
    using Microsoft.AspNetCore.Mvc;

    public class GamesController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IGamesService gamesService;

        public GamesController(
            ApplicationDbContext dbContext,
            IGamesService gamesService)
        {
            this.dbContext = dbContext;
            this.gamesService = gamesService;
        }

        public IActionResult All(int id = 1)
        {
            if (id <= 0)
            {
                return this.NotFound();
            }

            const int itemsPerPage = 12;
            var viewModel = new GamesListViewModel
            {
                ItemsPerPage = itemsPerPage,
                PageNumber = id,
                GamesCount = this.gamesService.GetCount(),
                Games = this.gamesService.GetAll<GameInListViewModel>(id, 12),
            };

            return this.View(viewModel);
        }
    }
}
