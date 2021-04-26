namespace Journey.Data.Models
{
    using Journey.Data.Common.Models;

    public class GameLanguage
    {
        public int Id { get; set; }

        public int GameId { get; set; }

        public virtual Game Game { get; set; }

        public int LanguageId { get; set; }

        public virtual Language Language { get; set; }
    }
}
