namespace Journey.Web.ViewModels.Profile
{
    using Journey.Data.Models;
    using Journey.Services.Mapping;

    public class OrderItemViewModel : IMapFrom<OrderItem>
    {
        public string OrderId { get; set; }

        public int GameId { get; set; }

        public string GameKey { get; set; }

        public decimal PriceOnPurchase { get; set; }
    }
}
