namespace Journey.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Journey.Data.Common.Repositories;
    using Journey.Data.Models;
    using Journey.Services.Data.Interfaces;
    using Journey.Services.Mapping;
    using Journey.Web.ViewModels.Administration.GameLanguage;

    public class LanguagesService : ILanguagesService
    {
        private readonly IDeletableEntityRepository<Language> languagesRepository;
        private readonly IRepository<GameLanguage> gamesLanguagesRepository;

        public LanguagesService(
            IDeletableEntityRepository<Language> languagesRepository,
            IRepository<GameLanguage> gamesLanguagesRepository)
        {
            this.languagesRepository = languagesRepository;
            this.gamesLanguagesRepository = gamesLanguagesRepository;
        }

        public IEnumerable<KeyValuePair<string, string>> GetAllAsKeyValuePairs()
        {
            return this.languagesRepository.All().Select(x => new
            {
                x.Id,
                x.Name,
            })
            .OrderBy(x => x.Id)
            .ToList().Select(x => new KeyValuePair<string, string>(x.Id.ToString(), x.Name));
        }

        public T GetById<T>(int id)
        {
            var gameLanguage = this.gamesLanguagesRepository
                .AllAsNoTracking()
                .Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefault();

            return gameLanguage;
        }

        public async Task UpdateAsync(int id, GameLanguageAdminInputModel input)
        {
            var gameLanguage = this.gamesLanguagesRepository.All().FirstOrDefault(x => x.Id == id);
            gameLanguage.GameId = input.GameId;
            gameLanguage.LanguageId = input.LanguageId;

            await this.gamesLanguagesRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll<T>()
        {
            return this.gamesLanguagesRepository.All().To<T>().ToList();
        }
    }
}
