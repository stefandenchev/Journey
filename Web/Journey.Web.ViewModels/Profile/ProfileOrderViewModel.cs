namespace Journey.Web.ViewModels.Profile
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Journey.Data.Models;
    using Journey.Services.Mapping;
    using Journey.Web.ViewModels.Games;

    public class ProfileOrderViewModel : IMapFrom<Order>
    {
        public string Id { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{dd, MMM, yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Order Placed")]
        public DateTime PurchaseDate { get; set; }

        [Display(Name = "Items")]
        public int OrderItemsCount { get; set; }

        [DataType(DataType.Currency)]
        public decimal Total { get; set; }

        public IEnumerable<GameThumbViewModel> Games { get; set; }
    }
}
