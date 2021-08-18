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

        public T GetById<T>(int id)
        {
            var post = this.postsRepository
                .All()
                .Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefault();

            return post;
        }

        public IEnumerable<T> GetAllInList<T>(int categoryId, int page, int itemsPerPage = 16)
        {
            return this.postsRepository.AllAsNoTracking()
                .Where(x => x.CategoryId == categoryId)
                .OrderByDescending(x => x.Id)
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .To<T>()
                .ToList();
        }

        public int GetCount(int categoryId)
        {
            return this.postsRepository
                .All()
                .Where(x => x.CategoryId == categoryId)
                .Count();
        }
    }
}
