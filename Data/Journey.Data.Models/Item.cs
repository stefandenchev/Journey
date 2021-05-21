using Journey.Data.Common.Models;

namespace Journey.Data.Models
{
    public class Item : BaseDeletableModel<int>
    {
        public int GameId { get; set; }

        public Game Game { get; set; }
    }
}
