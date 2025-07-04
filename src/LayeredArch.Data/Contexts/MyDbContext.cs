using LayeredArch.Business.Models.Entities;
using LayeredArch.Business.Models.Parameters;
using LayeredArch.Business.Models.Relationships;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LayeredArch.Data.Contexts
{
    public class MyDbContext : DbContext
    {

        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }

        //Entities
        public DbSet<User> E_Users { get; set; }

        // Parameters
        public DbSet<Role> P_Roles { get; set; }
        public DbSet<Module> P_Modules { get; set; }
        public DbSet<Claim> P_Claims { get; set; }

        // Relationsships
        public DbSet<Permission> R_Permissions { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /* NOTE: For cases of fields where the text type mapping is not declared  */
            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");

            /* NOTE: Apply settings from Reflection Mapping files (read meta data at run time) */
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MyDbContext).Assembly);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
                relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var properties = entityType.GetProperties()
                    .Where(p => p.ClrType == typeof(DateTime) || p.ClrType == typeof(DateTime?));

                foreach (var property in properties)
                {
                    property.SetValueConverter(new ValueConverter<DateTime, DateTime>(
                        v => v.Kind == DateTimeKind.Unspecified ? DateTime.SpecifyKind(v, DateTimeKind.Utc) : v.ToUniversalTime(),
                        v => v));
                }
            }

            base.OnModelCreating(modelBuilder);

        }

    }
}
