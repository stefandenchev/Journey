namespace Journey.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;

    using Journey.Common;
    using Journey.Data;
    using Journey.Data.Models;
    using Journey.Services.Data.Interfaces;
    using Journey.Web.ViewModels;
    using Journey.Web.ViewModels.Export;
    using Journey.Web.ViewModels.Games;
    using Journey.Web.ViewModels.Games.Create;
    using Journey.Web.ViewModels.Games.Edit;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;

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

            return this.RedirectToAction("All");
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public IActionResult Edit(int id)
        {
            var inputModel = this.gamesService.GetById<EditGameInputModel>(id);
            inputModel.GenresItems = this.genresService.GetAllAsKeyValuePairs();
            inputModel.PublisherItems = this.publishersService.GetAllAsKeyValuePairs();

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

                return this.View(input);
            }

            await this.gamesService.UpdateAsync(id, input);

            return this.RedirectToAction(nameof(this.ById), new { id });
        }

        public IActionResult All(int id = 1)
        {
            const int itemsPerPage = 16;
            var viewModel = new GamesListViewModel
            {
                ItemsPerPage = itemsPerPage,
                PageNumber = id,
                ItemsCount = this.gamesService.GetCount(),
                Games = this.gamesService.GetAllInList<GameInListViewModel>(id, 16),
            };

            if (id <= 0 || id > viewModel.PagesCount)
            {
                return this.NotFound();
            }

            return this.View(viewModel);
        }

        public IActionResult ById(int id)
        {
            var game = this.gamesService.GetById<SingleGameViewModel>(id);
            if (game == null)
            {
                return this.RedirectToPage("/NotFound", new { Area = "Home", Controller = "Home" });
            }

            if (!this.User.Identity.IsAuthenticated)
            {
                return this.View(game);
            }
            else
            {
                var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

                List<string> allOrderIds = new();
                var allOrders = this.db.Orders.Where(o => o.UserId == userId);
                foreach (Order o in allOrders)
                {
                    allOrderIds.Add(o.Id);
                }

                game.IsInUserCart = this.db.UserCartItems.Any(x => x.UserId == userId && x.GameId == id);
                game.IsInUserWishlist = this.db.Wishlists.Any(x => x.UserId == userId && x.GameId == id);
                game.IsInUserLibrary = this.db.OrderItems.Any(x => x.GameId == id && allOrderIds.Contains(x.OrderId));

                return this.View(game);
            }
        }

        public IActionResult ExportToJson(int id)
        {
            var game = this.gamesService.GetById<GameJsonExportModel>(id);

            string jsonResult = JsonConvert.SerializeObject(game, Formatting.Indented);

            var fileName = $"{game.Title}.txt";
            var mimeType = "text/plain";
            var fileBytes = Encoding.ASCII.GetBytes(jsonResult);
            return new FileContentResult(fileBytes, mimeType)
            {
                FileDownloadName = fileName,
            };
        }
    }
}
