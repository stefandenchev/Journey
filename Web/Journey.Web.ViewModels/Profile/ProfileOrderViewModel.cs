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
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MMMM dd, yyyy, h:mm tt}")]
        [Display(Name = "Order Placed")]
        public DateTime OrderPlaced { get; set; }

        [Display(Name = "Items")]
        public int ItemNumber { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Total")]
        public decimal Total { get; set; }

        public List<GameThumbViewModel> Games { get; set; }
    }
}
