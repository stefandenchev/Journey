namespace Journey.Services.Data.Interfaces
{
    using System.Collections.Generic;

    public interface IPublishersService
    {
        IEnumerable<KeyValuePair<string, string>> GetAllAsKeyValuePairs();
    }
}
