using Microsoft.EntityFrameworkCore;
using UrlShortener.Web.Models;

namespace UrlShortener.Web.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<ShortenedUrl> ShortenedUrls => Set<ShortenedUrl>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ShortenedUrl>(entity =>
        {
            entity.HasIndex(e => e.ShortCode).IsUnique();
            entity.Property(e => e.OriginalUrl).IsRequired().HasMaxLength(2048);
            entity.Property(e => e.ShortCode).IsRequired().HasMaxLength(10);
        });
    }
}
