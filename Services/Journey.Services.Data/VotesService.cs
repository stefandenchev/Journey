namespace Journey.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using Journey.Data.Common.Repositories;
    using Journey.Data.Models;
    using Journey.Services.Data.Interfaces;

    public class VotesService : IVotesService
    {
        private readonly IRepository<Vote> votesRepository;

        public VotesService(IRepository<Vote> votesRepository)
        {
            this.votesRepository = votesRepository;
        }

        public double GetAverageVotes(int gameId)
        {
            return this.votesRepository.All()
                .Where(x => x.GameId == gameId)
                .Average(x => x.Value);
        }

        public async Task SetVoteAsync(int gameId, string userId, byte value)
        {
            var vote = this.votesRepository.All().FirstOrDefault(
                x => x.GameId == gameId && x.UserId == userId);

            if (vote == null)
            {
                vote = new Vote
                {
                    GameId = gameId,
                    UserId = userId,
                };

                await this.votesRepository.AddAsync(vote);
            }

            vote.Value = value;
            await this.votesRepository.SaveChangesAsync();
        }
    }
}
