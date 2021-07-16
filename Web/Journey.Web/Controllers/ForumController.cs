namespace Journey.Web.Controllers
{
    using Journey.Services.Data.Interfaces;
    using Journey.Web.ViewModels.Forum;
    using Microsoft.AspNetCore.Mvc;

    public class ForumController : Controller
    {
        private readonly IForumService forumService;
        private readonly ICategoriesService categoriesService;

        public ForumController(
            IForumService forumService,
            ICategoriesService categoriesService)
        {
            this.forumService = forumService;
            this.categoriesService = categoriesService;
        }

        public IActionResult Index()
        {
            ForumViewModel viewModel = new()
            {
                Categories = this.categoriesService.GetAll<CategoriesViewModel>(),
            };

            return this.View(viewModel);
        }
    }
}
