namespace Journey.Web.ViewModels.Administration.GameTags
{
    using System;

    using Journey.Data.Models;
    using Journey.Services.Mapping;

    public class GamesTagsListViewModel : IMapFrom<GameTag>
    {
        public int Id { get; set; }

        public int GameId { get; set; }

        public Game Game { get; set; }

        public int TagId { get; set; }

        public Tag Tag { get; set; }

        public DateTime CreatedOn_17114092 { get; set; }

        public DateTime? ModifiedOn_17114092 { get; set; }
    }
}
