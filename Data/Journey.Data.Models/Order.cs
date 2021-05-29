namespace Journey.Data.Models
{
    using System;

    using Journey.Data.Common.Models;

    public class Order : BaseModel<string>
    {
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public int CreditCardId { get; set; }

        public CreditCard CreditCard { get; set; }

        public DateTime PurchaseDate { get; set; }
    }
}
