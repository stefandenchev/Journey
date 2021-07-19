namespace Journey.Data.Models
{
    using System.Collections.Generic;

    using Journey.Data.Common.Models;

    public class ForumPost : BaseDeletableModel<int>
    {
        public ForumPost()
        {
            this.Comments = new HashSet<Comment>();
            this.Votes = new HashSet<ForumVote>();
        }

        public string Title { get; set; }

        public string Content { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<ForumVote> Votes { get; set; }
    }
}
