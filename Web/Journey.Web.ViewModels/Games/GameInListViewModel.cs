namespace Journey.Web.ViewModels
{
    using System;
    using System.Linq;

    using AutoMapper;
    using Journey.Data.Models;
    using Journey.Services.Mapping;
    using Journey.Web.ViewModels.Games;

    public class GameInListViewModel : GameBaseViewModel, IHaveCustomMappings
    {
        public decimal Price { get; set; }

        public decimal CurrentPrice { get; set; }

        public DateTime ReleaseDate { get; set; }

        public string GenreName { get; set; }

        public string PublisherName { get; set; }

        public bool IsOnSale { get; set; }

        public int SalePercentage { get; set; }

        public override void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Game, GameInListViewModel>()
                .ForMember(x => x.ImageUrl, opt =>
                opt.MapFrom(x => x.Images.FirstOrDefault(x => x.OriginalUrl.Contains("boxshots")).OriginalUrl != null ?
                x.Images.FirstOrDefault(x => x.OriginalUrl.Contains("boxshots")).OriginalUrl :
                "/images/games/" + x.Images.FirstOrDefault(x => x.UploadName.Contains("cover")).Id + "." + x.Images.FirstOrDefault(x => x.UploadName.Contains("cover")).Extension));
        }
    }
}
