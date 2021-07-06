namespace Journey.Web.ViewModels.Games.Edit
{
    using System.ComponentModel;

    using Journey.Data.Models;
    using Journey.Services.Mapping;

    public class EditGameInputModel : GameBaseInputModel, IMapFrom<Game>
    {
        public int Id { get; set; }

        [DisplayName("On Sale Toggle")]
        public bool IsOnSale { get; set; }

        [DisplayName("Sale Percentage")]
        public int SalePercentage { get; set; }
    }
}
