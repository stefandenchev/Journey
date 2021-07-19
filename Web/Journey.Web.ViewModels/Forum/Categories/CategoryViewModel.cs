namespace Journey.Web.ViewModels.Forum.Categories
{
    using System.Collections.Generic;

    using Journey.Data.Models;
    using Journey.Services.Mapping;
    using Journey.Web.ViewModels.Forum.Posts;

    public class CategoryViewModel : PagingViewModel, IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public IEnumerable<PostInCategoryViewModel> ForumPosts { get; set; }
    }
}
