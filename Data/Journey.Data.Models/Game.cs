namespace Journey.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Game
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int GenreId { get; set; }

        public Genre Genre { get; set; }

        public int PublisherId { get; set; }

        public Publisher Publisher { get; set; }

        public DateTime ReleaseDate { get; set; }

        public decimal Price { get; set; }
    }
}
