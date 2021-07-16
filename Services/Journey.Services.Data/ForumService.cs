namespace Journey.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using Journey.Data.Common.Repositories;
    using Journey.Data.Models;
    using Journey.Services.Data.Interfaces;
    using Journey.Services.Mapping;

    public class ForumService : IForumService
    {
        private readonly IDeletableEntityRepository<ForumPost> forumPostsRepository;

        public ForumService(IDeletableEntityRepository<ForumPost> forumPosts)
        {
            this.forumPostsRepository = forumPosts;
        }

        public IEnumerable<T> GetAll<T>()
        {
            var posts = this.forumPostsRepository.AllAsNoTracking()
                .To<T>()
                .ToList();

            return posts;
        }
    }
}
