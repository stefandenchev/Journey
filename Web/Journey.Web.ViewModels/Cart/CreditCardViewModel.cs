namespace Journey.Web.ViewModels.Cart
{
    using Journey.Data.Models;
    using Journey.Services.Mapping;

    public class CreditCardViewModel : IMapFrom<CreditCard>
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public string CardNumber { get; set; }

        public string ExpirationDate { get; set; }
    }
}
