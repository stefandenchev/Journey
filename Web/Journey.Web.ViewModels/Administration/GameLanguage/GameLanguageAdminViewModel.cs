namespace Journey.Web.ViewModels.Administration.GameLanguage
{
    using Journey.Data.Models;
    using Journey.Services.Mapping;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class GameLanguageAdminViewModel
    {
        public IEnumerable<GamesLanguagesListViewModel> GamesLanguages { get; set; }
    }
}
