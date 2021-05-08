namespace Journey.Web.Controllers
{
    using Journey.Services.Data;
    using Journey.Web.ViewModels;
    using Journey.Web.ViewModels.Games;
    using Journey.Web.ViewModels.Games.Create;
    using Microsoft.AspNetCore.Mvc;

    public class GamesController : Controller
    {
        private readonly IGamesService gamesService;
        private readonly IGenresService genresService;
        private readonly ILanguagesService languagesService;
        private readonly ITagsService tagsService;

        public GamesController(
            IGamesService gamesService,
            IGenresService genresService,
            ILanguagesService languagesService,
            ITagsService tagsService)
        {
            this.gamesService = gamesService;
            this.genresService = genresService;
            this.languagesService = languagesService;
            this.tagsService = tagsService;
        }

        public IActionResult Create()
        {
            var viewModel = new CreateGameInputModel();
            viewModel.GenresItems = this.genresService.GetAllAsKeyValuePairs();
            viewModel.LanguagesItems = this.languagesService.GetAllAsKeyValuePairs();
            viewModel.TagsItems = this.tagsService.GetAllAsKeyValuePairs();

            return this.View(viewModel);
        }

        [HttpPost]
        public IActionResult Create(CreateGameInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.GenresItems = this.genresService.GetAllAsKeyValuePairs();
                return this.View(input);
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
