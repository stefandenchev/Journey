namespace Journey.Web.ViewModels.Administration.GameTags
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Journey.Data.Models;
    using Journey.Services.Mapping;

    public class GameTagAdminInputModel : IMapFrom<GameTag>
    {
        public int Id { get; set; }

        [Display(Name = "Game")]
        public int GameId { get; set; }

        public IEnumerable<KeyValuePair<string, string>> GamesItems { get; set; }

        [Display(Name = "Tag")]
        public int TagId { get; set; }

        public IEnumerable<KeyValuePair<string, string>> TagsItems { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
