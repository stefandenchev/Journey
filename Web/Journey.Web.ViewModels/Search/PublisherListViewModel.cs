namespace Journey.Web.ViewModels.Search
{
    using System.Collections.Generic;

    public class PublisherListViewModel
    {
        public string PublisherName { get; set; }

        public IEnumerable<GameInListViewModel> Games { get; set; }
    }
}
