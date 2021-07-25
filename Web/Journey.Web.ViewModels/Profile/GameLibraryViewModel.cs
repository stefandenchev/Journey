namespace Journey.Web.ViewModels.Profile
{
    using System.Collections.Generic;

    public class GameLibraryViewModel
    {
        public IEnumerable<GameInLibraryViewModel> Collection { get; set; }
    }
}
