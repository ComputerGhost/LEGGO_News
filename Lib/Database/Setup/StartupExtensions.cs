using Database.Internal;
using Database.Internal.Setup;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Database.Setup
{
    public static class StartupExtensions
    {
        public static void AddDatabase(this IServiceCollection services, DatabaseConfiguration config)
        {
            services.AddDbContext(config.ConnectionString);
            services.AddDependencyInjection();
            services.AddAutoMapper(typeof(MappingProfile));
        }


        // Too simple to move to a separate file.
        private static void AddDbContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<DatabaseContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });
        }
    }
}
