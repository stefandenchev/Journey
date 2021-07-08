namespace Journey.Web.ViewModels.Administration.NewsPosts
{
    using System;

    using Journey.Data.Models;
    using Journey.Services.Mapping;

    public class NewsPostsInListViewModel : IMapFrom<NewsPost>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
