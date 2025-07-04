using LayeredArch.Business.Models.Entities;
using LayeredArch.Business.Models.Parameters;
using LayeredArch.Data.Contexts;
using LayeredArch.Helper.StaticVariables;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LayeredArch.Infra.Data
{
    public static class DbSeeder
    {
        public static async Task SeedAsync(MyDbContext context)
        {
            var hasher = new PasswordHasher<User>();

            if (!context.P_Roles.Any())
            {
                var roles = new List<Role>
                {
                    new Role { Name = "Developer", Description = "Desenvolvedor", Tag = "developer", Activated = true },
                    new Role { Name = "Chefe de Equipa", Description = "Chefe técnico", Tag = "chefe_de_equipa", Activated = true },
                    new Role { Name = "Tech Lead", Description = "Líder técnico", Tag = "tech_lead", Activated = true },
                    new Role { Name = "Arquiteto de Software", Description = "Responsável por arquitetura", Tag = "arquiteto", Activated = true },
                    new Role { Name = "Responsável de Área", Description = "Responsável de área", Tag = "responsavel_de_area", Activated = true },
                    new Role { Name = "Diretor de Área", Description = "Diretor de área", Tag = "diretor_area", Activated = true },
                    new Role { Name = "Administrador", Description = "Administrador do sistema", Tag = "admin", Activated = true }
                };

                await context.P_Roles.AddRangeAsync(roles);
                await context.SaveChangesAsync();
            }

            if (!context.P_Modules.Any())
            {
                var modules = new[]
                {
                new Module { Name = "01 - Dashboard", Description = "...", Tag = "dashboard", Activated = true },
                new Module { Name = "90 - Utilizadores", Description = "...", Tag = "user", Activated = true },
                new Module { Name = "91 - Perfis", Description = "...", Tag = "role", Activated = true },
                new Module { Name = "92 - Módulos", Description = "...", Tag = "module", Activated = true },
                new Module { Name = "93 - Permissões (Claims)", Description = "...", Tag = "permission", Activated = true }
            };

                await context.P_Modules.AddRangeAsync(modules);
                await context.SaveChangesAsync();
            }

            if (!context.E_Users.Any())
            {
                var arquiteto = await context.P_Roles.FirstOrDefaultAsync(r => r.Tag == "arquiteto");

                if (arquiteto != null)
                {
                    var user = new User
                    {
                        Name = "Carlos R Freire",
                        Email = "carlos.rfreire@grupojap.pt",
                        Password = "", // ⚠️ Idealmente deve ser hasheada
                        RoleId = arquiteto.Id,
                        Activated = true,
                        CreatedDate = DateTime.UtcNow,
                        PasswordChanged = false
                    };
                    user.Password = hasher.HashPassword(user, GlobalHelper.PasswordDefault); //Note: criptografia PBKDF2

                    await context.E_Users.AddAsync(user);
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
