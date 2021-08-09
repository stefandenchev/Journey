namespace Journey.Web.Controllers
{
    using Journey.Services.Data.Interfaces;
    using Journey.Web.ViewModels;
    using Journey.Web.ViewModels.Forum.Categories;
    using Journey.Web.ViewModels.Forum.Posts;
    using Microsoft.AspNetCore.Mvc;

    public class ForumController : BaseController
    {
        private readonly ICategoriesService categoriesService;
        private readonly IPostsService postsService;

        public ForumController(
            ICategoriesService categoriesService,
            IPostsService postsService)
        {
            this.categoriesService = categoriesService;
            this.postsService = postsService;
        }

        public IActionResult Index()
        {
            ForumViewModel viewModel = new()
            {
                Categories = this.categoriesService.GetAll<CategoriesViewModel>(),
            };

            return this.View(viewModel);
        }

        public IActionResult ByTitle(string title, int page = 1)
        {
            const int itemsPerPage = 12;
            var viewModel = this.categoriesService.GetByTitle<CategoryViewModel>(title);

            viewModel.ItemsPerPage = itemsPerPage;
            viewModel.PageNumber = page;
            viewModel.ItemsCount = this.postsService.GetCount(viewModel.Id);
            viewModel.ForumPosts = this.postsService.GetAllInList<PostInCategoryViewModel>(viewModel.Id, page, itemsPerPage);

            if (page <= 0 || page > viewModel.PagesCount)
            {
                return this.NotFound();
            }

            return this.View(viewModel);
        }
    }
}
