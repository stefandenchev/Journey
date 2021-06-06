namespace Journey.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using Journey.Data.Common.Repositories;
    using Journey.Data.Models;
    using Journey.Services.Data.Interfaces;
    using Journey.Services.Mapping;

    public class PublishersService : IPublishersService
    {
        private readonly IDeletableEntityRepository<Publisher> publishersRepository;

        public PublishersService(IDeletableEntityRepository<Publisher> publishersRepository)
        {
            this.publishersRepository = publishersRepository;
        }

        public IEnumerable<T> GetAll<T>()
        {
            return this.publishersRepository.AllAsNoTracking().To<T>().ToList();
        }

        public IEnumerable<KeyValuePair<string, string>> GetAllAsKeyValuePairs()
        {
            return this.publishersRepository.AllAsNoTracking().Select(x => new
            {
                x.Id,
                x.Name,
            })
            .OrderBy(x => x.Name)
            .ToList().Select(x => new KeyValuePair<string, string>(x.Id.ToString(), x.Name));
        }

        public T GetById<T>(int id)
        {
            var publisher = this.publishersRepository.AllAsNoTracking()
                .Where(x => x.Id == id)
                .To<T>().FirstOrDefault();

            return publisher;
        }
    }
}
