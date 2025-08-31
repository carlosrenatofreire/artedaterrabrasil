using ArteDaTerraBrasil.Business.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArteDaTerraBrasil.Data.Mappings.Entities
{
    public class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(u => u.Name).IsRequired().HasMaxLength(100);

            builder.Property(u => u.Password).IsRequired().HasMaxLength(250);

            builder.Property(u => u.Email).IsRequired().HasMaxLength(150);

            builder.Property(p => p.CreatedDate).IsRequired().HasColumnType("datetime2");
            builder.Property(p => p.ChangedDate).IsRequired().HasColumnType("datetime2");


            builder.ToTable("E_Users");
        }
    }
}
