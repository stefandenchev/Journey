using Journey.Data.Common.Models;

namespace Journey.Data.Models
{
    public class GameGenre : BaseModel<int>
    {
        public int GameId { get; set; }

        public virtual Game Game { get; set; }

        public int GenreId { get; set; }

        public virtual Genre Genre { get; set; }
    }
}
