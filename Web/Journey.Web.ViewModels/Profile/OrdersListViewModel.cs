namespace Journey.Web.ViewModels.Profile
{
    using System.Collections.Generic;

    public class OrdersListViewModel
    {
        public ICollection<ProfileOrderViewModel> Orders { get; set; }

        public decimal Total { get; set; }
    }
}
