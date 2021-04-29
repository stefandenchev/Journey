namespace Journey.Web.ViewModels
{
    using System;
    using System.Collections.Generic;

    public class GamesListViewModel : PagingViewModel
    {
        public IEnumerable<GameInListViewModel> Games { get; set; }
    }
}
