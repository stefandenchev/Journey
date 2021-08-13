namespace Journey.Web.ViewModels.Forum.Posts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;
    using Ganss.XSS;
    using Journey.Data.Models;
    using Journey.Services.Mapping;
    using Journey.Web.ViewModels.Profile;

    public class PostViewModel : IMapFrom<ForumPost>, IMapTo<ForumPost>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string SanitizedContent => new HtmlSanitizer().Sanitize(this.Content);

        public string UserId { get; set; }

        public string UserUserName { get; set; }

        public ProfilePictureViewModel UserProfilePicture { get; set; }

        public int VotesCount { get; set; }

        public IEnumerable<ForumPostCommentViewModel> Comments { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<ForumPost, PostViewModel>()
                .ForMember(x => x.VotesCount, options =>
                {
                    options.MapFrom(p => p.Votes.Sum(v => (int)v.Type));
                });
        }
    }
}
