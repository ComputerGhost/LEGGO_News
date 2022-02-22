using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Article> Article { get; set; }
        public DbSet<Character> Character { get; set; }
        public DbSet<Media> Media { get; set; }
        public DbSet<Tag> Tag { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Some global configurations that we want are:
            // 1. Tables are the type name.
            // 2. Columns named "Id" are primary keys.

            modelBuilder.Entity<Article>()
                .Property("Id")
                .UseIdentityColumn();

            modelBuilder.Entity<Character>()
                .Property("Id")
                .UseIdentityColumn();

            modelBuilder.Entity<Media>()
                .Property("Id")
                .UseIdentityColumn();

            modelBuilder.Entity<Tag>()
                .Property("Id")
                .UseIdentityColumn();


            // Set up relations

            modelBuilder.Entity<Article>()
                .HasMany(left => left.Tags)
                .WithMany(right => right.Articles)
                .UsingEntity(join => join.ToTable("ArticleTag"));

        }

    }
}
