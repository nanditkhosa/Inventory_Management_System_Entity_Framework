using System;

namespace Core
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedTimeStamp { get; set; }
        public DateTime LastModifiedTimeStamp { get; set; }
        public string LastModifiedUser { get; set; }
    }
}
