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
        private readonly IOrdersService ordersService;

        public PostsController(
            IPostsService postsService,
            ICategoriesService categoriesService,
            IUsersService usersService,
            IOrdersService ordersService)
        {
            this.postsService = postsService;
            this.categoriesService = categoriesService;
            this.usersService = usersService;
            this.ordersService = ordersService;
        }

        public IActionResult ById(int id)
        {
            var postViewModel = this.postsService.GetById<PostViewModel>(id);
            if (postViewModel == null)
            {
                return this.NotFound();
            }

            foreach (var comment in postViewModel.Comments)
            {
                var commenterGamesBought = this.ordersService.GetGamesBoughtCount(comment.UserId);

                comment.UserProfile = new ProfileViewModel
                {
                    ImageUrl = this.usersService.GetProfilePicturePath(comment.UserId),
                    GamesBought = commenterGamesBought,
                    ProfileRank = this.usersService.GetProfileRank(commenterGamesBought),
                    Badge = this.usersService.GetProfileBadge(commenterGamesBought),
                };
            }

            var gamesBought = this.ordersService.GetGamesBoughtCount(postViewModel.UserId);

            postViewModel.UserProfile = new ProfileViewModel
            {
                ImageUrl = this.usersService.GetProfilePicturePath(postViewModel.UserId),
                GamesBought = gamesBought,
                ProfileRank = this.usersService.GetProfileRank(gamesBought),
                Badge = this.usersService.GetProfileBadge(gamesBought),
            };

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
