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
    using Journey.Web.ViewModels.Forum.Posts;
    using Moq;
    using Xunit;

    public class PostsServiceTest
    {
        private readonly Mock<IDeletableEntityRepository<ForumPost>> postsRepo;
        private readonly List<ForumPost> postsList;
        private readonly PostsService service;

        public PostsServiceTest()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            this.postsRepo = new Mock<IDeletableEntityRepository<ForumPost>>();
            this.postsList = new List<ForumPost>();
            this.service = new PostsService(this.postsRepo.Object);

            this.postsRepo.Setup(x => x.All()).Returns(this.postsList.AsQueryable());
            this.postsRepo.Setup(x => x.AddAsync(It.IsAny<ForumPost>())).Callback(
                (ForumPost item) => this.postsList.Add(item));
            this.postsRepo.Setup(x => x.Delete(It.IsAny<ForumPost>())).Callback(
                (ForumPost item) => this.postsList.Remove(item));
        }

        [Fact]
        public void GetCountShouldWorkCorrectly()
        {
            this.postsRepo.Object.AddAsync(new ForumPost
            {
                Id = 1,
                Title = "Post Test 1",
                CategoryId = 1,
            });

            this.postsRepo.Object.AddAsync(new ForumPost
            {
                Id = 2,
                Title = "Post Test 2",
                CategoryId = 2,
            });

            this.postsRepo.Object.AddAsync(new ForumPost
            {
                Id = 3,
                Title = "Post Test 3",
                CategoryId = 1,
            });

            var result = this.service.GetCount(2);

            Assert.Equal(1, result);
        }

        [Fact]
        public async Task CreateShouldWorkCorrectly()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            var user = new ClaimsPrincipal(new ClaimsIdentity(
                 new Claim[]
                 {
                     new Claim(ClaimTypes.NameIdentifier, "TestValue"),
                     new Claim(ClaimTypes.Name, "kal@dunno.com"),
                 }));

            await this.service.CreateAsync("TestPost1", "TestContent", 1, user.Identity.Name);
            await this.service.CreateAsync("TestPost2", "TestContent2", 1, user.Identity.Name);

            Assert.Equal(2, this.postsList.Count);
        }
    }
}
