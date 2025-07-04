using LayeredArch.Business.Interfaces.Auxiliares;
using LayeredArch.Business.Interfaces.Entities;
using LayeredArch.Business.Interfaces.Parameters;
using LayeredArch.Business.Interfaces.Relationships;
using LayeredArch.Business.Interfaces.Shareds;
using LayeredArch.Business.Services.Auxiliares;
using LayeredArch.Business.Services.Entities;
using LayeredArch.Business.Services.Parameters;
using LayeredArch.Business.Services.Relationship;
using LayeredArch.Business.Services.Shareds;
using LayeredArch.Data.Contexts;
using LayeredArch.Data.Repository.Auxiliares;
using LayeredArch.Data.Repository.Entities;
using LayeredArch.Data.Repository.Parameters;
using LayeredArch.Data.Repository.Relationships;
using LayeredArch.Data.Repository.Shareds;
using LayeredArch.Infra.Interfaces.Security;
using LayeredArch.Infra.Services.Security;
using Microsoft.AspNetCore.Authentication;
using System.Security.Principal;

namespace LayeredArch.Mvc.Configurations
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
            services.AddScoped<IClaimsTransformation, ClaimsTransformationService>();

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
