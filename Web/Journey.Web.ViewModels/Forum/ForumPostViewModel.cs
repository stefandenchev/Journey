namespace Journey.Web.ViewModels.Forum
{
    using Journey.Data.Models;
    using Journey.Services.Mapping;

    public class ForumPostViewModel : IMapFrom<ForumPost>
    {
        public string Title { get; set; }

        public string Description { get; set; }
    }
}
