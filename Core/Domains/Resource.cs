using System;

namespace Core.Domains
{
    public  class Resource : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int InitialCount { get; set; }
        public int CurrentCount { get; set; }
        public int? FacilityId { get; set; }
        public string Comment { get; set; }

        public virtual Facility Facility { get; set; }
    }
}
