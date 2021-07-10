namespace Journey.Web.ViewModels.News
{
    using System;

    using AutoMapper;
    using Journey.Data.Models;
    using Journey.Services.Mapping;

    public class NewsInListViewModel : IMapFrom<NewsPost>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }

        public string ImageOrVideoUrl { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<NewsPost, NewsInListViewModel>().ForMember(
                m => m.Content,
                opt => opt.MapFrom(u => u.ShortContent));
        }
    }
}