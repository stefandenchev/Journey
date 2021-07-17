namespace Journey.Web.ViewModels.Forum
{
    using Journey.Data.Models;
    using Journey.Services.Mapping;

    public class CategoryDropDownViewModel : IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Title { get; set; }
    }
}
