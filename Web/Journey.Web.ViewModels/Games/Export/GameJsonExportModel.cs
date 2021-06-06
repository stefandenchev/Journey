namespace Journey.Web.ViewModels.Games.Export
{
    using System;
    using System.Collections.Generic;

    using Journey.Data.Models;
    using Journey.Services.Mapping;
    using Newtonsoft.Json;

    public class GameJsonExportModel : IMapFrom<Game>
    {
        public string Title { get; set; }

        public string Description { get; set; }

        [JsonConverter(typeof(DateFormatConverter), "dd-MM-yyyy")]
        public DateTime ReleaseDate { get; set; }

        [JsonProperty("Genre")]
        public string GenreName { get; set; }

        [JsonProperty("Publisher")]

        public string PublisherName { get; set; }

        public string Drm { get; set; }

        public virtual IEnumerable<TagViewModel> Tags { get; set; }

        public virtual IEnumerable<LanguagesViewModel> Languages { get; set; }

        public string MininumRequirements { get; set; }

        public string RecommendedRequirements { get; set; }

        public decimal Price { get; set; }
    }
}
