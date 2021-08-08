using System.Collections.Generic;

namespace Journey.Web.ViewModels.Administration.GameTags
{
    public class GameTagAdminViewModel
    {
        public IEnumerable<GamesTagsListViewModel> GamesTags { get; set; }
    }
}
