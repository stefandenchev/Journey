namespace Journey.Web.ViewModels.Games
{
    using System;

    public class CreateGameInputModel
    {

        public string Title { get; set; }

        public string Description { get; set; }

        public int PublisherId { get; set; }

        public DateTime ReleaseDate { get; set; }

        public string Drm { get; set; }

        public string MininumRequirements { get; set; }

        public string RecommendedRequirements { get; set; }

        public decimal Price { get; set; }
    }
}
