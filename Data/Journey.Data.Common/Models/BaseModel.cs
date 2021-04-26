namespace Journey.Data.Common.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public abstract class BaseModel<TKey> : IAuditInfo
    {
        [Key]
        public TKey Id { get; set; }

        public DateTime CreatedOn_17114092 { get; set; }

        public DateTime? ModifiedOn_17114092 { get; set; }
    }
}
