namespace Journey.Services.Data.Interfaces
{
    using System.Collections.Generic;

    public interface INewsService
    {
        T GetById<T>(int id);

        public IEnumerable<T> GetAll<T>();
    }
}
