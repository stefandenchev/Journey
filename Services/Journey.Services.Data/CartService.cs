namespace Journey.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using Journey.Data.Common.Repositories;
    using Journey.Data.Models;
    using Journey.Services.Data.Interfaces;
    using Journey.Services.Mapping;

    public class CartService : ICartService
    {
        private readonly IDeletableEntityRepository<UserCartItem> userCartItemsRepository;

        public CartService(IDeletableEntityRepository<UserCartItem> userCartItemsRepository)
        {
            this.userCartItemsRepository = userCartItemsRepository;
        }

        public bool IsInCart(string userId, int gameId)
        {
            var isInCart = this.userCartItemsRepository.All().Any(x => x.UserId == userId && x.GameId == gameId);
            return isInCart;
        }

        public T Get<T>(string userId, int gameId)
        {
            var cartItem = this.userCartItemsRepository.AllAsNoTracking()
                .Where(x => x.UserId == userId && x.GameId == gameId)
                .To<T>().FirstOrDefault();

            return cartItem;
        }

        public async Task CreateAsync(string userId, int gameId)
        {
            var cartItem = new UserCartItem
            {
                UserId = userId,
                GameId = gameId,
            };

            await this.userCartItemsRepository.AddAsync(cartItem);
            await this.userCartItemsRepository.SaveChangesAsync();
        }

        public async Task RemoveAsync(string userId, int gameId)
        {
            var cartItem = this.userCartItemsRepository
                .All()
                .FirstOrDefault(x => x.UserId == userId && x.GameId == gameId);

            this.userCartItemsRepository.Delete(cartItem);
            await this.userCartItemsRepository.SaveChangesAsync();
        }

        public int GetCount(string userId)
        {
            return this.userCartItemsRepository
                .All()
                .Where(x => x.UserId == userId)
                .Count();
        }
    }
}
