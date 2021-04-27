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

        public IActionResult All(int id)
        {
            var viewModel = new GamesListViewModel
            {
                PageNumber = id,
                Games = this.gamesService.GetAll(id, 12),
            };

            return this.View(viewModel);
        }
    }
}
