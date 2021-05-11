namespace Journey.Web.ViewModels.Games.Home
{
    using System.Collections.Generic;

    public class LatestReleasesViewModel
    {
        public IEnumerable<GameInListViewModel> Games { get; set; }
    }
}
