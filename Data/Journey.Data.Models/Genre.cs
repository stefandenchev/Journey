namespace Journey.Data.Models
{
    using System.Collections.Generic;

    public class Genre
    {
        public Genre()
        {
            this.Games = new HashSet<Game>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Game> Games { get; set; }
    }
}
