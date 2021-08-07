namespace Journey.Web.ViewModels.Wishlist
{
    using System.Linq;

    using AutoMapper;
    using Journey.Data.Models;
    using Journey.Services.Mapping;
    using Journey.Web.ViewModels.Games;

    public class GameInWishlistServiceModel : GameBaseViewModel, IHaveCustomMappings
    {
        public decimal Price { get; set; }

        public decimal CurrentPrice { get; set; }

        public override void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Game, GameInWishlistServiceModel>()
                .ForMember(x => x.ImageUrl, opt =>
                opt.MapFrom(x => x.Images.FirstOrDefault(x => x.OriginalUrl.Contains("boxshots")).OriginalUrl != null ?
                x.Images.FirstOrDefault(x => x.OriginalUrl.Contains("boxshots")).OriginalUrl :
                "/images/games/" + x.Images.FirstOrDefault(x => x.UploadName.Contains("cover")).Id + "." + x.Images.FirstOrDefault(x => x.UploadName.Contains("cover")).Extension));
        }
    }
}
