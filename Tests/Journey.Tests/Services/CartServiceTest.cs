namespace Journey.Tests.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Security.Claims;
    using System.Threading;
    using System.Threading.Tasks;

    using Journey.Data.Common.Repositories;
    using Journey.Data.Models;
    using Journey.Services.Data;
    using Journey.Services.Mapping;
    using Journey.Web.ViewModels;
    using Journey.Web.ViewModels.Cart;
    using Moq;
    using Xunit;

    using static Journey.Tests.Data.Games;

    public class CartServiceTest
    {
        private readonly Mock<IDeletableEntityRepository<UserCartItem>> userCartItemsRepo;
        private readonly List<UserCartItem> cartItemsList;
        private readonly CartService cartService;

        private readonly Mock<IDeletableEntityRepository<Game>> gamesRepo;
        private readonly Mock<IDeletableEntityRepository<Language>> languagesRepo;
        private readonly Mock<IDeletableEntityRepository<Tag>> tagsRepo;
        private readonly List<Game> gamesList;
        private readonly GamesService gamesService;

        public CartServiceTest()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            this.userCartItemsRepo = new Mock<IDeletableEntityRepository<UserCartItem>>();
            this.gamesRepo = new Mock<IDeletableEntityRepository<Game>>();
            this.cartItemsList = new List<UserCartItem>();
            this.cartService = new CartService(this.userCartItemsRepo.Object, this.gamesRepo.Object);

            this.userCartItemsRepo.Setup(x => x.All()).Returns(this.cartItemsList.AsQueryable());
            this.userCartItemsRepo.Setup(x => x.AddAsync(It.IsAny<UserCartItem>())).Callback(
                (UserCartItem item) => this.cartItemsList.Add(item));
            this.userCartItemsRepo.Setup(x => x.Delete(It.IsAny<UserCartItem>())).Callback(
                (UserCartItem item) => this.cartItemsList.Remove(item));
            this.userCartItemsRepo.Setup(x => x.HardDelete(It.IsAny<UserCartItem>())).Callback(
                (UserCartItem item) => this.cartItemsList.Remove(item));

            this.gamesRepo = new Mock<IDeletableEntityRepository<Game>>();
            this.languagesRepo = new Mock<IDeletableEntityRepository<Language>>();
            this.tagsRepo = new Mock<IDeletableEntityRepository<Tag>>();

            this.gamesList = new List<Game>();
            this.gamesService = new GamesService(this.gamesRepo.Object, this.languagesRepo.Object, this.tagsRepo.Object);

            this.gamesRepo.Setup(x => x.All()).Returns(this.gamesList.AsQueryable());
            this.gamesRepo.Setup(x => x.AllAsNoTracking()).Returns(this.gamesList.AsQueryable());
            this.gamesRepo.Setup(x => x.AddAsync(It.IsAny<Game>())).Callback(
                (Game game) => this.gamesList.Add(game));
        }

        [Fact]
        public async Task AddingToCartShouldWorkCorrectly()
        {
            var user = new ClaimsPrincipal(new ClaimsIdentity(
                new Claim[]
                {
                     new Claim(ClaimTypes.NameIdentifier, "TestValue"),
                     new Claim(ClaimTypes.Name, "kal@dunno.com"),
                }));

            var games = TenGames;

            foreach (var game in games)
            {
                await this.cartService.CreateAsync(user.Identity.Name, game.Id);
            }

            Assert.Equal(10, this.cartItemsList.Count);
        }

        [Fact]
        public async Task RemovingFromCartShouldWorkCorrectly()
        {
            var user = new ClaimsPrincipal(new ClaimsIdentity(
                new Claim[]
                {
                     new Claim(ClaimTypes.NameIdentifier, "TestValue"),
                     new Claim(ClaimTypes.Name, "kal@dunno.com"),
                }));

            var games = TenGames;

            foreach (var game in games)
            {
                await this.cartService.CreateAsync(user.Identity.Name, game.Id);
            }

            await this.cartService.RemoveAsync(user.Identity.Name, 3);
            await this.cartService.RemoveAsync(user.Identity.Name, 6);

            Assert.Equal(8, this.cartItemsList.Count);
        }

        [Fact]
        public async Task ClearAllFromCartShouldWorkCorrectly()
        {
            var user = new ClaimsPrincipal(new ClaimsIdentity(
                new Claim[]
                {
                     new Claim(ClaimTypes.NameIdentifier, "TestValue"),
                     new Claim(ClaimTypes.Name, "kal@dunno.com"),
                }));

            var games = TenGames;

            foreach (var game in games)
            {
                await this.cartService.CreateAsync(user.Identity.Name, game.Id);
            }

            await this.cartService.ClearAllAsync(user.Identity.Name);

            Assert.Empty(this.cartItemsList);
        }

        [Fact]
        public async Task GetCountInCartShouldWorkCorrectly()
        {
            var user = new ClaimsPrincipal(new ClaimsIdentity(
                new Claim[]
                {
                     new Claim(ClaimTypes.NameIdentifier, "TestValue"),
                     new Claim(ClaimTypes.Name, "kal@dunno.com"),
                }));

            var games = TenGames;

            foreach (var game in games)
            {
                await this.cartService.CreateAsync(user.Identity.Name, game.Id);
            }

            await this.cartService.RemoveAsync(user.Identity.Name, 3);
            await this.cartService.RemoveAsync(user.Identity.Name, 6);

            Assert.Equal(8, this.cartService.GetCount(user.Identity.Name));
        }

        [Fact]
        public async Task IsInCartShouldWorkCorrectly()
        {
            var user = new ClaimsPrincipal(new ClaimsIdentity(
                new Claim[]
                {
                     new Claim(ClaimTypes.NameIdentifier, "TestValue"),
                     new Claim(ClaimTypes.Name, "kal@dunno.com"),
                }));

            var game = OneGame;

            await this.cartService.CreateAsync(user.Identity.Name, game.Id);

            Assert.True(this.cartService.IsInCart(user.Identity.Name, game.Id));
        }

        [Fact]
        public async Task GetItemFromCartShouldWorkCorrectly()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            var user = new ClaimsPrincipal(new ClaimsIdentity(
                new Claim[]
                {
                     new Claim(ClaimTypes.NameIdentifier, "TestValue"),
                     new Claim(ClaimTypes.Name, "kal@dunno.com"),
                }));

            var game = OneGame;

            await this.cartService.CreateAsync(user.Identity.Name, game.Id);

            Assert.NotNull(this.cartService.Get<CartItemViewModel>(user.Identity.Name, game.Id));
            Assert.Equal(1, this.cartService.Get<CartItemViewModel>(user.Identity.Name, game.Id).GameId);
        }
    }
}
