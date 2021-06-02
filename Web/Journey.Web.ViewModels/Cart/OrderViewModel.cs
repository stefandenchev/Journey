namespace Journey.Web.ViewModels.Cart
{
    using System;

    using Journey.Data.Models;
    using Journey.Services.Mapping;

    public class OrderViewModel : IMapFrom<Order>
    {
        public string Id { get; set; }

        public string UserId { get; set; }

        public string User { get; set; }

        public int CreditCardId { get; set; }

        public string CreditCard { get; set; }

        public DateTime PurchaseDate { get; set; }
    }
}
