namespace Journey.Web.ViewModels.Profile
{
    using System.Collections.Generic;

    public class GameLibraryViewModel
    {
        public ICollection<GameInLibraryViewModel> Collection { get; set; }
    }
}
