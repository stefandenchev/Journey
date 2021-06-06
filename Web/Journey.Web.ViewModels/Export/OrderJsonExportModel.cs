namespace Journey.Web.ViewModels.Export
{
    using System;
    using System.Collections.Generic;

    using Journey.Data.Models;
    using Journey.Services.Mapping;
    using Newtonsoft.Json;

    public class OrderJsonExportModel : IMapFrom<Order>
    {
        [JsonIgnoreAttribute]
        public string Id { get; set; }

        [JsonProperty("Credit Card")]
        public string CreditCardCardNumber { get; set; }

        [JsonConverter(typeof(DateFormatConverter), "dd-MM-yyyy")]
        public DateTime PurchaseDate { get; set; }

        [JsonProperty("Games")]
        public IEnumerable<OrderItemJsonExportModel> OrderItems { get; set; }

        public decimal Total { get; set; }
    }
}
