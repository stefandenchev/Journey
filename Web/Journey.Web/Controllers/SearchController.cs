namespace Journey.Web.Controllers
{
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

        [HttpGet]
        public IActionResult Results(string s, string sortOrder)
        {
            if (string.IsNullOrEmpty(s))
            {
                return this.RedirectPermanent("/Games/All");
            }

            this.ViewBag.TitleSortParam = string.IsNullOrEmpty(sortOrder) ? "title_desc" : string.Empty;
            this.ViewBag.PriceSortParam = sortOrder == "price_asc" ? "price_desc" : "price_asc";

            var viewModel = new SearchListViewModel();

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
                .OrderByDescending(x => x.CurrentPrice);
            }

            if (sortOrder == "price_asc")
            {
                viewModel.Games = this.searchService.GetAll<GameInListViewModel>()
                .Where(x => x.Title.ToLower().Contains(s.ToLower()))
                .OrderBy(x => x.CurrentPrice);
            }

            return this.View(viewModel);
        }

        [HttpGet]
        public IActionResult Genre(string genre, string sortOrder)
        {
            this.ViewBag.TitleSortParam = string.IsNullOrEmpty(sortOrder) ? "title_desc" : string.Empty;
            this.ViewBag.PriceSortParam = sortOrder == "price_asc" ? "price_desc" : "price_asc";

            var viewModel = new GenreListViewModel
            {
                GenreName = genre,
                Games = this.searchService.GetAll<GameInListViewModel>()
                .Where(x => x.GenreName == genre)
                .OrderBy(x => x.Title),
            };

            if (sortOrder == "title_desc")
            {
                viewModel.Games = this.searchService.GetAll<GameInListViewModel>()
                .Where(x => x.GenreName == genre)
                .OrderByDescending(x => x.Title);
            }

            if (sortOrder == "price_desc")
            {
                viewModel.Games = this.searchService.GetAll<GameInListViewModel>()
                .Where(x => x.GenreName == genre)
                .OrderByDescending(x => x.CurrentPrice);
            }

            if (sortOrder == "price_asc")
            {
                viewModel.Games = this.searchService.GetAll<GameInListViewModel>()
                .Where(x => x.GenreName == genre)
                .OrderBy(x => x.CurrentPrice);
            }

            return this.View(viewModel);
        }

        [HttpGet]
        public IActionResult Publisher(string publisher, string sortOrder)
        {
            this.ViewBag.TitleSortParam = string.IsNullOrEmpty(sortOrder) ? "title_desc" : string.Empty;
            this.ViewBag.PriceSortParam = sortOrder == "price_asc" ? "price_desc" : "price_asc";

            var viewModel = new PublisherListViewModel
            {
                PublisherName = publisher,
                Games = this.searchService.GetAll<GameInListViewModel>()
                .Where(x => x.PublisherName == publisher)
                .OrderBy(x => x.Title),
            };

            if (sortOrder == "title_desc")
            {
                viewModel.Games = this.searchService.GetAll<GameInListViewModel>()
                .Where(x => x.PublisherName == publisher)
                .OrderByDescending(x => x.Title);
            }

            if (sortOrder == "price_desc")
            {
                viewModel.Games = this.searchService.GetAll<GameInListViewModel>()
                .Where(x => x.PublisherName == publisher)
                .OrderByDescending(x => x.CurrentPrice);
            }

            if (sortOrder == "price_asc")
            {
                viewModel.Games = this.searchService.GetAll<GameInListViewModel>()
                .Where(x => x.PublisherName == publisher)
                .OrderBy(x => x.CurrentPrice);
            }

            return this.View(viewModel);
        }

        [HttpGet]
        public IActionResult Sales(string sortOrder)
        {
            this.ViewBag.TitleSortParam = string.IsNullOrEmpty(sortOrder) ? "title_desc" : string.Empty;
            this.ViewBag.PriceSortParam = sortOrder == "price_desc" ? "price_asc" : "price_desc";
            this.ViewBag.DiscountSortParam = sortOrder == "disc_desc" ? "disc_asc" : "disc_desc";

            var viewModel = new SalesViewModel();

            viewModel = new SalesViewModel
            {
                Games = this.searchService.GetAll<GameInListViewModel>()
                .Where(x => x.IsOnSale)
                .OrderBy(x => x.Title),
            };

            if (sortOrder == "title_desc")
            {
                viewModel.Games = this.searchService.GetAll<GameInListViewModel>()
                .Where(x => x.IsOnSale)
                .OrderByDescending(x => x.Title);
            }

            if (sortOrder == "price_asc")
            {
                viewModel.Games = this.searchService.GetAll<GameInListViewModel>()
                .Where(x => x.IsOnSale)
                .OrderBy(x => x.CurrentPrice);
            }

            if (sortOrder == "price_desc")
            {
                viewModel.Games = this.searchService.GetAll<GameInListViewModel>()
                .Where(x => x.IsOnSale)
                .OrderByDescending(x => x.CurrentPrice);
            }

            if (sortOrder == "disc_asc")
            {
                viewModel.Games = this.searchService.GetAll<GameInListViewModel>()
                .Where(x => x.IsOnSale)
                .OrderBy(x => x.SalePercentage);
            }

            if (sortOrder == "disc_desc")
            {
                viewModel.Games = this.searchService.GetAll<GameInListViewModel>()
                .Where(x => x.IsOnSale)
                .OrderByDescending(x => x.SalePercentage);
            }

            return this.View(viewModel);
        }

    }
}
