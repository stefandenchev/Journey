namespace Journey.Web.ViewModels.Profile
{
    using AutoMapper;
    using Journey.Data.Models;
    using Journey.Services.Mapping;

    public class ProfilePictureViewModel : IMapFrom<UserImage>, IHaveCustomMappings
    {
        public string ImageUrl { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<UserImage, ProfilePictureViewModel>()
                .ForMember(x => x.ImageUrl, opt =>
                opt.MapFrom(x => "/images/users/" + x.Id + "." + x.Extension));
        }
    }
}
