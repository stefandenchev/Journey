﻿namespace Journey.Web.ViewModels.News
{
    using System.Collections.Generic;

    public class NewsListViewModel : PagingViewModel
    {
        public IEnumerable<NewsInListViewModel> News { get; set; }
    }
}
