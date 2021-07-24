namespace Journey.Web.ViewModels.Cart
{
    using Journey.Data.Models;
    using Journey.Services.Mapping;

    public class CartItemViewModel : IMapFrom<UserCartItem>
    {
        public string UserId { get; set; }

        public int GameId { get; set; }
    }
}
