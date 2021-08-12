namespace Journey.Web.ViewModels.Administration.GameLanguage
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Journey.Data.Models;
    using Journey.Services.Mapping;

    public class GameLanguageAdminInputModel : IMapFrom<GameLanguage>
    {
        public int Id { get; set; }

        [Display(Name = "Game")]
        public int GameId { get; set; }

        public IEnumerable<KeyValuePair<string, string>> GamesItems { get; set; }

        [Display(Name = "Language")]
        public int LanguageId { get; set; }

        public IEnumerable<KeyValuePair<string, string>> LanguagesItems { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
