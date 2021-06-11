namespace Journey.Services.Data.Interfaces
{
    using System.Threading.Tasks;

    public interface IVotesService
    {
        Task SetVoteAsync(int gameId, string userId, byte value);

        double GetAverageVotes(int gameId);
    }
}
