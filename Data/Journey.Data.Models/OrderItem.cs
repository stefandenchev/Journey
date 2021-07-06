namespace Journey.Data.Models
{
    using Journey.Data.Common.Models;

    public class OrderItem : BaseDeletableModel<int>
    {
        public string OrderId { get; set; }

        public Order Order { get; set; }

        public int GameId { get; set; }

        public Game Game { get; set; }

        public string GameKey { get; set; }

        public decimal PriceOnPurchase { get; set; }
    }
}
