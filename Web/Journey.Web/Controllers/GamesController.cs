namespace Journey.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Journey.Common;
    using Journey.Data;
    using Journey.Services.Data.Interfaces;
    using Journey.Web.ViewModels;
    using Journey.Web.ViewModels.Games;
    using Journey.Web.ViewModels.Games.Create;
    using Journey.Web.ViewModels.Games.Edit;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;

    public class GamesController : BaseController
    {
        private readonly IGamesService gamesService;
        private readonly IGenresService genresService;
        private readonly ILanguagesService languagesService;
        private readonly ITagsService tagsService;
        private readonly IPublishersService publishersService;
        private readonly IWebHostEnvironment environment;
        private readonly ApplicationDbContext db;

        public GamesController(
            IGamesService gamesService,
            IGenresService genresService,
            ILanguagesService languagesService,
            ITagsService tagsService,
            IPublishersService publishersService,
            IWebHostEnvironment environment,
            ApplicationDbContext db)
        {
            this.gamesService = gamesService;
            this.genresService = genresService;
            this.languagesService = languagesService;
            this.tagsService = tagsService;
            this.publishersService = publishersService;
            this.environment = environment;
            this.db = db;
        }

        [Authorize]
        public IActionResult Create()
        {
            var viewModel = new CreateGameInputModel();
            viewModel.GenresItems = this.genresService.GetAllAsKeyValuePairs();
            viewModel.LanguagesItems = this.languagesService.GetAllAsKeyValuePairs();
            viewModel.TagsItems = this.tagsService.GetAllAsKeyValuePairs();
            viewModel.PublisherItems = this.publishersService.GetAllAsKeyValuePairs();

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateGameInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.GenresItems = this.genresService.GetAllAsKeyValuePairs();
                input.LanguagesItems = this.languagesService.GetAllAsKeyValuePairs();
                input.TagsItems = this.tagsService.GetAllAsKeyValuePairs();
                input.PublisherItems = this.publishersService.GetAllAsKeyValuePairs();
                return this.View(input);
            }

            // return this.Json(input);
            try
            {
                await this.gamesService.CreateAsync(input, $"{this.environment.WebRootPath}/images/games");
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                input.GenresItems = this.genresService.GetAllAsKeyValuePairs();
                input.LanguagesItems = this.languagesService.GetAllAsKeyValuePairs();
                input.TagsItems = this.tagsService.GetAllAsKeyValuePairs();
                input.PublisherItems = this.publishersService.GetAllAsKeyValuePairs();
                return this.View(input);
            }

            // this.TempData["Message"] = "Game added successfully.";

            return this.RedirectToAction("All");
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public IActionResult Edit(int id)
        {
            var inputModel = this.gamesService.GetById<EditGameInputModel>(id);
            inputModel.GenresItems = this.genresService.GetAllAsKeyValuePairs();
            inputModel.PublisherItems = this.publishersService.GetAllAsKeyValuePairs();

            // inputModel.LanguagesItems = this.languagesService.GetAllAsKeyValuePairs();
            return this.View(inputModel);
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> Edit(int id, EditGameInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.GenresItems = this.genresService.GetAllAsKeyValuePairs();
                input.PublisherItems = this.publishersService.GetAllAsKeyValuePairs();

                // input.LanguagesItems = this.languagesService.GetAllAsKeyValuePairs();
                return this.View(input);
            }

            await this.gamesService.UpdateAsync(id, input);

            return this.RedirectToAction(nameof(this.ById), new { id });
        }

        public IActionResult All(int id = 1)
        {
            if (id <= 0)
            {
                return this.NotFound();
            }

            const int itemsPerPage = 16;
            var viewModel = new GamesListViewModel
            {
                ItemsPerPage = itemsPerPage,
                PageNumber = id,
                GamesCount = this.gamesService.GetCount(),
                Games = this.gamesService.GetAll<GameInListViewModel>(id, 16),
            };

            return this.View(viewModel);
        }

        public IActionResult ById(int id)
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                var game = this.gamesService.GetById<SingleGameViewModel>(id);
                return this.View(game);
            }
            else
            {
                var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                var game = this.gamesService.GetById<SingleGameViewModel>(id);

                game.IsInUserCart = this.db.UserCartItems.Any(c => c.UserId == userId && c.GameId == id);
                game.IsInUserWishlist = this.db.Wishlists.Any(c => c.UserId == userId && c.GameId == id);

                return this.View(game);
            }
        }
    }
}
