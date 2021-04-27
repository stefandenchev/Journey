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
            this.Genres = new HashSet<GameGenre>();
        }

        public string Title { get; set; }

        public string Description { get; set; }

        public int PublisherId { get; set; }

        public virtual Publisher Publisher { get; set; }

        public DateTime ReleaseDate { get; set; }

        public virtual ICollection<GameGenre> Genres { get; set; }

        public virtual ICollection<GameLanguage> Languages { get; set; }

        public virtual ICollection<GameTag> Tags { get; set; }

        public virtual ICollection<Image> Images { get; set; }

        public int VideoId { get; set; }

        public virtual Video Video { get; set; }

        public string Drm { get; set; }

        public string MininumRequirements { get; set; }

        public string RecommendedRequirements { get; set; }

        public decimal Price { get; set; }

        public string OriginalUrl { get; set; }
    }
}
