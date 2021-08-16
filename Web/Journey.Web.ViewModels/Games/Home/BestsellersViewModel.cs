namespace Journey.Web.ViewModels.Games.Home
{
    using System.Collections.Generic;

    public class BestsellersViewModel
    {
       public IEnumerable<GameInListViewModel> Games { get; set; }
    }
}
