namespace Journey.Web.ViewModels.Administration.GameLanguage
{
    using System;

    using Journey.Data.Models;
    using Journey.Services.Mapping;

    public class GamesLanguagesListViewModel : IMapFrom<Journey.Data.Models.GameLanguage>
    {
        public int Id { get; set; }

        public int GameId { get; set; }

        public Game Game { get; set; }

        public int LanguageId { get; set; }

        public Language Language { get; set; }

        public DateTime CreatedOn_17114092 { get; set; }

        public DateTime? ModifiedOn_17114092 { get; set; }
    }
}
