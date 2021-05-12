namespace Journey.Services.Data.Interfaces
{
    using System.Collections.Generic;

    public interface IGenresService
    {
        IEnumerable<KeyValuePair<string, string>> GetAllAsKeyValuePairs();
    }
}
