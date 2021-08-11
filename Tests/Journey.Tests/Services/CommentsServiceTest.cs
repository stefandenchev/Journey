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

    public class CommentsServiceTest
    {
        private readonly Mock<IDeletableEntityRepository<Comment>> commentsRepository;
        private readonly CommentsService service;
        private readonly List<Comment> comments;

        public CommentsServiceTest()
        {
            this.commentsRepository = new Mock<IDeletableEntityRepository<Comment>>();
            this.comments = new List<Comment>();
            this.service = new CommentsService(this.commentsRepository.Object);

            this.commentsRepository.Setup(x => x.All()).Returns(this.comments.AsQueryable());

            this.commentsRepository.Setup(x => x.AddAsync(It.IsAny<Comment>())).Callback(
                (Comment item) => this.comments.Add(item));
            this.commentsRepository.Setup(x => x.Delete(It.IsAny<Comment>())).Callback(
                (Comment item) => this.comments.Remove(item));
        }

        [Fact]
        public async Task CreatingCommentWorksCorrectly()
        {
            var user = new ClaimsPrincipal(new ClaimsIdentity(
                new Claim[]
                {
                     new Claim(ClaimTypes.NameIdentifier, "TestValue"),
                     new Claim(ClaimTypes.Name, "kal@dunno.com"),
                }));

            await this.service.Create(1, user.Identity.Name, "YAAA");

            Assert.Single(this.comments);
        }

        [Fact]
        public async Task IsInPostIdReturnsTrueWhenCorrect()
        {
            var user = new ClaimsPrincipal(new ClaimsIdentity(
                new Claim[]
                {
                     new Claim(ClaimTypes.NameIdentifier, "TestValue"),
                     new Claim(ClaimTypes.Name, "kal@dunno.com"),
                }));

            await this.commentsRepository.Object.AddAsync(new Comment
            {
                Content = "hi",
                ForumPostId = 1,
                UserId = user.Identity.Name,
            });

            var comment = this.comments[0];

            Assert.True(this.service.IsInPostId(comment.Id, 1));
        }

        [Fact]
        public async Task IsInPostIdReturnsFalseWhenWrong()
        {
            var user = new ClaimsPrincipal(new ClaimsIdentity(
                new Claim[]
                {
                     new Claim(ClaimTypes.NameIdentifier, "TestValue"),
                     new Claim(ClaimTypes.Name, "kal@dunno.com"),
                }));

            await this.commentsRepository.Object.AddAsync(new Comment
            {
                Content = "hi",
                ForumPostId = 1,
                UserId = user.Identity.Name,
            });

            var comment = this.comments[0];

            Assert.False(this.service.IsInPostId(comment.Id, 2));
        }
    }
}
