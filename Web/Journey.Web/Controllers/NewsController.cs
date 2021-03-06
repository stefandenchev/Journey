namespace Journey.Web.Controllers
{
    using Journey.Services.Data.Interfaces;
    using Journey.Web.ViewModels.News;
    using Microsoft.AspNetCore.Mvc;

    public class NewsController : BaseController
    {
        private const int PostsPerPageDefaultValue = 3;
        private readonly INewsService newsService;

        public NewsController(INewsService newsService)
        {
            this.newsService = newsService;
        }

        public IActionResult All(int id = 1, int perPage = PostsPerPageDefaultValue)
        {
            var model = new NewsListViewModel
            {
                PageNumber = id,
                ItemsPerPage = perPage,
                ItemsCount = this.newsService.GetCount(),
                News = this.newsService.GetAllInList<NewsInListViewModel>(id, 3),
            };

            return this.View(model);
        }

        public IActionResult Post(int id)
        {
            var viewModel = this.newsService.GetById<NewsPostViewModel>(id);

            if (viewModel == null)
            {
                return this.NotFound("Blog post not found");
            }

            return this.View(viewModel);
        }
    }
}
