
using LayeredArch.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace LayeredArch.Mvc.Configurations
{
    public static class ContextConfig
    {
        public static IServiceCollection AddContextConfig(this IServiceCollection services, IConfiguration configuration)
        {
            var connection = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<MyDbContext>(options => options.UseSqlServer(connection));

            return services;

        }
    }
}
