namespace Journey.Web.ViewModels.Games.Create
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class CreateGameInputModel
    {
        [Required]
        [MinLength(3)]
        public string Title { get; set; }

        [Required]
        [MinLength(100)]
        public string Description { get; set; }

        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        [Required]
        [Display(Name = "Genre")]
        public int GenreId { get; set; }

        public IEnumerable<KeyValuePair<string, string>> GenresItems { get; set; }

        [Display(Name = "Languages")]
        public IEnumerable<KeyValuePair<string, string>> LanguagesItems { get; set; }

        [Display(Name = "Tags")]
        public IEnumerable<KeyValuePair<string, string>> TagsItems { get; set; }

        public int PublisherId { get; set; }

        [Range(0.99, 199.99)]
        public decimal Price { get; set; }

        [Required]
        [MinLength(3)]
        public string Drm { get; set; }

        [Required]
        [MinLength(30)]
        public string MininumRequirements { get; set; }

        [Required]
        [MinLength(30)]
        public string RecommendedRequirements { get; set; }

    }
}
