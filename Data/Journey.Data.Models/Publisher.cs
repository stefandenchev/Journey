namespace Journey.Data.Models
{
    using System.Collections.Generic;

    public class Publisher
    {
        public Publisher()
        {
            this.Games = new HashSet<Game>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Game> Games { get; set; }
    }
}
