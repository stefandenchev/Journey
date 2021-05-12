namespace Journey.Services.Data.Interfaces
{
    using System.Collections.Generic;

    public interface ILanguagesService
    {
        IEnumerable<KeyValuePair<string, string>> GetAllAsKeyValuePairs();
    }
}
