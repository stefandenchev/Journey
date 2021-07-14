namespace Journey.Data.Models
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    using Journey.Data.Common.Models;

    public class NewsPost : BaseDeletableModel<int>
    {
        public string Title { get; set; }

        [DataType(DataType.Html)]
        public string Content { get; set; }

        [DataType(DataType.Html)]
        [DisplayName("Short Content")]
        public string ShortContent { get; set; }

        [DisplayName("Content Url")]
        public string ImageOrVideoUrl { get; set; }
    }
}
