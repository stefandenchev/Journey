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
        public IActionResult Results(string s, string sortOrder)
        {
            this.ViewBag.TitleSortParam = string.IsNullOrEmpty(sortOrder) ? "title_desc" : string.Empty;
            this.ViewBag.PriceSortParam = sortOrder == "price_asc" ? "price_desc" : "price_asc";

            var viewModel = new SearchListViewModel();

            if (string.IsNullOrEmpty(s))
            {
                viewModel = new SearchListViewModel
                {
                    Search = s,
                    Games = this.searchService.GetAll<GameInListViewModel>(),
                };
            }
            else
            {
                viewModel = new SearchListViewModel
                {
                    Search = s,
                    Games = this.searchService.GetAll<GameInListViewModel>()
                    .Where(x => x.Title.ToLower().Contains(s.ToLower()))
                    .OrderBy(x => x.Title),
                };

                if (sortOrder == "title_desc")
                {
                    viewModel.Games = this.searchService.GetAll<GameInListViewModel>()
                    .Where(x => x.Title.ToLower().Contains(s.ToLower()))
                    .OrderByDescending(x => x.Title);
                }

                if (sortOrder == "price_desc")
                {
                    viewModel.Games = this.searchService.GetAll<GameInListViewModel>()
                    .Where(x => x.Title.ToLower().Contains(s.ToLower()))
                    .OrderByDescending(x => x.Price);
                }

                if (sortOrder == "price_asc")
                {
                    viewModel.Games = this.searchService.GetAll<GameInListViewModel>()
                    .Where(x => x.Title.ToLower().Contains(s.ToLower()))
                    .OrderBy(x => x.Price);
                }
            }

            return this.View(viewModel);
        }

        [HttpGet]
        public IActionResult Genre(string g, string sortOrder)
        {
            this.ViewBag.TitleSortParam = string.IsNullOrEmpty(sortOrder) ? "title_desc" : string.Empty;
            this.ViewBag.PriceSortParam = sortOrder == "price_asc" ? "price_desc" : "price_asc";

            var viewModel = new GenreListViewModel
            {
                GenreName = g,
                Games = this.searchService.GetAll<GameInListViewModel>()
                .Where(x => x.GenreName == g)
                .OrderBy(x => x.Title),
            };

            if (sortOrder == "title_desc")
            {
                viewModel.Games = this.searchService.GetAll<GameInListViewModel>()
                .Where(x => x.GenreName == g)
                .OrderByDescending(x => x.Title);
            }

            if (sortOrder == "price_desc")
            {
                viewModel.Games = this.searchService.GetAll<GameInListViewModel>()
                .Where(x => x.GenreName == g)
                .OrderByDescending(x => x.Price);
            }

            if (sortOrder == "price_asc")
            {
                viewModel.Games = this.searchService.GetAll<GameInListViewModel>()
                .Where(x => x.GenreName == g)
                .OrderBy(x => x.Price);
            }

            return this.View(viewModel);
        }

        [HttpGet]
        public IActionResult Publisher(string p, string sortOrder)
        {
            this.ViewBag.TitleSortParam = string.IsNullOrEmpty(sortOrder) ? "title_desc" : string.Empty;
            this.ViewBag.PriceSortParam = sortOrder == "price_asc" ? "price_desc" : "price_asc";

            var viewModel = new PublisherListViewModel
            {
                PublisherName = p,
                Games = this.searchService.GetAll<GameInListViewModel>()
                .Where(x => x.PublisherName == p)
                .OrderBy(x => x.Title),
            };

            if (sortOrder == "title_desc")
            {
                viewModel.Games = this.searchService.GetAll<GameInListViewModel>()
                .Where(x => x.PublisherName == p)
                .OrderByDescending(x => x.Title);
            }

            if (sortOrder == "price_desc")
            {
                viewModel.Games = this.searchService.GetAll<GameInListViewModel>()
                .Where(x => x.PublisherName == p)
                .OrderByDescending(x => x.Price);
            }

            if (sortOrder == "price_asc")
            {
                viewModel.Games = this.searchService.GetAll<GameInListViewModel>()
                .Where(x => x.PublisherName == p)
                .OrderBy(x => x.Price);
            }

            return this.View(viewModel);
        }
    }
}
