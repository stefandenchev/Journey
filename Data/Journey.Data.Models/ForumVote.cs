namespace Journey.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using Journey.Data.Common.Models;

    public class ForumVote : BaseModel<int>
    {
        public int PostId { get; set; }

        public virtual ForumPost Post { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public VoteType Type { get; set; }
    }
}
