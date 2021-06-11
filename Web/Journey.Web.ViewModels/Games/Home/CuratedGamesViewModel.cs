using System;
using System.Collections.Generic;
using System.Text;

namespace Journey.Web.ViewModels.Games.Home
{
    public class CuratedGamesViewModel
    {
        public IEnumerable<GameInListViewModel> Games { get; set; }
    }
}
