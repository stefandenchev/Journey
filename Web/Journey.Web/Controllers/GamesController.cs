namespace Journey.Web.Controllers
{
    using System;
    using System.Text;
    using System.Threading.Tasks;

    using Journey.Common;
    using Journey.Services.Data.Interfaces;
    using Journey.Web.Infrastructure;
    using Journey.Web.ViewModels;
    using Journey.Web.ViewModels.Export;
    using Journey.Web.ViewModels.Games;
    using Journey.Web.ViewModels.Games.Create;
    using Journey.Web.ViewModels.Games.Edit;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Memory;
    using Newtonsoft.Json;

    public class GamesController : BaseController
    {
        private readonly IGamesService gamesService;
        private readonly IGenresService genresService;
        private readonly ILanguagesService languagesService;
        private readonly ITagsService tagsService;
        private readonly IPublishersService publishersService;
        private readonly IWebHostEnvironment environment;
        private readonly IWishlistService wishlistService;
        private readonly IOrdersService ordersService;
        private readonly ICartService cartService;
        private readonly IMemoryCache cache;

        public GamesController(
            IGamesService gamesService,
            IGenresService genresService,
            ILanguagesService languagesService,
            ITagsService tagsService,
            IPublishersService publishersService,
            IWebHostEnvironment environment,
            IWishlistService wishlistService,
            IOrdersService ordersService,
            ICartService cartService,
            IMemoryCache cache)
        {
            this.gamesService = gamesService;
            this.genresService = genresService;
            this.languagesService = languagesService;
            this.tagsService = tagsService;
            this.publishersService = publishersService;
            this.environment = environment;
            this.wishlistService = wishlistService;
            this.ordersService = ordersService;
            this.cartService = cartService;
            this.cache = cache;
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public IActionResult Create()
        {
            var viewModel = new CreateGameInputModel
            {
                GenresItems = this.genresService.GetAllAsKeyValuePairs(),
                LanguagesItems = this.languagesService.GetAllAsKeyValuePairs(),
                TagsItems = this.tagsService.GetAllAsKeyValuePairs(),
                PublisherItems = this.publishersService.GetAllAsKeyValuePairs(),
            };

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
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

            try
            {
                await this.gamesService.CreateAsync(input, $"{this.environment.WebRootPath}/images/users");
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
                Games = this.gamesService.All<GameInListViewModel>(id, itemsPerPage),
            };

            if (id <= 0)
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
                 return this.Redirect("/Home/NotFound");
            }

            if (!this.User.Identity.IsAuthenticated)
            {
                return this.View(game);
            }
            else
            {
                var userId = this.User.GetId();

                game.IsInUserCart = this.cartService.IsInCart(userId, id);
                game.IsInUserWishlist = this.wishlistService.IsInWish(userId, id);
                game.IsInUserLibrary = this.ordersService.IsInLibrary(userId, id);

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
