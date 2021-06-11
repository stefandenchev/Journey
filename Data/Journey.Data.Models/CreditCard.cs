namespace Journey.Data.Models
{
    using Journey.Data.Common.Models;

    public class CreditCard : BaseDeletableModel<int>
    {
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public string CardNumber { get; set; }

        public string ExpirationDate { get; set; }
    }
}
