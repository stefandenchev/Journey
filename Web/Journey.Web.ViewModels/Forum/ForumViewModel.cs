namespace Journey.Web.ViewModels
{
    using System.Collections.Generic;

    using Journey.Web.ViewModels.Forum;

    public class ForumViewModel
    {
        public IEnumerable<CategoriesViewModel> Categories { get; set; }
    }
}
