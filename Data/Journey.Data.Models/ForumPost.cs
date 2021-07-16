namespace Journey.Data.Models
{
    using Journey.Data.Common.Models;

    public class ForumPost : BaseDeletableModel<int>
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public int? GameId { get; set; }

        public virtual Game Game { get; set; }
    }
}
