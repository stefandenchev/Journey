namespace Journey.Web.ViewModels.Games
{
    using Journey.Data.Models;
    using Journey.Services.Mapping;

    public class TagViewModel : IMapFrom<GameTag>
    {
        public string TagName { get; set; }
    }
}