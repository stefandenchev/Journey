namespace Journey.Web.ViewModels.News
{
    using System;

    using Journey.Data.Models;
    using Journey.Services.Mapping;

    public class NewsPostViewModel : IMapFrom<NewsPost>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
