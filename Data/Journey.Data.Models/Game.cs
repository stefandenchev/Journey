namespace Journey.Data.Models
{
    using System;
    using System.Collections.Generic;

    using Journey.Data.Common.Models;

    public class Game : BaseDeletableModel<int>
    {
        public Game()
        {
            this.Languages = new HashSet<GameLanguage>();
            this.Tags = new HashSet<GameTag>();
            this.Images = new HashSet<Image>();
        }

        public string Title { get; set; }

        public string Description { get; set; }

        public int PublisherId { get; set; }

        public virtual Publisher Publisher { get; set; }

        public DateTime ReleaseDate { get; set; }

        public int GenreId { get; set; }

        public virtual Genre Genre { get; set; }

        public virtual ICollection<GameLanguage> Languages { get; set; }

        public virtual ICollection<GameTag> Tags { get; set; }

        public virtual ICollection<Image> Images { get; set; }

        public string Drm { get; set; } = "Steam";

        public string MininumRequirements { get; set; }

        public string RecommendedRequirements { get; set; }

        public decimal Price { get; set; }

        public string OriginalUrl { get; set; }
    }
}
