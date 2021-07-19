namespace Journey.Services.Data.Interfaces
{
    using System.Threading.Tasks;

    public interface IForumVotesService
    {
        Task VoteAsync(int postId, string userId, bool isUpVote);

        int GetVotes(int postId);
    }
}
