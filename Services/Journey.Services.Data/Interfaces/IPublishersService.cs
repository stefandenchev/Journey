namespace Journey.Services.Data.Interfaces
{
    using System.Collections.Generic;

    public interface IPublishersService
    {
        IEnumerable<KeyValuePair<string, string>> GetAllAsKeyValuePairs();

        T GetById<T>(int id);

        public IEnumerable<T> GetAll<T>();
    }
}
