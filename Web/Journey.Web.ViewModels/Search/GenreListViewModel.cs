namespace Journey.Web.ViewModels.Search
{
    using AutoMapper;
    using Journey.Data.Models;
    using Journey.Services.Mapping;
    using System.Collections.Generic;
    using System.Linq;

    public class GenreListViewModel : IMapFrom<Game>
    {
        public string GenreName { get; set; }

        public IEnumerable<GameInListViewModel> Games { get; set; }

    }
}
