namespace Journey.Web.ViewModels.Search
{
    using System.Collections.Generic;

    public class GenreListViewModel
    {
        public string GenreName { get; set; }

        public IEnumerable<GameInListViewModel> Games { get; set; }
    }
}
