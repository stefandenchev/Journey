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

    public class ForumVotesServiceTest
    {
        private readonly Mock<IRepository<ForumVote>> votesRepo;
        private readonly ForumVotesService service;
        private readonly List<ForumVote> votes;

        public ForumVotesServiceTest()
        {
            this.votesRepo = new Mock<IRepository<ForumVote>>();
            this.votes = new List<ForumVote>();
            this.service = new ForumVotesService(this.votesRepo.Object);

            this.votesRepo.Setup(x => x.All()).Returns(this.votes.AsQueryable());
            this.votesRepo.Setup(x => x.AddAsync(It.IsAny<ForumVote>())).Callback(
                (ForumVote item) => this.votes.Add(item));
            this.votesRepo.Setup(x => x.Delete(It.IsAny<ForumVote>())).Callback(
                (ForumVote item) => this.votes.Remove(item));
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

            await this.service.VoteAsync(1, user.Identity.Name, true);

            Assert.Single(this.votes);
        }

        [Fact]
        public async Task GetVotesWorksCorrectly()
        {
            var user = new ClaimsPrincipal(new ClaimsIdentity(
                new Claim[]
                {
                     new Claim(ClaimTypes.NameIdentifier, "TestValue"),
                     new Claim(ClaimTypes.Name, "kal@dunno.com"),
                }));

            var user2 = new ClaimsPrincipal(new ClaimsIdentity(
                new Claim[]
                {
                     new Claim(ClaimTypes.NameIdentifier, "TestValue2"),
                     new Claim(ClaimTypes.Name, "kal2@dunno.com"),
                }));

            var user3 = new ClaimsPrincipal(new ClaimsIdentity(
                new Claim[]
                {
                     new Claim(ClaimTypes.NameIdentifier, "TestValue3"),
                     new Claim(ClaimTypes.Name, "kal3@dunno.com"),
                }));

            var user4 = new ClaimsPrincipal(new ClaimsIdentity(
                new Claim[]
                {
                     new Claim(ClaimTypes.NameIdentifier, "TestValue4"),
                     new Claim(ClaimTypes.Name, "kal4@dunno.com"),
                }));

            await this.service.VoteAsync(1, user.Identity.Name, true);
            await this.service.VoteAsync(1, user2.Identity.Name, true);
            await this.service.VoteAsync(1, user3.Identity.Name, false);
            await this.service.VoteAsync(1, user4.Identity.Name, true);

            Assert.Equal(2, this.service.GetVotes(1));
        }
    }
}
