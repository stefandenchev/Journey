namespace Journey.Services.Data
{
    using System.Linq;

    using Journey.Data.Common.Repositories;
    using Journey.Data.Models;
    using Journey.Services.Data.Interfaces;

    public class CartService : ICartService
    {
        private readonly IDeletableEntityRepository<UserCartItem> userCartItemsRepository;

        public CartService(IDeletableEntityRepository<UserCartItem> userCartItemsRepository)
        {
            this.userCartItemsRepository = userCartItemsRepository;
        }

        public bool CheckCart(string userId, int gameId)
        {
            var isInCart = this.userCartItemsRepository.All().Any(x => x.UserId == userId && x.GameId == gameId);
            return isInCart;
        }
    }
}
