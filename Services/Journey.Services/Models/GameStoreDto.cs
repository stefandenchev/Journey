namespace Journey.Services.Models
{
    using System.Collections.Generic;

    using Journey.Data.Models;

    public class GameStoreDto
    {
        public GameStoreDto()
        {
            this.Languages = new List<string>();
            this.Tags = new List<string>();
        }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Publisher { get; set; }

        public string ReleaseDate { get; set; }

        public string Genre { get; set; }

        public List<string> Languages { get; set; }

        public List<string> Tags { get; set; }

        public List<Image> Images { get; set; }

        public string Drm { get; set; }

        public string MininumRequirements { get; set; }

        public string RecommendedRequirements { get; set; }

        public decimal Price { get; set; }

        public string OriginalUrl { get; set; }
    }
}
