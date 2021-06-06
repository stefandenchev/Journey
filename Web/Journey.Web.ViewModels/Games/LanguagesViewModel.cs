namespace Journey.Web.ViewModels.Games
{
    using Journey.Data.Models;
    using Journey.Services.Mapping;
    using Newtonsoft.Json;

    public class LanguagesViewModel : IMapFrom<GameLanguage>
    {
        [JsonProperty("Language")]
        public string LanguageName { get; set; }
    }
}
