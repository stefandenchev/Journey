namespace Journey.Web.ViewModels.Games.Create
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    public class CreateGameInputModel : GameBaseInputModel
    {
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        public IEnumerable<int> Tags { get; set; }

        [Display(Name = "Tags")]
        public IEnumerable<KeyValuePair<string, string>> TagsItems { get; set; }

        public IEnumerable<int> Languages { get; set; }

        [Display(Name = "Languages")]
        public IEnumerable<KeyValuePair<string, string>> LanguagesItems { get; set; }

        public IEnumerable<IFormFile> Images { get; set; }
    }
}
