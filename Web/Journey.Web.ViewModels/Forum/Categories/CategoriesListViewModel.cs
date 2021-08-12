namespace Journey.Web.ViewModels.Forum.Categories
{
    using Journey.Data.Models;
    using Journey.Services.Mapping;

    public class CategoriesListViewModel : IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int ForumPostsCount { get; set; }
    }
}
