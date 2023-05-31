using DataAccess;
using Microsoft.Data.SqlClient;
using System.Data;

namespace CMS.Setup
{
    public static class DependencyInjection
    {
        public static void AddDependencyInjection(this IServiceCollection services, Config config)
        {
            services.AddTransient<IDbConnection>(provider => new SqlConnection(config.ConnectionString));
        }
    }
}
