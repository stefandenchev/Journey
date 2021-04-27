namespace Journey.Data.Models
{
    using Journey.Data.Common.Models;

    public class GameTag : BaseModel<int>
    {
        public int GameId { get; set; }

        public Game Game { get; set; }

        public int TagId { get; set; }

        public Tag Tag { get; set; }
    }
}
