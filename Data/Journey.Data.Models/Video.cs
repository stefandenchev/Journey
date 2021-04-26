namespace Journey.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using Journey.Data.Common.Models;

    public class Video : BaseDeletableModel<int>
    {
        public int GameId { get; set; }

        public virtual Game Game { get; set; }

        public string OriginalUrl { get; set; }
    }
}
