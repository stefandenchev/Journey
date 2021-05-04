namespace Journey.Web.ViewModels.Games
{
    using Journey.Data.Models;
    using Journey.Services.Mapping;

    public class ImagesViewModel : IMapFrom<Image>
    {
        public string OriginalUrl { get; set; }
    }
}
