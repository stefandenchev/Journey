namespace Journey.Web.Controllers
{
    using Journey.Services.Data.Interfaces;
    using Journey.Web.ViewModels.News;
    using Microsoft.AspNetCore.Mvc;

    public class NewsController : BaseController
    {
        private readonly INewsService newsService;

        public NewsController(INewsService newsService)
        {
            this.newsService = newsService;
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
