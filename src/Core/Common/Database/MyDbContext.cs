using Microsoft.EntityFrameworkCore;

namespace Core.Common.Database;
internal class MyDbContext : DbContext
{
    public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options)
    {
    }

    public DbSet<AlbumEntity> Albums { get; set; } = null!;
    public DbSet<AlbumTypeEntity> AlbumTypes { get; set; } = null!;
    public DbSet<FileEntity> Files { get; set; } = null!;
    public DbSet<ImageEntity> Images { get; set; } = null!;
}
