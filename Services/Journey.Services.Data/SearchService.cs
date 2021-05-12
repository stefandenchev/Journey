namespace Journey.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using Journey.Data.Common.Repositories;
    using Journey.Data.Models;
    using Journey.Services.Data.Interfaces;
    using Journey.Services.Mapping;

    public class SearchService : ISearchService
    {
        private readonly IDeletableEntityRepository<Game> gameRepository;

        public SearchService(IDeletableEntityRepository<Game> gameRepository)
        {
            this.gameRepository = gameRepository;
        }

        public IEnumerable<T> GetAll<T>()
        {
            return this.gameRepository.All().To<T>().ToList();
        }
    }
}
