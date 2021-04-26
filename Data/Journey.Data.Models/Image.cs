namespace Journey.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using Journey.Data.Common.Models;

    public class Image : BaseModel<string>
    {
        public Image()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public int GameId { get; set; }

        public virtual Game Game { get; set; }

        public string OriginalUrl { get; set; }
    }
}
