namespace Journey.Services.Data
{
    using System.Collections.Generic;

    using Journey.Web.ViewModels;

    public interface IGamesService
    {
        IEnumerable<GameInListViewModel> GetAll(int page, int itemsPerPage = 12);
    }
}
