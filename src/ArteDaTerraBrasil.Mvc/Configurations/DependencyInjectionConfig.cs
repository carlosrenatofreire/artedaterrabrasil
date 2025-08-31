using ArteDaTerraBrasil.Business.Interfaces.Auxiliares;
using ArteDaTerraBrasil.Business.Interfaces.Entities;
using ArteDaTerraBrasil.Business.Interfaces.Parameters;
using ArteDaTerraBrasil.Business.Interfaces.Relationships;
using ArteDaTerraBrasil.Business.Interfaces.Shareds;
using ArteDaTerraBrasil.Business.Services.Auxiliares;
using ArteDaTerraBrasil.Business.Services.Entities;
using ArteDaTerraBrasil.Business.Services.Parameters;
using ArteDaTerraBrasil.Business.Services.Relationship;
using ArteDaTerraBrasil.Business.Services.Shareds;
using ArteDaTerraBrasil.Data.Contexts;
using ArteDaTerraBrasil.Data.Repository.Auxiliares;
using ArteDaTerraBrasil.Data.Repository.Entities;
using ArteDaTerraBrasil.Data.Repository.Parameters;
using ArteDaTerraBrasil.Data.Repository.Relationships;
using ArteDaTerraBrasil.Data.Repository.Shareds;
using ArteDaTerraBrasil.Infra.Interfaces.Security;
using ArteDaTerraBrasil.Infra.Services.Security;
using Microsoft.AspNetCore.Authentication;
using System.Security.Principal;

namespace ArteDaTerraBrasil.Mvc.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependenciesMvc(this IServiceCollection services, IConfiguration configuration)
        {
            /* General and External Services */
            services.AddScoped<IPrincipal>(provider => { return provider.GetService<IHttpContextAccessor>().HttpContext.User; });
            services.AddHttpContextAccessor();
            services.AddHttpClient();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<INotifierService, NotifierService>();
            services.AddScoped<IAccessorService, AccessorService>();
            services.AddScoped<IPasswordService, PasswordService>();
            services.AddTransient<IClaimsTransformation, ClaimsTransformationService>();

            /* Services */
            services.AddScoped<IAuditLogService, AuditLogService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IModuleService, ModuleService>();
            services.AddScoped<IPermissionService, PermissionService>();
            services.AddScoped<IClaimService, ClaimService>();


            /* Repositories */
            services.AddScoped<IAuditLogRepository, AuditLogRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IModuleRepository, ModuleRepository>();
            services.AddScoped<IPermissionRepository, PermissionRepository>();
            services.AddScoped<IClaimRepository, ClaimRepository>();

            // Data
            services.AddScoped<MyDbContext>();

            return services;

        }
    }
}
