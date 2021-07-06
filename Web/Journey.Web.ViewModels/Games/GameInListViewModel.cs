namespace Journey.Web.ViewModels
{
    using System;

    using Journey.Data.Models;
    using Journey.Services.Mapping;
    using Journey.Web.ViewModels.Games;

    public class GameInListViewModel : GameBaseViewModel, IMapFrom<OrderItem>, IMapFrom<Game>, IHaveCustomMappings
    {
        public string Title { get; set; }

        public decimal Price { get; set; }

        public decimal CurrentPrice { get; set; }

        public DateTime ReleaseDate { get; set; }

        public string GenreName { get; set; }

        public string PublisherName { get; set; }

        public bool IsOnSale { get; set; }

        public int SalePercentage { get; set; }

        public int PriceOnPurchase { get; set; }
    }
}
