using System;
using System.Collections.Generic;
using System.Text;

namespace Journey.Web.ViewModels.Administration.GameTags
{
    public class GameTagAdminViewModel
    {
        public IEnumerable<GamesTagsListViewModel> GamesTags { get; set; }
    }
}
