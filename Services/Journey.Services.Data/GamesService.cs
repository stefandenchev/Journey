namespace Journey.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using Journey.Data.Common.Repositories;
    using Journey.Data.Models;
    using Journey.Web.ViewModels;

    public class GamesService : IGamesService
    {
        private readonly IDeletableEntityRepository<Game> gamesRepository;

        public GamesService(IDeletableEntityRepository<Game> gamesRepository)
        {
            this.gamesRepository = gamesRepository;
        }

        public IEnumerable<GameInListViewModel> GetAll(int page, int itemsPerPage = 12)
        {
            var games = this.gamesRepository.AllAsNoTracking()
                .OrderByDescending(x => x.Id)
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .Select(x => new GameInListViewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Price = x.Price,
                    ImageUrl = x.Images.FirstOrDefault().OriginalUrl,
                }).ToList();

            return games;
        }
    }
}
