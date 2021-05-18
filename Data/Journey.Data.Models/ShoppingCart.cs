namespace Journey.Data.Models
{
    using System;
    using System.Collections.Generic;

    using Journey.Data.Common.Models;

    public class ShoppingCart : BaseModel<string>
    {
        public ShoppingCart()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Games = new HashSet<Game>();
        }

        public ICollection<Game> Games { get; set; }
    }
}
