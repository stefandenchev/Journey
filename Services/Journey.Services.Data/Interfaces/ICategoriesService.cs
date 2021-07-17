namespace Journey.Services.Data.Interfaces
{
    using System.Collections.Generic;

    public interface ICategoriesService
    {
        IEnumerable<T> GetAll<T>();

        T GetById<T>(int id);

        T GetByTitle<T>(string title);
    }
}
