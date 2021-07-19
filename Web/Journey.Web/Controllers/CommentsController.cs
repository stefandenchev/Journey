﻿namespace Journey.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Journey.Services.Data.Interfaces;
    using Journey.Web.ViewModels.Forum.Comments;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class CommentsController : BaseController
    {
        private readonly ICommentsService commentsService;

        public CommentsController(
            ICommentsService commentsService)
        {
            this.commentsService = commentsService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateCommentInputModel input)
        {
            var parentId =
                input.ParentId == 0 ?
                    (int?)null :
                    input.ParentId;

            if (parentId.HasValue)
            {
                if (!this.commentsService.IsInPostId(parentId.Value, input.PostId))
                {
                    return this.BadRequest();
                }
            }

            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            await this.commentsService.Create(input.PostId, userId, input.Content, parentId);
            return this.RedirectToAction("ById", "Posts", new { id = input.PostId });
        }
    }
}