namespace Journey.Web.ViewModels.Administration.Publishers
{
    using System;

    using Journey.Data.Models;
    using Journey.Services.Mapping;

    public class PublishersInListViewModel : IMapFrom<Publisher>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
