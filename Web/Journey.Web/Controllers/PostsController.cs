namespace Journey.Web.Controllers
{
    using System.Threading.Tasks;

    using Journey.Services.Data.Interfaces;
    using Journey.Web.Infrastructure;
    using Journey.Web.ViewModels.Forum;
    using Journey.Web.ViewModels.Forum.Categories;
    using Journey.Web.ViewModels.Forum.Posts;
    using Journey.Web.ViewModels.Profile;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class PostsController : BaseController
    {
        private readonly IPostsService postsService;
        private readonly ICategoriesService categoriesService;
        private readonly IUsersService usersService;

        public PostsController(
            IPostsService postsService,
            ICategoriesService categoriesService,
            IUsersService usersService)
        {
            this.postsService = postsService;
            this.categoriesService = categoriesService;
            this.usersService = usersService;
        }

        public IActionResult ById(int id)
        {
            var userId = this.User.GetId();

            var postViewModel = this.postsService.GetById<PostViewModel>(id);
            if (postViewModel == null)
            {
                return this.NotFound();
            }

            foreach (var comment in postViewModel.Comments)
            {
                comment.UserProfilePicture = this.usersService.GetProfilePicture<ProfilePictureViewModel>(comment.UserId);
            }

            postViewModel.UserProfilePicture = this.usersService.GetProfilePicture<ProfilePictureViewModel>(postViewModel.UserId);
            return this.View(postViewModel);
        }

        [Authorize]
        public IActionResult Create()
        {
            var categories = this.categoriesService.GetAll<CategoryDropDownViewModel>();
            var viewModel = new ForumPostCreateInputModel
            {
                Categories = categories,
            };
            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(ForumPostCreateInputModel input)
        {
            var userId = this.User.GetId();
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var postId = await this.postsService.CreateAsync(input.Title, input.Content, input.CategoryId, userId);
            this.TempData["InfoMessage"] = "Forum post created!";
            return this.RedirectToAction(nameof(this.ById), new { id = postId });
        }
    }
}
