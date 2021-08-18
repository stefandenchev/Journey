namespace Journey.Web.ViewModels.Forum.Posts
{
    using System;
    using System.Net;
    using System.Text.RegularExpressions;

    using Journey.Data.Models;
    using Journey.Services.Mapping;

    public class PostInCategoryViewModel : IMapFrom<ForumPost>
    {
        public int Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string ShortContent
        {
            get
            {
                var content = WebUtility.HtmlDecode(Regex.Replace(this.Content, @"<[^>]+>", string.Empty));
                return content.Length > 300
                        ? content.Substring(0, 300) + "..."
                        : content;
            }
        }

        public string UserUserName { get; set; }

        public int CommentsCount { get; set; }

        public int VotesCount { get; set; }
    }
}
