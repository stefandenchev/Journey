namespace Journey.Web.ViewModels.Cart
{
    using Journey.Data.Models;
    using Journey.Services.Mapping;

    public class CartItemInputModel : IMapFrom<UserCartItem>
    {
        public string UserId { get; set; }

        public int GameId { get; set; }
    }
}
