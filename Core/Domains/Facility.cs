using System.Collections.Generic;

namespace Core.Domains
{
    public class Facility : BaseEntity
    {
        public string Name { get; set; }
        public string Landmark { get; set; }
        public string Address { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }

        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<Resource> Resources { get; set; }

    }
}
