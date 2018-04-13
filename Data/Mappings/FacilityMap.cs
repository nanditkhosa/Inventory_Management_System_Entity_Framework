﻿
using System.Data.Entity.ModelConfiguration;
using Core.Domains;

namespace Data.Mappings
{
    class FacilityMap : EntityTypeConfiguration<Facility>
    {
        public FacilityMap()
        {
            // Table 
            ToTable("Facility", "dbo");
            // Primary Key
            HasKey(u => u.Id);

            // validations
            Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);

            Property(c => c.Landmark)
                .IsRequired();

            Property(c => c.Address)
                .IsRequired();

            Property(c => c.CreatedTimeStamp)
                .HasColumnType("datetime2");

            Property(c => c.LastModifiedTimeStamp)
                 .HasColumnType("datetime2");

            HasMany(u => u.Users)
                .WithMany(f => f.Facilities)
                .Map(fu => { fu.MapLeftKey("FacilityFKId");
                    fu.MapRightKey("UserFKId");
                    fu.ToTable("FacilityUser");
                });
            

            HasMany(r => r.Resources)
                .WithRequired(f => f.Facility);
                   
        }
    }
}
