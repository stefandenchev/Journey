namespace Journey.Web.ViewModels.Profile
{
    using AutoMapper;
    using Journey.Data.Models;
    using Journey.Services.Mapping;

    public class ProfileViewModel : BaseProfileViewModel, IHaveCustomMappings
    {
        public int GamesBought { get; set; }

        public string ProfileRank { get; set; }

        public string Badge { get; set; }

        public override void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<UserImage, ProfileViewModel>()
                .ForMember(x => x.ImageUrl, opt =>
                opt.MapFrom(x => "/images/users/" + x.Id + "." + x.Extension));
        }
    }
}
