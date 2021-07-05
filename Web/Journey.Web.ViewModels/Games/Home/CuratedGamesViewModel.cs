namespace Journey.Web.ViewModels.Games.Home
{
    using System.Collections.Generic;

    public class CuratedGamesViewModel
    {
        public IEnumerable<GameInListViewModel> Games { get; set; }
    }
}
