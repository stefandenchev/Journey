namespace Journey.Web.ViewModels.Cart
{
    using Journey.Data.Models;
    using Journey.Services.Mapping;

    public class OrderCompleteViewModel : CartViewModel, IMapFrom<Order>
    {
        public int CreditCardId { get; set; }

        public string CreditCardLast4 { get; set; }
    }
}
