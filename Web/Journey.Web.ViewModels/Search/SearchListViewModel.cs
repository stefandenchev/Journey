namespace Journey.Web.ViewModels.Search
{
    using System.Collections.Generic;

    public class SearchListViewModel : PagingViewModel
    {
        public IEnumerable<GameInListViewModel> Games { get; set; }

        public string Search { get; set; }
    }
}
