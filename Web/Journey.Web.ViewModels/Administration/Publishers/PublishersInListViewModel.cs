namespace Journey.Web.ViewModels.Administration.Publishers
{
    using System;

    using Journey.Data.Models;
    using Journey.Services.Mapping;

    public class PublishersInListViewModel : IMapFrom<Publisher>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime CreatedOn_17114092 { get; set; }

        public DateTime? ModifiedOn_17114092 { get; set; }
    }
}
