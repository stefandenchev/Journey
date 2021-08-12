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
            return this.newsRepository
                .All()
                .To<T>()
                .ToList();
        }

        public IEnumerable<T> GetAllInList<T>(int page, int itemsPerPage = 6)
        {
            var news = this.newsRepository.AllAsNoTracking()
                .OrderByDescending(x => x.Id)
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .To<T>()
                .ToList();

            return news;
        }

        public T GetById<T>(int id)
        {
            var newsPost = this.newsRepository.AllAsNoTracking()
                .Where(x => x.Id == id)
                .To<T>().FirstOrDefault();

            return newsPost;
        }

        public int GetCount()
        {
            return this.newsRepository.All().Count();
        }
    }
}
