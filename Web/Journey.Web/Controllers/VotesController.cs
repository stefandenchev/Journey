namespace Journey.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Journey.Services.Data.Interfaces;
    using Journey.Web.ViewModels.Forum.Votes;
    using Journey.Web.ViewModels.Votes;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    public class VotesController : BaseController
    {
        private readonly IVotesService votesService;
        private readonly IForumVotesService forumVotesService;

        public VotesController(IVotesService votesService, IForumVotesService forumVotesService)
        {
            this.votesService = votesService;
            this.forumVotesService = forumVotesService;
        }

        [Authorize]
        [HttpPost]
        [Route("game")]

        public async Task<ActionResult<GameVoteResponseModel>> GamePost(GameVoteInputModel input)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            await this.votesService.SetVoteAsync(input.GameId, userId, input.Value);
            var averageVotes = this.votesService.GetAverageVotes(input.GameId);

            return new GameVoteResponseModel { AverageVote = averageVotes };
        }

        [Authorize]
        [HttpPost]
        [Route("forum")]

        public async Task<ActionResult<ForumVoteResponseModel>> ForumPost(ForumVoteInputModel input)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            await this.forumVotesService.VoteAsync(input.PostId, userId, input.IsUpVote);
            var votes = this.forumVotesService.GetVotes(input.PostId);
            return new ForumVoteResponseModel { VotesCount = votes };
        }
    }
}
