namespace Journey.Web.ViewModels.Games.Edit
{
    using Journey.Data.Models;
    using Journey.Services.Mapping;

    public class EditGameInputModel : BaseGameInputModel, IMapFrom<Game>
    {
        public int Id { get; set; }
    }
}
