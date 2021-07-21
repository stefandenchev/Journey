namespace Journey.Web.ViewModels.Wishlist
{
    using System.Linq;

    using AutoMapper;
    using Journey.Data.Models;
    using Journey.Services.Mapping;

    public class GameInWishlistViewModel : IMapFrom<Wishlist>, IMapFrom<Game>
    {
        public int GameId { get; set; }

        public string GameTitle { get; set; }

        public decimal GamePrice { get; set; }

        public decimal GameCurrentPrice { get; set; }

        public string GameImageUrl { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Game, GameInWishlistViewModel>()
                .ForMember(x => x.GameImageUrl, opt =>
                opt.MapFrom(x => x.Images.FirstOrDefault(x => x.OriginalUrl.Contains("boxshots")).OriginalUrl != null ?
                x.Images.FirstOrDefault(x => x.OriginalUrl.Contains("boxshots")).OriginalUrl :
                "/images/games/" + x.Images.FirstOrDefault(x => x.UploadName.Contains("cover")).Id + "." + x.Images.FirstOrDefault(x => x.UploadName.Contains("cover")).Extension));
        }
    }
}
