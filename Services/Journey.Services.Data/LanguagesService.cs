namespace Journey.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using Journey.Data.Common.Repositories;
    using Journey.Data.Models;

    public class LanguagesService : ILanguagesService
    {
        private readonly IDeletableEntityRepository<Language> languagesRepository;

        public LanguagesService(IDeletableEntityRepository<Language> languagesRepository)
        {
            this.languagesRepository = languagesRepository;
        }

        public IEnumerable<KeyValuePair<string, string>> GetAllAsKeyValuePairs()
        {
            return this.languagesRepository.AllAsNoTracking().Select(x => new
            {
                x.Id,
                x.Name,
            })
            .OrderBy(x => x.Name)
            .ToList().Select(x => new KeyValuePair<string, string>(x.Id.ToString(), x.Name));
        }
    }
}
