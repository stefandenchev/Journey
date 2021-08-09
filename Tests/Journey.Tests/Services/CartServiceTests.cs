namespace Journey.Tests.Services
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;

    using Journey.Data.Common.Repositories;
    using Journey.Data.Models;
    using Journey.Services.Data;
    using Journey.Web.ViewModels.Games.Create;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Moq;
    using Xunit;

    using static Journey.Tests.Data.Games;
    using static Journey.Tests.Data.User;

    public class CartServiceTests
    {
        private readonly Mock<IDeletableEntityRepository<UserCartItem>> userCartItemsRepo;
        private readonly Mock<IDeletableEntityRepository<Game>> gamesRepo;
        private readonly List<UserCartItem> cartItemsList;
        private readonly CartService service;

        protected Mock<UserManager<ApplicationUser>> UserManager;

        public CartServiceTests()
        {
            this.userCartItemsRepo = new Mock<IDeletableEntityRepository<UserCartItem>>();
            this.gamesRepo = new Mock<IDeletableEntityRepository<Game>>();
            this.cartItemsList = new List<UserCartItem>();
            this.service = new CartService(this.userCartItemsRepo.Object, this.gamesRepo.Object);

            this.userCartItemsRepo.Setup(x => x.All()).Returns(this.cartItemsList.AsQueryable());
            this.userCartItemsRepo.Setup(x => x.AddAsync(It.IsAny<UserCartItem>())).Callback(
                (UserCartItem item) => this.cartItemsList.Add(item));
            this.userCartItemsRepo.Setup(x => x.Delete(It.IsAny<UserCartItem>())).Callback(
                (UserCartItem item) => this.cartItemsList.Remove(item));
            this.userCartItemsRepo.Setup(x => x.HardDelete(It.IsAny<UserCartItem>())).Callback(
                (UserCartItem item) => this.cartItemsList.Remove(item));

            this.UserManager = New;
        }

        [Fact]
        public async Task AddingToCartWorksCorrectly()
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
                await this.service.CreateAsync(user.Identity.Name, game.Id);
            }

            Assert.Equal(10, this.cartItemsList.Count);
        }

        [Fact]
        public async Task RemovingFromCartWorksCorrectly()
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
                await this.service.CreateAsync(user.Identity.Name, game.Id);
            }

            await this.service.RemoveAsync(user.Identity.Name, 3);
            await this.service.RemoveAsync(user.Identity.Name, 6);

            Assert.Equal(8, this.cartItemsList.Count);
        }

        [Fact]
        public async Task ClearAllFromCartWorksCorrectly()
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
                await this.service.CreateAsync(user.Identity.Name, game.Id);
            }

            await this.service.ClearAllAsync(user.Identity.Name);

            Assert.Empty(this.cartItemsList);
        }

        [Fact]
        public async Task GetCountInCartWorksCorrectly()
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
                await this.service.CreateAsync(user.Identity.Name, game.Id);
            }

            await this.service.RemoveAsync(user.Identity.Name, 3);
            await this.service.RemoveAsync(user.Identity.Name, 6);

            Assert.Equal(8, this.service.GetCount(user.Identity.Name));
        }
    }
}
