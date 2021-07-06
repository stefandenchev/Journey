namespace Journey.Data.Models
{
    using System;
    using System.Collections.Generic;

    using Journey.Data.Common.Models;

    public class Order : BaseModel<string>
    {
        public Order()
        {
            this.OrderItems = new HashSet<OrderItem>();
        }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public int CreditCardId { get; set; }

        public CreditCard CreditCard { get; set; }

        public DateTime PurchaseDate { get; set; }

        public decimal Total { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
