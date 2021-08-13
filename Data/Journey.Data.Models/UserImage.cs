namespace Journey.Data.Models
{
    using System;

    using Journey.Data.Common.Models;

    public class UserImage : BaseDeletableModel<string>
    {
        public UserImage()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public string UploadName { get; set; }

        public string Extension { get; set; }
    }
}
