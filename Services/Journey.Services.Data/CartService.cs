namespace Journey.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Journey.Data.Common.Repositories;
    using Journey.Data.Models;
    using Journey.Services.Data.Interfaces;
    using Journey.Services.Mapping;

    public class CartService : ICartService
    {
        private readonly IDeletableEntityRepository<UserCartItem> userCartItemsRepository;
        private readonly IDeletableEntityRepository<Game> gamesRepository;

        public CartService(
            IDeletableEntityRepository<UserCartItem> userCartItemsRepository,
            IDeletableEntityRepository<Game> gamesRepository)
        {
            this.userCartItemsRepository = userCartItemsRepository;
            this.gamesRepository = gamesRepository;
        }

        public bool IsInCart(string userId, int gameId)
        {
            var isInCart = this.userCartItemsRepository.All()
                .Any(x => x.UserId == userId && x.GameId == gameId);
            return isInCart;
        }

        public IEnumerable<T> GetAllInCart<T>(string userId)
        {
            var cart = this.userCartItemsRepository
                .All()
                .Where(x => x.UserId == userId)
                .ToList();

            List<int> ids = new();
            foreach (var game in cart)
            {
                ids.Add(game.GameId);
            }

            var games = this.gamesRepository
                .All()
                .Where(x => ids.Contains(x.Id))
                .To<T>();

            return games;
        }

        public T Get<T>(string userId, int gameId)
        {
            var cartItem = this.userCartItemsRepository.All()
                .Where(x => x.UserId == userId && x.GameId == gameId)
                .To<T>()
                .FirstOrDefault();

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

        public async Task ClearAllAsync(string userId)
        {
            var gamesInCart = this.userCartItemsRepository.All().Where(c => c.UserId == userId);
            foreach (var game in gamesInCart.ToList())
            {
                this.userCartItemsRepository.HardDelete(game);
            }

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
