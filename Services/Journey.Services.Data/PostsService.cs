namespace Journey.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Journey.Data.Common.Repositories;
    using Journey.Data.Models;
    using Journey.Services.Data.Interfaces;
    using Journey.Services.Mapping;

    public class PostsService : IPostsService
    {
        private readonly IDeletableEntityRepository<ForumPost> postsRepository;

        public PostsService(IDeletableEntityRepository<ForumPost> postsRepository)
        {
            this.postsRepository = postsRepository;
        }

        public async Task<int> CreateAsync(string title, string content, int categoryId, string userId)
        {
            var post = new ForumPost
            {
                CategoryId = categoryId,
                Content = content,
                Title = title,
                UserId = userId,
            };

            await this.postsRepository.AddAsync(post);
            await this.postsRepository.SaveChangesAsync();
            return post.Id;
        }

        public IEnumerable<T> GetByCategoryId<T>(int categoryId, int? take = null, int skip = 0)
        {
            var query = this.postsRepository.All()
                .OrderByDescending(x => x.CreatedOn)
                .Where(x => x.CategoryId == categoryId).Skip(skip);
            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return query.To<T>().ToList();
        }

        public T GetById<T>(int id)
        {
            var post = this.postsRepository.All().Where(x => x.Id == id)
                .To<T>().FirstOrDefault();
            return post;
        }

        public int GetCountByCategoryId(int categoryId)
        {
            return this.postsRepository.All().Count(x => x.CategoryId == categoryId);
        }

        public IEnumerable<T> GetAllInList<T>(int categoryId, int page, int itemsPerPage = 16)
        {
            var games = this.postsRepository.AllAsNoTracking()
                .Where(x => x.CategoryId == categoryId)
                .OrderByDescending(x => x.Id)
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .To<T>()
                .ToList();

            return games;
        }

        public int GetCount(int categoryId)
        {
            return this.postsRepository.All().Where(x => x.CategoryId == categoryId).Count();
        }
    }
}
