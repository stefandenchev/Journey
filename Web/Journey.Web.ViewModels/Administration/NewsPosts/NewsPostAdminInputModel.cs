namespace Journey.Web.ViewModels.Administration.NewsPosts
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    using Journey.Data.Models;
    using Journey.Services.Mapping;

    public class NewsPostAdminInputModel : IMapFrom<NewsPost>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        [DataType(DataType.Html)]
        public string Content { get; set; }

        [DataType(DataType.Html)]
        public string ShortContent { get; set; }

        [DisplayName("Content Url")]
        public string ImageOrVideoUrl { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
