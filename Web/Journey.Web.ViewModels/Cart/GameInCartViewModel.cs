namespace Journey.Web.ViewModels.Cart
{
    using System.Linq;

    using AutoMapper;
    using Journey.Data.Models;
    using Journey.Services.Mapping;

    public class GameInCartViewModel : IMapFrom<Game>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string ImageUrl { get; set; }

        public decimal Price { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Game, GameInCartViewModel>()
                .ForMember(x => x.ImageUrl, opt =>
                opt.MapFrom(x => x.Images.FirstOrDefault(x => x.OriginalUrl.Contains("boxshots")).OriginalUrl != null ?
                x.Images.FirstOrDefault(x => x.OriginalUrl.Contains("boxshots")).OriginalUrl :
                "/images/games/" + x.Images.FirstOrDefault(x => x.UploadName.Contains("cover")).Id + "." + x.Images.FirstOrDefault().Extension));
        }
    }
}
