namespace Journey.Web.ViewModels.Administration.GameTags
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Journey.Data.Models;
    using Journey.Services.Mapping;

    public class GameTagAdminInputModel : IMapFrom<GameTag>
    {
        public int Id { get; set; }

        public int GameId { get; set; }

        [Display(Name = "Games")]
        public IEnumerable<KeyValuePair<string, string>> GamesItems { get; set; }

        public int TagId { get; set; }

        [Display(Name = "Tags")]
        public IEnumerable<KeyValuePair<string, string>> TagsItems { get; set; }
    }
}
