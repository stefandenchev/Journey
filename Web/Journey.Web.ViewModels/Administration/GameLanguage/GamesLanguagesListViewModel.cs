namespace Journey.Web.ViewModels.Administration.GameLanguage
{
    using System;

    using Journey.Data.Models;
    using Journey.Services.Mapping;

    public class GamesLanguagesListViewModel : IMapFrom<GameLanguage>
    {
        public int Id { get; set; }

        public int GameId { get; set; }

        public Game Game { get; set; }

        public int LanguageId { get; set; }

        public Language Language { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
