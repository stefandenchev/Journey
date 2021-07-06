namespace Journey.Web.ViewModels.Games.Edit
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    using Journey.Data.Models;
    using Journey.Services.Mapping;

    public class EditGameInputModel : GameBaseInputModel, IMapFrom<Game>
    {
        public int Id { get; set; }

        [DisplayName("On Sale")]
        public bool IsOnSale { get; set; }

        [DisplayName("Sale Percentage")]
        [Range(0, 99)]
        public int SalePercentage { get; set; }
    }
}
