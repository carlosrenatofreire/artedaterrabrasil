using LayeredArch.Business.Models.Parameters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LayeredArch.Data.Mappings.Parameters
{
    public class ClaimMapping : IEntityTypeConfiguration<Claim>
    {
        public void Configure(EntityTypeBuilder<Claim> builder)
        {

            builder.HasKey(p => p.Id);

            builder.Property(x => x.Description).HasMaxLength(500);

            builder.Property(x => x.Tag).HasMaxLength(50);

            builder.HasOne(p => p.Module)
                .WithMany(p => p.Claims)
                .HasForeignKey(p => p.ModuleId);

            builder.ToTable("P_Claims");

        }
    }
}
