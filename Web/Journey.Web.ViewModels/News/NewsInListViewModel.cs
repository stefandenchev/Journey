namespace Journey.Web.ViewModels.News
{
    using System;

    using AutoMapper;
    using Journey.Data.Models;
    using Journey.Services.Mapping;

    public class NewsInListViewModel : IMapFrom<NewsPost>, IHaveCustomMappings
    {
/*        private readonly INewsUrlGenerator urlGenerator;

        public NewsInListViewModel()
            : this(new INewsUrlGenerator())
        {
        }

        public NewsInListViewModel(INewsUrlGenerator urlGenerator)
        {
            this.urlGenerator = urlGenerator;
        }*/

        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }

        public string ImageOrVideoUrl { get; set; }

/*        public string Url => this.urlGenerator.GenerateUrl(this.Id, this.Title, this.CreatedOn);
*/
        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<NewsPost, NewsInListViewModel>().ForMember(
                m => m.Content,
                opt => opt.MapFrom(u => u.ShortContent));
        }
    }
}