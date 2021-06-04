namespace Journey.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Journey.Data.Common.Repositories;
    using Journey.Data.Models;
    using Journey.Services.Data.Interfaces;
    using Journey.Services.Mapping;
    using Journey.Web.ViewModels.Administration.GameTags;

    public class TagsService : ITagsService
    {
        private readonly IDeletableEntityRepository<Tag> tagsRepository;
        private readonly IRepository<GameTag> gamesTagsRepository;

        public TagsService(
            IDeletableEntityRepository<Tag> tagsRepository,
            IRepository<GameTag> gamesTagsRepository)
        {
            this.tagsRepository = tagsRepository;
            this.gamesTagsRepository = gamesTagsRepository;
        }

        public IEnumerable<KeyValuePair<string, string>> GetAllAsKeyValuePairs()
        {
            return this.tagsRepository.AllAsNoTracking().Select(x => new
            {
                x.Id,
                x.Name,
            }).ToList().Select(x => new KeyValuePair<string, string>(x.Id.ToString(), x.Name));
        }

        public IEnumerable<T> GetAll<T>()
        {
            return this.gamesTagsRepository.All().To<T>().ToList();
        }

        public T GetById<T>(int id)
        {
            var gameTag = this.gamesTagsRepository.AllAsNoTracking()
                .Where(x => x.Id == id)
                .To<T>().FirstOrDefault();

            return gameTag;
        }

        public async Task UpdateAsync(int id, GameTagAdminInputModel input)
        {
            var gameTag = this.gamesTagsRepository.All().FirstOrDefault(x => x.Id == id);
            gameTag.GameId = input.GameId;
            gameTag.TagId = input.TagId;

            await this.gamesTagsRepository.SaveChangesAsync();
        }
    }
}
