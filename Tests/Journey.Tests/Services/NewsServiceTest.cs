namespace Journey.Tests.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Journey.Data.Common.Repositories;
    using Journey.Data.Models;
    using Journey.Services.Data;
    using Moq;
    using Xunit;

    public class NewsServiceTest
    {
        private readonly Mock<IDeletableEntityRepository<NewsPost>> newsRepo;
        private readonly NewsService service;
        private readonly List<NewsPost> news;

        public NewsServiceTest()
        {
            this.newsRepo = new Mock<IDeletableEntityRepository<NewsPost>>();
            this.news = new List<NewsPost>();
            this.service = new NewsService(this.newsRepo.Object);

            this.newsRepo.Setup(x => x.All()).Returns(this.news.AsQueryable());
            this.newsRepo.Setup(x => x.AddAsync(It.IsAny<NewsPost>())).Callback(
                (NewsPost item) => this.news.Add(item));
            this.newsRepo.Setup(x => x.Delete(It.IsAny<NewsPost>())).Callback(
                (NewsPost item) => this.news.Remove(item));
        }

        [Fact]
        public async Task VoteAsyncWorksCorrectly()
        {
            var user = new ClaimsPrincipal(new ClaimsIdentity(
                new Claim[]
                {
                     new Claim(ClaimTypes.NameIdentifier, "TestValue"),
                     new Claim(ClaimTypes.Name, "kal@dunno.com"),
                }));

            await this.newsRepo.Object.AddAsync(new NewsPost
            {
                Id = 1,
            });

            await this.newsRepo.Object.AddAsync(new NewsPost
            {
                Id = 2,
            });

            int result = this.service.GetCount();

            Assert.Equal(2, result);
        }
    }
}
