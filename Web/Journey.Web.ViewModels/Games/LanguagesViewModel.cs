namespace Journey.Web.ViewModels.Games
{
    using Journey.Data.Models;
    using Journey.Services.Mapping;

    public class LanguagesViewModel : IMapFrom<GameLanguage>
    {
        public string LanguageName { get; set; }
    }
}
