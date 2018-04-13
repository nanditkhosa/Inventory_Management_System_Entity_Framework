
using System.Data.Entity.ModelConfiguration;
using Core.Domains;

namespace Data.Mappings
{
    public class ResourceMap : EntityTypeConfiguration<Resource>
    {
        public ResourceMap()
        {
            // Table 
            ToTable("Resource", "dbo");
            // Primary Key
            HasKey(u => u.Id);

            // validations
            Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);

            Property(c => c.Description)
                .IsRequired();

            Property(c => c.InitialCount)
                .IsRequired();

            Property(c => c.CreatedTimeStamp)
                .HasColumnType("datetime2");

            Property(c => c.LastModifiedTimeStamp)
                 .HasColumnType("datetime2");

            HasRequired(f => f.Facility)
                .WithMany( r => r.Resources)
                .HasForeignKey(r => r.FacilityId)
                .WillCascadeOnDelete(false);

        }
    }
}
