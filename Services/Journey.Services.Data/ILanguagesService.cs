namespace Journey.Services.Data
{
    using System.Collections.Generic;

    public interface ILanguagesService
    {
        public IEnumerable<KeyValuePair<string, string>> GetAllAsKeyValuePairs();
    }
}
