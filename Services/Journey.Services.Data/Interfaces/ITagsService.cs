namespace Journey.Services.Data.Interfaces
{
    using System.Collections.Generic;

    public interface ITagsService
    {
        IEnumerable<KeyValuePair<string, string>> GetAllAsKeyValuePairs();
    }
}
