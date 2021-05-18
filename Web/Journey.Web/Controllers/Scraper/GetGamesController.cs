namespace Journey.Web.Controllers.Scraper
{
    using System.Threading.Tasks;

    using Journey.Services;
    using Microsoft.AspNetCore.Mvc;

    public class GetGamesController : BaseController
    {
        private readonly IGameStoreScraperService gameStoreScraperService;

        public GetGamesController(IGameStoreScraperService gameStoreScraperService)
        {
            this.gameStoreScraperService = gameStoreScraperService;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        public async Task<IActionResult> Add()
        {
            await this.gameStoreScraperService.PopulateDbAsync(13500);

            return this.View();
        }
    }
}
