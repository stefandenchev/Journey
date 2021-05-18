namespace Journey.Web.ViewModels.Games.Home.ViewComponents
{
    using System.Collections.Generic;

    public class SamePublisherGamesListViewModel
    {
        public string PublisherName { get; set; }

        public IEnumerable<GameInListViewModel> Games { get; set; }
    }
}
