using Database.Internal.Models;
using Microsoft.EntityFrameworkCore;

namespace Database
{
    public class DatabaseContext : DbContext
    {
        internal DbSet<Article> Articles { get; set; }
        internal DbSet<Character> Characters { get; set; }
        internal DbSet<Lead> Leads { get; set; }
        internal DbSet<Media> Medias { get; set; }
        internal DbSet<Tag> Tags { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            // Set up tables and columns

            modelBuilder.Entity<Article>()
                .Property(nameof(Article.Id))
                .UseIdentityColumn();

            modelBuilder.Entity<Character>()
                .Property(nameof(Character.Id))
                .UseIdentityColumn();

            modelBuilder.Entity<Lead>()
                .Property(nameof(Lead.Id))
                .UseIdentityColumn();

            modelBuilder.Entity<Media>()
                .Property(nameof(Media.Id))
                .UseIdentityColumn();

            modelBuilder.Entity<Tag>(e => {
                e.Property(nameof(Tag.Id)).UseIdentityColumn();
                e.HasIndex(nameof(Tag.Name)).IsUnique();
            });


            // Set up relations

            modelBuilder.Entity<Article>()
                .HasMany(left => left.Tags)
                .WithMany(right => right.Articles)
                .UsingEntity(join => join.ToTable("ArticleTag"));

        }

    }
}
