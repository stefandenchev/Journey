namespace Journey.Web.ViewModels.Administration.GameLanguage
{
    using Journey.Data.Models;
    using Journey.Services.Mapping;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class GameLanguageAdminInputModel : IMapFrom<GameLanguage>
    {
        public int Id { get; set; }

        public int GameId { get; set; }

        [Display(Name = "Games")]
        public IEnumerable<KeyValuePair<string, string>> GamesItems { get; set; }

        public int LanguageId { get; set; }

        [Display(Name = "Languages")]
        public IEnumerable<KeyValuePair<string, string>> LanguagesItems { get; set; }
    }
}
