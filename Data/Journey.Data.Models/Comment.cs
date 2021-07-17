namespace Journey.Data.Models
{
    using Journey.Data.Common.Models;

    public class Comment : BaseDeletableModel<int>
    {
        public int ForumPostId { get; set; }

        public virtual ForumPost ForumPost { get; set; }

        public string Content { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public int? ParentId { get; set; }

        public virtual Comment Parent { get; set; }
    }
}
