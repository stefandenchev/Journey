namespace Journey.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Journey.Services.Data.Interfaces;
    using Journey.Web.ViewModels.Forum.Votes;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    public class TestController : BaseController
    {
        private readonly IForumVotesService forumVotesService;

        public TestController(IForumVotesService forumVotesService)
        {
            this.forumVotesService = forumVotesService;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<ForumVoteResponseModel>> Post(ForumVoteInputModel input)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            await this.forumVotesService.VoteAsync(input.PostId, userId, input.IsUpVote);
            var votes = this.forumVotesService.GetVotes(input.PostId);

            return new ForumVoteResponseModel { VotesCount = votes };
        }
    }
}
