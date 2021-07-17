namespace Journey.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using Journey.Data.Common.Repositories;
    using Journey.Data.Models;
    using Journey.Services.Data.Interfaces;
    using Journey.Services.Mapping;

    public class CategoriesService : ICategoriesService
    {
        private readonly IDeletableEntityRepository<Category> categoriesRepository;

        public CategoriesService(IDeletableEntityRepository<Category> categoriesRepository)
        {
            this.categoriesRepository = categoriesRepository;
        }

        public IEnumerable<T> GetAll<T>()
        {
            var categories = this.categoriesRepository.AllAsNoTracking()
                 .To<T>()
                 .ToList();

            return categories;
        }

        public T GetById<T>(int id)
        {
            var category = this.categoriesRepository.AllAsNoTracking()
                .Where(x => x.Id == id)
                .To<T>().FirstOrDefault();

            return category;
        }

        public T GetByTitle<T>(string title)
        {
            var category = this.categoriesRepository.All()
                .Where(x => x.Title.Replace(" ", "-") == title.Replace(" ", "-"))
                .To<T>().FirstOrDefault();

            return category;
        }
    }
}
