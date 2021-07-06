namespace Journey.Web.ViewModels.Games
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public abstract class GameBaseInputModel
    {
        [Required]
        [MinLength(3)]
        public string Title { get; set; }

        [Required]
        [MinLength(100)]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Publisher")]
        public int PublisherId { get; set; }

        public IEnumerable<KeyValuePair<string, string>> PublisherItems { get; set; }

        [Required]
        [Display(Name = "Genre")]
        public int GenreId { get; set; }

        public IEnumerable<KeyValuePair<string, string>> GenresItems { get; set; }

        [Range(0.99, 199.99)]
        public decimal Price { get; set; }

        public string Drm { get; set; }

        [Required]
        [MinLength(30)]
        [Display(Name = "Mininum Requirements")]
        public string MininumRequirements { get; set; }

        [Required]
        [MinLength(30)]
        [Display(Name = "Recommended Requirements")]
        public string RecommendedRequirements { get; set; }
    }
}
