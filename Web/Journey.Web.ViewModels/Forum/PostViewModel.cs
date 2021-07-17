namespace Journey.Web.ViewModels.Forum
{
    using System;
    using System.Collections.Generic;

    using Ganss.XSS;
    using Journey.Data.Models;
    using Journey.Services.Mapping;

    public class PostViewModel : IMapFrom<ForumPost>, IMapTo<ForumPost>
    {
        public int Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string SanitizedContent => new HtmlSanitizer().Sanitize(this.Content);

        public string UserUserName { get; set; }

        public int VotesCount { get; set; }

        public IEnumerable<ForumPostCommentViewModel> Comments { get; set; }

    }
}
