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

        public int GameId { get; set; }

        [Display(Name = "Games")]
        public IEnumerable<KeyValuePair<string, string>> GamesItems { get; set; }

        public int LanguageId { get; set; }

        [Display(Name = "Languages")]
        public IEnumerable<KeyValuePair<string, string>> LanguagesItems { get; set; }

        public DateTime CreatedOn_17114092 { get; set; }
    }
}
