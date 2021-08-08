namespace Journey.Web.Controllers
{
    using System;
    using System.Diagnostics;

    using Journey.Services.Data.Interfaces;
    using Journey.Web.ViewModels;
    using Journey.Web.ViewModels.Games.Home;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Memory;

    using static Journey.Common.GlobalConstants.Cache;

    public class HomeController : BaseController
    {
        private readonly IGamesService gamesService;
        private readonly IMemoryCache cache;

        public HomeController(
            IGamesService gamesService,
            IMemoryCache cache)
        {
            this.gamesService = gamesService;
            this.cache = cache;
        }

        public IActionResult Index()
        {
            var viewModel = this.cache.Get<HomeGamesViewModel>(HomeGamesCacheKey);

            if (viewModel == null)
            {
                viewModel = new HomeGamesViewModel
                {
                    Lastest = new LatestReleasesViewModel { Games = this.gamesService.GetLatest<GameInListViewModel>() },
                    Curated = new CuratedGamesViewModel { Games = this.gamesService.GetCurated<GameInListViewModel>() },
                };

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(15));

                this.cache.Set(HomeGamesCacheKey, viewModel, cacheOptions);
            }

            return this.View(viewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }

        public new IActionResult NotFound()
        {
            return this.View();
        }
    }
}
