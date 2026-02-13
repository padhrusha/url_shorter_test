using Microsoft.EntityFrameworkCore;
using UrlShortener.Web.Data;
using UrlShortener.Web.Models;

namespace UrlShortener.Web.Services;

public class UrlShortenerService : IUrlShortenerService
{
    private readonly AppDbContext _db;

    public UrlShortenerService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<IReadOnlyList<ShortenedUrl>> GetAllAsync()
    {
        return await _db.ShortenedUrls
            .OrderByDescending(u => u.CreatedAt)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<ShortenedUrl?> GetByIdAsync(int id)
    {
        return await _db.ShortenedUrls.FindAsync(id);
    }

    public async Task<ShortenedUrl?> GetByShortCodeAsync(string shortCode)
    {
        return await _db.ShortenedUrls
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.ShortCode == shortCode);
    }

    public async Task<ShortenedUrl> CreateAsync(string originalUrl)
    {
        var entity = new ShortenedUrl
        {
            OriginalUrl = originalUrl.Trim(),
            ShortCode = await GenerateUniqueCodeAsync(),
            CreatedAt = DateTime.UtcNow
        };

        _db.ShortenedUrls.Add(entity);
        await _db.SaveChangesAsync();
        return entity;
    }

    public async Task<ShortenedUrl?> UpdateAsync(int id, string newOriginalUrl)
    {
        var entity = await _db.ShortenedUrls.FindAsync(id);
        if (entity is null) return null;

        entity.OriginalUrl = newOriginalUrl.Trim();
        await _db.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _db.ShortenedUrls.FindAsync(id);
        if (entity is null) return false;

        _db.ShortenedUrls.Remove(entity);
        await _db.SaveChangesAsync();
        return true;
    }


    public async Task IncrementClickCountAsync(string shortCode)
    {
        await _db.Database.ExecuteSqlInterpolatedAsync(
            $"UPDATE ShortenedUrls SET ClickCount = ClickCount + 1 WHERE ShortCode = {shortCode}");
    }


    private async Task<string> GenerateUniqueCodeAsync()
    {
        const int maxAttempts = 10;
        for (int i = 0; i < maxAttempts; i++)
        {
            var code = ShortCodeGenerator.Generate();
            var exists = await _db.ShortenedUrls.AnyAsync(u => u.ShortCode == code);
            if (!exists) return code;
        }

        throw new InvalidOperationException("Failed to generate a unique short code after multiple attempts.");
    }
}
