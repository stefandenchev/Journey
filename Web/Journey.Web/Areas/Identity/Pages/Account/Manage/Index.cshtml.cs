namespace Journey.Web.Areas.Identity.Pages.Account.Manage
{
    using System.ComponentModel.DataAnnotations;
    using System.Threading.Tasks;

    using Journey.Data.Models;
    using Journey.Services.Data.Interfaces;
    using Journey.Web.ViewModels.Profile;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    public partial class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IUsersService usersService;
        private readonly IOrdersService ordersService;
        private readonly IWebHostEnvironment environment;

        public IndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IUsersService usersService,
            IOrdersService ordersService,
            IWebHostEnvironment environment)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.usersService = usersService;
            this.ordersService = ordersService;
            this.environment = environment;
        }

        public string Username { get; set; }

        public string PhoneNumber { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        [Display(Name = "Picture")]
        public IFormFile ProfilePictureUpload { get; set; }

        public ProfileViewModel ProfileInfo { get; set; }

        public class InputModel
        {
            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            if (user == null)
            {
                return this.NotFound($"Unable to load user with ID '{this.userManager.GetUserId(this.User)}'.");
            }

            var gamesBought = this.ordersService.GetGamesBoughtCount(user.Id);

            await this.LoadAsync(user);
            this.ProfileInfo = new ProfileViewModel
            {
                ImageUrl = this.usersService.GetProfilePicturePath(user.Id),
                GamesBought = gamesBought,
                ProfileRank = this.usersService.GetProfileRank(gamesBought),
                Badge = this.usersService.GetProfileBadge(gamesBought),
            };

            return this.Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            if (user == null)
            {
                return this.NotFound($"Unable to load user with ID '{this.userManager.GetUserId(this.User)}'.");
            }

            if (!this.ModelState.IsValid)
            {
                await this.LoadAsync(user);
                return this.Page();
            }

            var phoneNumber = await this.userManager.GetPhoneNumberAsync(user);
            if (this.Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await this.userManager.SetPhoneNumberAsync(user, this.Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    this.StatusMessage = "Unexpected error when trying to set phone number.";
                    return this.RedirectToPage();
                }
            }

            await this.usersService.AddProfilePicture(this.ProfilePictureUpload, user.Id, $"{this.environment.WebRootPath}/images/users");

            await this.signInManager.RefreshSignInAsync(user);
            this.StatusMessage = "Your profile has been updated";
            return this.RedirectToPage();
        }

        private async Task LoadAsync(ApplicationUser user)
        {
            var userName = await this.userManager.GetUserNameAsync(user);
            var phoneNumber = await this.userManager.GetPhoneNumberAsync(user);

            this.Username = userName;

            this.Input = new InputModel
            {
                PhoneNumber = phoneNumber,
            };
        }
    }
}
