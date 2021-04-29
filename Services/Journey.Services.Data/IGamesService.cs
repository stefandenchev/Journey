namespace Journey.Services.Data
{
    using System.Collections.Generic;

    using Journey.Web.ViewModels;

    public interface IGamesService
    {
        IEnumerable<T> GetAll<T>(int page, int itemsPerPage = 12);

        int GetCount();

        T GetById<T>(int id);
    }
}
