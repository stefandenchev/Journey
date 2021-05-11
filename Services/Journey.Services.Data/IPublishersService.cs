namespace Journey.Services.Data
{
    using System.Collections.Generic;

    public interface IPublishersService
    {
        IEnumerable<KeyValuePair<string, string>> GetAllAsKeyValuePairs();
    }
}
