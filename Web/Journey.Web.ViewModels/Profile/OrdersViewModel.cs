namespace Journey.Web.ViewModels.Profile
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Journey.Data.Models;
    using Journey.Services.Mapping;

    public class OrdersViewModel : IMapFrom<Order>
    {
        public string Id { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MMMM dd, yyyy, h:mm tt}")]
        [Display(Name = "Order Placed")]
        public DateTime OrderPlaced { get; set; }

        [Display(Name = "Items")]
        public int ItemNumber { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Total")]
        public decimal Total { get; set; }

        public List<GameInListViewModel> Games { get; set; }
    }
}
