namespace Journey.Services.Data.Interfaces
{
    using System.Collections.Generic;

    public interface ICategoriesService
    {
        IEnumerable<T> GetAll<T>();
    }
}
