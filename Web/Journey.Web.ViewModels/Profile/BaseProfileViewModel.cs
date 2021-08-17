namespace Journey.Web.ViewModels.Profile
{
    using AutoMapper;
    using Journey.Data.Models;
    using Journey.Services.Mapping;

    public class BaseProfileViewModel : IHaveCustomMappings
    {
        public string ImageUrl { get; set; }

        public virtual void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<UserImage, BaseProfileViewModel>()
                .ForMember(x => x.ImageUrl, opt =>
                opt.MapFrom(x => "/images/users/" + x.Id + "." + x.Extension));
        }
    }
}
