namespace Journey.Services.Data.Interfaces
{
    using System.Collections.Generic;

    public interface ISearchService
    {
        IEnumerable<T> GetAll<T>();
    }
}
