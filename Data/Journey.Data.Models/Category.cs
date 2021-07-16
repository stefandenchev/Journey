namespace Journey.Data.Models
{
    using System.Collections.Generic;

    using Journey.Data.Common.Models;

    public class Category : BaseDeletableModel<int>
    {
        public Category()
        {
            this.ForumPosts = new HashSet<ForumPost>();
        }

        public string Title { get; set; }

        public virtual ICollection<ForumPost> ForumPosts { get; set; }
    }
}
