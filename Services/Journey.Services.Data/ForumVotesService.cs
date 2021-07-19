namespace Journey.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using Journey.Data.Common.Repositories;
    using Journey.Data.Models;
    using Journey.Services.Data.Interfaces;

    public class ForumVotesService : IForumVotesService
    {
        private readonly IRepository<ForumVote> votesRepository;

        public ForumVotesService(IRepository<ForumVote> votesRepository)
        {
            this.votesRepository = votesRepository;
        }

        public int GetVotes(int postId)
        {
            var votes = this.votesRepository.All()
                .Where(x => x.PostId == postId).Sum(x => (int)x.Type);

            return votes;
        }

        public async Task VoteAsync(int postId, string userId, bool isUpVote)
        {
            var vote = this.votesRepository.All()
                .FirstOrDefault(x => x.PostId == postId && x.UserId == userId);
            if (vote != null)
            {
                vote.Type = isUpVote ? VoteType.UpVote : VoteType.DownVote;
            }
            else
            {
                vote = new ForumVote
                {
                    PostId = postId,
                    UserId = userId,
                    Type = isUpVote ? VoteType.UpVote : VoteType.DownVote,
                };

                await this.votesRepository.AddAsync(vote);
            }

            await this.votesRepository.SaveChangesAsync();
        }
    }
}
