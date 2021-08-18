namespace Journey.Web.ViewModels.Administration.NewsPosts
{
    using System.Collections.Generic;

    public class NewsPostsListViewModel
    {
        public IEnumerable<NewsPostsInListViewModel> News { get; set; }
    }
}
