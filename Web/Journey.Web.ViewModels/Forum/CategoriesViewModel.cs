namespace Journey.Web.ViewModels.Forum
{
    using Journey.Data.Models;
    using Journey.Services.Mapping;

    public class CategoriesViewModel : IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int ForumPostsCount { get; set; }

        public string Url => $"/f/{this.Title.Replace(' ', '-')}";
    }
}
