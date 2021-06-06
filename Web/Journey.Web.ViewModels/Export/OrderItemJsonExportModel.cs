namespace Journey.Web.ViewModels.Export
{
    using Journey.Data.Models;
    using Journey.Services.Mapping;
    using Newtonsoft.Json;

    public class OrderItemJsonExportModel : IMapFrom<OrderItem>
    {
        [JsonIgnoreAttribute]
        public string OrderId { get; set; }

        [JsonProperty("Title")]
        public string GameTitle { get; set; }

        public string GameKey { get; set; }
    }
}
