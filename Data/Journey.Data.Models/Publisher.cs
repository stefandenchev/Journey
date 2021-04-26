namespace Journey.Data.Models
{
    using System.Collections.Generic;

    using Journey.Data.Common.Models;

    public class Publisher : BaseDeletableModel<int>
    {
        public Publisher()
        {
            this.Games = new HashSet<Game>();
        }

        public string Name { get; set; }

        public ICollection<Game> Games { get; set; }
    }
}
