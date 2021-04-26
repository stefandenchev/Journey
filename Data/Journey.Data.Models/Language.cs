namespace Journey.Data.Models
{
    using System.Collections.Generic;

    using Journey.Data.Common.Models;

    public class Language : BaseDeletableModel<int>
    {
        public Language()
        {
            this.Games = new HashSet<GameLanguage>();
        }

        public string Name { get; set; }

        public ICollection<GameLanguage> Games { get; set; }
    }
}
