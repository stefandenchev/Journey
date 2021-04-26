namespace Journey.Web.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class GameViewModel
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime ReleaseDate { get; set; }

        public decimal Price { get; set; }
    }
}
