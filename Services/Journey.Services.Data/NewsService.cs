namespace Journey.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using Journey.Data.Common.Repositories;
    using Journey.Data.Models;
    using Journey.Services.Data.Interfaces;
    using Journey.Services.Mapping;

    public class NewsService : INewsService
    {
        private readonly IDeletableEntityRepository<NewsPost> newsRepository;

        public NewsService(IDeletableEntityRepository<NewsPost> newsRepository)
        {
            this.newsRepository = newsRepository;
        }

        public IEnumerable<T> GetAll<T>()
        {
            return this.newsRepository.AllAsNoTracking().To<T>().ToList();
        }

        public T GetById<T>(int id)
        {
            var newsPost = this.newsRepository.AllAsNoTracking()
                .Where(x => x.Id == id)
                .To<T>().FirstOrDefault();

            return newsPost;
        }
    }
}
