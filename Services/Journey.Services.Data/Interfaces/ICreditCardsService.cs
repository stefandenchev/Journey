namespace Journey.Services.Data.Interfaces
{
    using System.Collections.Generic;

    public interface ICreditCardsService
    {
        public IEnumerable<T> GetAll<T>();
    }
}
