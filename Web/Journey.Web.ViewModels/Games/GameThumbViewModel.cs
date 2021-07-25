namespace Journey.Web.ViewModels.Games
{
    using System.Linq;

    using AutoMapper;
    using Journey.Data.Models;

    public class GameThumbViewModel : GameBaseViewModel
    {
        public string Title { get; set; }

        public decimal PriceOnPurchase { get; set; }

        public override void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Game, GameThumbViewModel>()
                .ForMember(x => x.ImageUrl, opt =>
                opt.MapFrom(x => x.Images.FirstOrDefault(x => x.OriginalUrl.Contains("boxshots")).OriginalUrl != null ?
                x.Images.FirstOrDefault(x => x.OriginalUrl.Contains("boxshots")).OriginalUrl :
                "/images/games/" + x.Images.FirstOrDefault(x => x.UploadName.Contains("cover")).Id + "." + x.Images.FirstOrDefault(x => x.UploadName.Contains("cover")).Extension));
        }
    }
}
