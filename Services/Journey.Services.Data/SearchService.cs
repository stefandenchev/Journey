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
        private readonly IDeletableEntityRepository<Game> gamesRepository;

        public SearchService(IDeletableEntityRepository<Game> gamesRepository)
        {
            this.gamesRepository = gamesRepository;
        }

        public IEnumerable<T> GetAll<T>()
        {
            return this.gamesRepository.All().To<T>().ToList();
        }
    }
}
