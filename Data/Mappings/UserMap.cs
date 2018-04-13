using Core.Domains;
using System.Data.Entity.ModelConfiguration;


namespace Data.Mappings
{
    public class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            // Table 
            ToTable("User", "dbo");
            HasKey(u => u.Id);

            // validations
            Property(c => c.UserName)
                    .IsRequired()
                    .HasMaxLength(100);

            Property(c => c.CreatedTimeStamp)
                .HasColumnType("datetime2");

            Property(c => c.LastModifiedTimeStamp)
                 .HasColumnType("datetime2");

            HasMany(f => f.Facilities)
                .WithMany(u => u.Users)
                .Map(fu =>
                        {
                            fu.MapLeftKey("UserFKId");
                            fu.MapRightKey("FacilityFKId");
                            fu.ToTable("FacilityUser");
                        }
                    );

        }
    }
}