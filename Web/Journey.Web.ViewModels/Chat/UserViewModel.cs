namespace Journey.Web.ViewModels.Chat
{
    using Journey.Data.Models;
    using Journey.Services.Mapping;
    using Journey.Web.ViewModels.Profile;

    public class UserViewModel : IMapFrom<ApplicationUser>
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public BaseProfileViewModel Profile { get; set; }
    }
}
