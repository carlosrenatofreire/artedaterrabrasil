using LayeredArch.Business.Models.Parameters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LayeredArch.Data.Mappings.Parameters
{
    public class RoleMapping : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(p => p.Description)
                   .HasMaxLength(500);

            builder.Property(p => p.Tag)
                   .HasMaxLength(50);

            builder.HasMany(r => r.Users)
                   .WithOne(u => u.Role)
                   .HasForeignKey(u => u.RoleId);

            builder.HasOne(r => r.Supervisor)
                   .WithMany()
                   .HasForeignKey(r => r.SupervisorId);

            // Table Mapping
            builder.ToTable("P_Roles");

        }
    }
}
