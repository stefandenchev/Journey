namespace Journey.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Journey.Services.Data.Interfaces;
    using Journey.Web.ViewModels.Votes;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    public class VotesController : BaseController
    {
        private readonly IVotesService votesService;

        public VotesController(IVotesService votesService)
        {
            this.votesService = votesService;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<PostVoteResponseModel>> Post(PostVoteViewModel input)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            await this.votesService.SetVoteAsync(input.GameId, userId, input.Value);
            var averageVotes = this.votesService.GetAverageVotes(input.GameId);

            return new PostVoteResponseModel { AverageVote = averageVotes };
        }
    }
}
