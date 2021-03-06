 namespace Journey.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using Journey.Data.Common.Models;

    public class Vote : BaseModel<int>
    {
        public int GameId { get; set; }

        public virtual Game Game { get; set; }

        [Required]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public byte Value { get; set; }
    }
}
