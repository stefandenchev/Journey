namespace Journey.Web.ViewModels.Games
{
    using System.Collections.Generic;

    using Journey.Data.Models;
    using Journey.Services.Mapping;

    public class SingleGameViewModel : IMapFrom<Game>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string PublisherName { get; set; }

        public virtual IEnumerable<LanguagesViewModel> Languages { get; set; }

        public string Drm { get; set; }

        public string MininumRequirements { get; set; }

        public string RecommendedRequirements { get; set; }

        public decimal Price { get; set; }
    }
}
