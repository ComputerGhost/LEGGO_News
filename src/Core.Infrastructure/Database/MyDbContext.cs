using Core.Domain.Common.Entities;
using Core.Infrastructure.Startup;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Core.Infrastructure.Database;
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
    public virtual DbSet<UserEntity> Users { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AlbumEntity>()
            .ToTable("Albums");

        modelBuilder.Entity<AlbumTypeEntity>()
            .ToTable("AlbumTypes");

        modelBuilder.Entity<FileEntity>()
            .ToTable("Files");

        modelBuilder.Entity<ImageEntity>()
            .ToTable("Images");

        modelBuilder.Entity<UserEntity>()
            .ToTable("Users");
    }
}

internal static class MyDbContextExtensions
{
    public static IServiceCollection AddMyDatabase(this IServiceCollection services)
    {
        return services.AddDbContext<MyDbContext>(config =>
        {
            var serviceProvider = services.BuildServiceProvider();
            var options = serviceProvider.GetRequiredService<IOptions<InfrastructureOptions>>().Value;
            config.UseSqlServer(options.DatabaseConnectionString);
        });
    }
}
