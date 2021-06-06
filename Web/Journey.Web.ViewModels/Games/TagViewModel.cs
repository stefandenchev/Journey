namespace Journey.Web.ViewModels.Games
{
    using Journey.Data.Models;
    using Journey.Services.Mapping;
    using Newtonsoft.Json;

    public class TagViewModel : IMapFrom<GameTag>
    {
        [JsonProperty("Tag")]
        public string TagName { get; set; }
    }
}
