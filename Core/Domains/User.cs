using System.Collections.Generic;

namespace Core.Domains
{
    public class User : BaseEntity
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }
        public virtual string PasswordHash { get; set; }
        public virtual string PasswordSalt { get; set; }

        public virtual ICollection<Facility> Facilities { get; set; }

    }
}
