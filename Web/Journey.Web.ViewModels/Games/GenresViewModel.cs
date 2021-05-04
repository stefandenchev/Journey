namespace Journey.Web.ViewModels.Games
{
    using Journey.Data.Models;
    using Journey.Services.Mapping;

    public class GenresViewModel : IMapFrom<GameGenre>
    {
        public string GenreName { get; set; }
    }
}
