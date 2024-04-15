using Core.Startup;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Core.Common.Database;
internal class MyDbContext : DbContext
{
    public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AlbumEntity> Albums { get; set; } = null!;
    public virtual DbSet<AlbumTypeEntity> AlbumTypes { get; set; } = null!;
    public virtual DbSet<FileEntity> Files { get; set; } = null!;
    public virtual DbSet<ImageEntity> Images { get; set; } = null!;
}

internal static class MyDbContextExtensions
{
    public static IServiceCollection AddMyDatabase(this IServiceCollection services)
    {
        return services.AddDbContext<MyDbContext>(config =>
        {
            var serviceProvider = services.BuildServiceProvider();
            var options = serviceProvider.GetRequiredService<IOptions<CoreOptions>>().Value;
            config.UseSqlServer(options.DatabaseConnectionString);
        });
    }
}
