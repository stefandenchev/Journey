namespace Journey.Web.ViewModels.Games
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;
    using Journey.Data.Models;
    using Journey.Services.Mapping;
    using Journey.Web.ViewModels.Search;

    public class SingleGameViewModel : IMapFrom<Game>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime ReleaseDate { get; set; }

        public string GenreName { get; set; }

        public string PublisherName { get; set; }

        public string Drm { get; set; }

        public virtual IEnumerable<TagViewModel> Tags { get; set; }

        public virtual IEnumerable<LanguagesViewModel> Languages { get; set; }

        public string MainImage { get; set; }

        public virtual IEnumerable<ImagesViewModel> Images { get; set; }

        public string MininumRequirements { get; set; }

        public string RecommendedRequirements { get; set; }

        public decimal Price { get; set; }

        public string OriginalUrl { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Game, SingleGameViewModel>()
               .ForMember(x => x.MainImage, opt =>
               opt.MapFrom(x => x.Images.FirstOrDefault(x => x.OriginalUrl.Contains("boxshots")).OriginalUrl != null ?
               x.Images.FirstOrDefault(x => x.OriginalUrl.Contains("boxshots")).OriginalUrl :
               "/images/games/" + x.Images.FirstOrDefault(x => x.UploadName.Contains("cover")).Id + "." + x.Images.FirstOrDefault(x => x.UploadName.Contains("cover")).Extension));
        }

/*        public string EditDescription(string description)
        {
            var regex = new Regex(@"(?s).*?[.?!](?:\s.*?[.?!]){0,2}");

            var matches = regex.Matches(description);

            StringBuilder sb = new();

            foreach (var match in matches)
            {
                sb.AppendLine(match.ToString());
                sb.AppendLine();
            }

            return sb.ToString();
        }*/
    }
}
