using Microsoft.EntityFrameworkCore;
using Analytics.Api.Models;

namespace Analytics.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Article> Articles { get; set; }
        public DbSet<ArticleDetails> ArticleDetails { get; set; }
        public DbSet<Pageview> Pageviews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Article>()
                .HasOne(a => a.Details)
                .WithOne(d => d.Article)
                .HasForeignKey<ArticleDetails>(d => d.ArticleId);

            modelBuilder.Entity<Article>()
                .HasMany(a => a.Pageviews)
                .WithOne(p => p.Article)
                .HasForeignKey(p => p.ArticleId);
        }
    }
}
