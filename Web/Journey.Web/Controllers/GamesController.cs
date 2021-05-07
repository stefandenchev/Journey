namespace Journey.Web.Controllers
{
    using Journey.Services.Data;
    using Journey.Web.ViewModels;
    using Journey.Web.ViewModels.Games;
    using Microsoft.AspNetCore.Mvc;

    public class GamesController : Controller
    {
        private readonly IGamesService gamesService;

        public GamesController(
            IGamesService gamesService)
        {
            this.gamesService = gamesService;
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Create(CreateGameInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            // Create recipe using service method
            // Redirect to recipe info page
            return this.Redirect("/");
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

        public IActionResult ById(int id)
        {
            var game = this.gamesService.GetById<SingleGameViewModel>(id);

            return this.View(game);
        }
    }
}
