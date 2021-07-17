namespace Journey.Web.ViewModels.Forum
{
    using System;

    using Ganss.XSS;
    using Journey.Data.Models;
    using Journey.Services.Mapping;

    public class ForumPostCommentViewModel : IMapFrom<Comment>
    {
        public int Id { get; set; }

        public int? ParentId { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Content { get; set; }

        public string SanitizedContent => new HtmlSanitizer().Sanitize(this.Content);

        public string UserUserName { get; set; }
    }
}
