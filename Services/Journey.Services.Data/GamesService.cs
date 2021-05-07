namespace Journey.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    using Journey.Data.Common.Repositories;
    using Journey.Data.Models;
    using Journey.Services.Mapping;
    using Journey.Web.ViewModels;

    public class GamesService : IGamesService
    {
        private readonly IDeletableEntityRepository<Game> gamesRepository;

        public GamesService(IDeletableEntityRepository<Game> gamesRepository)
        {
            this.gamesRepository = gamesRepository;
        }

        public IEnumerable<T> GetAll<T>(int page, int itemsPerPage = 12)
        {
            var games = this.gamesRepository.AllAsNoTracking()
                .OrderByDescending(x => x.Id)
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .To<T>()
                .ToList();

            return games;
        }

        public IEnumerable<T> GetAllByPublisher<T>(int page, int publisherId, int itemsPerPage = 12)
        {
            var games = this.gamesRepository.AllAsNoTracking()
                .Where(x => x.Publisher.Id == publisherId)
                .OrderByDescending(x => x.Id)
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .To<T>()
                .ToList();

            return games;
        }

        public T GetById<T>(int id)
        {
            var game = this.gamesRepository.AllAsNoTracking()
                .Where(x => x.Id == id)
                .To<T>().FirstOrDefault();

            return game;
        }

        public int GetCount()
        {
            return this.gamesRepository.All().Count();
        }
    }
}
