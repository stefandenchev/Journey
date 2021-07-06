namespace Journey.Data.Models
{
    using System;

    public class Log
    {
        public int Id { get; set; }

        public string TableName { get; set; }

        public string OperationType { get; set; }

        public DateTime DateOfChange { get; set; }
    }
}
