namespace Journey.Data.Models
{
    using Journey.Data.Common.Models;

    public class GameTag
    {
        public int Id { get; set; }

        public int GameId { get; set; }

        public Game Game { get; set; }

        public int TagId { get; set; }

        public Tag Tag { get; set; }
    }
}
