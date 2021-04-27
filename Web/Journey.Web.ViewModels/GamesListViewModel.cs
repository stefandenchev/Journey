namespace Journey.Web.ViewModels
{
    using System.Collections.Generic;

    public class GamesListViewModel
    {
        public IEnumerable<GameInListViewModel> Games { get; set; }

        public int PageNumber { get; set; }
    }
}
