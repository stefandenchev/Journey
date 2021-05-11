namespace Journey.Web.ViewModels.Games.Create
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

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

        [Required]
        [Display(Name = "Publisher")]
        public int PublisherId { get; set; }

        public IEnumerable<KeyValuePair<string, string>> PublisherItems { get; set; }

        public IEnumerable<int> Languages { get; set; }

        [Display(Name = "Languages")]
        public IEnumerable<KeyValuePair<string, string>> LanguagesItems { get; set; }

        public IEnumerable<int> Tags { get; set; }

        [Display(Name = "Tags")]
        public IEnumerable<KeyValuePair<string, string>> TagsItems { get; set; }

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

        public IEnumerable<IFormFile> Images { get; set; }
    }
}
