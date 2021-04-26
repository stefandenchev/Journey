namespace Journey.Data.Models
{
    using System.Collections.Generic;

    using Journey.Data.Common.Models;

    public class Tag : BaseDeletableModel<int>
    {
        public Tag()
        {
            this.Games = new HashSet<GameTag>();
        }

        public string Name { get; set; }

        public ICollection<GameTag> Games { get; set; }
    }
}
