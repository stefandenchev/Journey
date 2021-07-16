namespace Journey.Services.Data.Interfaces
{
    using System.Collections.Generic;

    public interface IForumService
    {
        public IEnumerable<T> GetAll<T>();
    }
}
