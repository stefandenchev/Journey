namespace Journey.Services.Data
{
    using System.Collections.Generic;

    public interface ITagsService
    {
        IEnumerable<KeyValuePair<string, string>> GetAllAsKeyValuePairs();
    }
}
