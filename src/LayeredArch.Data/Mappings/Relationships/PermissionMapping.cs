using LayeredArch.Business.Models.Relationships;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayeredArch.Data.Mappings.Relationships
{
    public class PermissionMapping : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {

            builder.HasKey(p => p.Id);

            builder.HasOne(p => p.Role)
                .WithMany(r => r.Permissions)
                .HasForeignKey(p => p.RoleId);

            builder.HasOne(p => p.Module)
                .WithMany(m => m.Permissions)
                .HasForeignKey(p => p.ModuleId);

            builder.HasOne(p => p.Claim)
                .WithMany(p => p.Permissions)
                .HasForeignKey(p => p.ClaimId);

            builder.ToTable("R_Permissions");

        }
    }
}
