namespace Journey.Web.ViewModels.Games
{
    using AutoMapper;
    using Journey.Data.Models;
    using Journey.Services.Mapping;

    public class ImagesViewModel : IMapFrom<Image>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string OriginalUrl { get; set; }

        public string UploadName { get; set; }

        public string Extension { get; set; }

        public string LocalPath => $"/images/games/{this.UploadName}.{this.Extension}";

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Image, ImagesViewModel>()
               .ForMember(x => x.LocalPath, opt =>
               opt.MapFrom(x => $"{x.UploadName}.{x.Extension}"));
        }
    }
}
