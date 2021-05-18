namespace Journey.Web.ViewComponents
{
    using System;
    using System.Linq;

    using Journey.Services.Data.Interfaces;
    using Journey.Web.ViewModels;
    using Journey.Web.ViewModels.Games.Home.ViewComponents;
    using Microsoft.AspNetCore.Mvc;

    public class SamePublisherGamesListViewComponent : ViewComponent
    {
        private readonly ISearchService searchService;

        public SamePublisherGamesListViewComponent(ISearchService searchService)
        {
            this.searchService = searchService;
        }

        public IViewComponentResult Invoke(string publisherName, string currentGame)
        {
            var rnd = new Random();
            var viewModel = new SamePublisherGamesListViewModel
            {
                PublisherName = publisherName,
                Games = this.searchService.GetAll<GameInListViewModel>()
                .Where(x => x.PublisherName == publisherName && x.Title != currentGame)
                .OrderBy(x => rnd.Next())
                .Take(5),
            };

            return this.View(viewModel);
        }
    }
}
