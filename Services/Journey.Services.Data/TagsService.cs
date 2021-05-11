﻿namespace Journey.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using Journey.Data.Common.Repositories;
    using Journey.Data.Models;

    public class TagsService : ITagsService
    {
        private readonly IDeletableEntityRepository<Tag> tagsRepository;

        public TagsService(IDeletableEntityRepository<Tag> tagsRepository)
        {
            this.tagsRepository = tagsRepository;
        }

        public IEnumerable<KeyValuePair<string, string>> GetAllAsKeyValuePairs()
        {
            return this.tagsRepository.AllAsNoTracking().Select(x => new
            {
                x.Id,
                x.Name,
            }).ToList().Select(x => new KeyValuePair<string, string>(x.Id.ToString(), x.Name));
        }
    }
}