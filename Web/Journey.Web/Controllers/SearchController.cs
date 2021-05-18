namespace Journey.Web.Controllers
{
    using System;
    using System.Linq;

    using Journey.Services.Data.Interfaces;
    using Journey.Web.ViewModels;
    using Journey.Web.ViewModels.Search;
    using Microsoft.AspNetCore.Mvc;

    public class SearchController : BaseController
    {
        private readonly ISearchService searchService;

        public SearchController(ISearchService searchService)
        {
            this.searchService = searchService;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        [HttpGet]
        public IActionResult Results(string s)
        {
            var viewModel = new SearchListViewModel();

            if (string.IsNullOrEmpty(s))
            {
                viewModel = new SearchListViewModel
                {
                    Games = this.searchService.GetAll<GameInListViewModel>(),
                };
            }
            else
            {
                viewModel = new SearchListViewModel
                {
                    Games = this.searchService.GetAll<GameInListViewModel>()
                    .Where(x => x.Title.ToLower().Contains(s.ToLower()))
                    .OrderBy(x => x.Title),
                };
            }

            return this.View(viewModel);
        }

        [HttpGet]
        public IActionResult Genre(string g)
        {
            var viewModel = new GenreListViewModel
            {
                GenreName = g,
                Games = this.searchService.GetAll<GameInListViewModel>()
                .Where(x => x.GenreName == g)
                .OrderBy(x => x.Title),
            };

            return this.View(viewModel);
        }

        [HttpGet]
        public IActionResult Publisher(string p)
        {
            var viewModel = new PublisherListViewModel
            {
                PublisherName = p,
                Games = this.searchService.GetAll<GameInListViewModel>()
                .Where(x => x.PublisherName == p)
                .OrderBy(x => x.Title),
            };

            return this.View(viewModel);
        }
    }
}
