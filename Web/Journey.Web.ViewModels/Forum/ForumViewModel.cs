namespace Journey.Web.ViewModels.Forum
{
    using System.Collections.Generic;

    public class ForumViewModel
    {
        public IEnumerable<CategoriesViewModel> Categories { get; set; }
    }
}
