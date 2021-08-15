namespace Journey.Tests.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Journey.Data.Common.Repositories;
    using Journey.Data.Models;
    using Journey.Services.Data;
    using Journey.Services.Mapping;
    using Journey.Web.ViewModels;
    using Moq;
    using Xunit;

    public class UsersServiceTest
    {
        private readonly Mock<IDeletableEntityRepository<UserImage>> userImagesRepo;
        private readonly UsersService service;
        private readonly List<UserImage> userImages;

        public UsersServiceTest()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            this.userImagesRepo = new Mock<IDeletableEntityRepository<UserImage>>();
            this.userImages = new List<UserImage>();
            this.service = new UsersService(this.userImagesRepo.Object);

            this.userImagesRepo.Setup(x => x.All()).Returns(this.userImages.AsQueryable());
            this.userImagesRepo.Setup(x => x.AddAsync(It.IsAny<UserImage>())).Callback(
                (UserImage item) => this.userImages.Add(item));
            this.userImagesRepo.Setup(x => x.Delete(It.IsAny<UserImage>())).Callback(
                (UserImage item) => this.userImages.Remove(item));
        }

        [Fact]
        public void GetProfilePicturePathShouldReturnCorrectPathIfSuchExists()
        {
            var user = new ClaimsPrincipal(new ClaimsIdentity(
                new Claim[]
                {
                     new Claim(ClaimTypes.NameIdentifier, "TestValue"),
                     new Claim(ClaimTypes.Name, "kal@dunno.com"),
                }));

            this.userImagesRepo.Object.AddAsync(new UserImage
            {
                Id = "image1",
                UserId = "kal@dunno.com",
                Extension = "png",
            });

            var expected = "/images/users/image1.png";

            var result = this.service.GetProfilePicturePath(user.Identity.Name);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void GetProfilePicturePathShouldReturnDefaultImageIfUserDoesNotHaveAnImage()
        {
            var user = new ClaimsPrincipal(new ClaimsIdentity(
                new Claim[]
                {
                     new Claim(ClaimTypes.NameIdentifier, "TestValue"),
                     new Claim(ClaimTypes.Name, "kaladin@dunno.com"),
                }));

            this.userImagesRepo.Object.AddAsync(new UserImage
            {
                Id = "image1",
                UserId = "kal@dunno.com",
                Extension = "png",
            });

            var expected = "/images/users/default-user.png";

            var result = this.service.GetProfilePicturePath(user.Identity.Name);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("Bronze", 5)]
        [InlineData("Silver", 26)]
        [InlineData("Gold", 56)]
        public void GetProfileRankShouldReturnCorrectRank(string rank, int games)
        {
            var result = this.service.GetProfileRank(games);

            Assert.Equal(rank, result);
        }

        [Theory]
        [InlineData("/images/badges/games-1.png", 2)]
        [InlineData("/images/badges/games-5.png", 8)]
        [InlineData("/images/badges/games-10.png", 23)]
        [InlineData("/images/badges/games-25.png", 44)]
        [InlineData("/images/badges/games-50.png", 99)]
        [InlineData("/images/badges/games-100.png", 100)]
        [InlineData("", 0)]
        public void GetProfileBadgeShouldReturnCorrectBadge(string badge, int games)
        {
            var result = this.service.GetProfileBadge(games);

            Assert.Equal(badge, result);
        }
    }
}
