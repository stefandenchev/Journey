namespace Journey.Web.Controllers
{
    using System.Diagnostics;

    using Journey.Services.Data.Interfaces;
    using Journey.Web.ViewModels;
    using Journey.Web.ViewModels.Games.Home;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        private readonly IGamesService gamesService;

        public HomeController(IGamesService gamesService)
        {
            this.gamesService = gamesService;
        }

        public IActionResult Index()
        {
            var viewModel = new HomeGamesViewModel
            {
                Lastest = new LatestReleasesViewModel { Games = this.gamesService.GetLatest<GameInListViewModel>() },
                Curated = new CuratedGamesViewModel { Games = this.gamesService.GetCurated<GameInListViewModel>() },
            };

            return this.View(viewModel);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }

        public IActionResult NotFound()
        {
            return this.View();
        }

        public IActionResult About()
        {
            return this.View();
        }
    }
}
