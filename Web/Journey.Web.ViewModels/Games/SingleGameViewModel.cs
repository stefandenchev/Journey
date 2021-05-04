namespace Journey.Web.ViewModels.Games
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AutoMapper;
    using Journey.Data.Models;
    using Journey.Services.Mapping;

    public class SingleGameViewModel : IMapFrom<Game>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime ReleaseDate { get; set; }

        public virtual IEnumerable<GenresViewModel> Genres { get; set; }

        public string PublisherName { get; set; }

        public string Drm { get; set; }

        public virtual IEnumerable<TagViewModel> Tags { get; set; }

        public virtual IEnumerable<LanguagesViewModel> Languages { get; set; }

        public string MainImage { get; set; }

        public virtual IEnumerable<ImagesViewModel> Images { get; set; }

        public string MininumRequirements { get; set; }

        public string RecommendedRequirements { get; set; }

        public decimal Price { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Game, SingleGameViewModel>()
               .ForMember(x => x.MainImage, opt =>
               opt.MapFrom(x => x.Images.FirstOrDefault(x => x.OriginalUrl.Contains("boxshots"))
               .OriginalUrl));
        }
    }
}
